using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheKey.Backend.Hubs;
using TheKey.Backend.Models;
using TheKey.Backend.Persistence;
using TheKey.Backend.WordCounter;

namespace TheKey.Backend.Blog.Commands;

public class NewBlogEntryCommandHandler : IRequestHandler<NewBlogEntryCommand, bool>
{
    private readonly IHubContext<BlogHub, IBlogEntryClient> _blogHub;
    private readonly IWordCounter _wordCounter;
    private readonly IBlogEntryRepository _blogEntryRepository;

    public NewBlogEntryCommandHandler(IHubContext<BlogHub, IBlogEntryClient> blogHub, IWordCounter wordCounter, IBlogEntryRepository blogEntryRepository)
    {
        _blogHub = blogHub;
        _wordCounter = wordCounter;
        _blogEntryRepository = blogEntryRepository;
    }

    public async Task<bool> Handle(NewBlogEntryCommand post, CancellationToken cancellationToken)
    {
        var wordCounterMap = _wordCounter.Process(post.Content);

        var msg = $"Unique words: {wordCounterMap.Count} // words total: {wordCounterMap.Values.Sum(v => v)}";
        var newBlogEntry = new BlogEntry() { Id = post.Id, Message = msg, Title = post.Title, WordCounterMap = wordCounterMap, Content = post.Content };
        _blogEntryRepository.Add(newBlogEntry);
        await _blogHub.Clients.All.NewBlogEntry(newBlogEntry);

        await Task.Delay(1000);

        return true;
    }
}
