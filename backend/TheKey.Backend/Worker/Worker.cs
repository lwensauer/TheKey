using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheKey.Backend.Hubs;
using TheKey.Backend.Models;
using TheKey.Backend.Persistence;
using TheKey.Backend.WordCounter;
using WordPressPCL;

namespace TheKey.Backend.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHubContext<BlogHub, IBlogEntryClient> _blogHub;
    private readonly IWordCounter _wordCounter;
    private readonly IBlogEntryRepository _blogEntryRepository;

    public Worker(ILogger<Worker> logger, IHubContext<BlogHub, IBlogEntryClient> blogHub, IWordCounter wordCounter, IBlogEntryRepository blogEntryRepository)
    {
        _logger = logger;
        _blogHub = blogHub;
        _wordCounter = wordCounter;
        _blogEntryRepository = blogEntryRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var client = new WordPressClient("https://internate.org/wp-json/");
            var posts = await client.Posts.GetAllAsync();

            foreach (var post in posts)
            {
                var postId = post.Id;
                if (!_blogEntryRepository.AlreadyExists(postId))
                    await ProcessPost(post);

            }
            await Task.Delay(System.TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task ProcessPost(WordPressPCL.Models.Post post)
    {
        var htmlMessage = post.Content.Rendered;
        var wordCounterMap = _wordCounter.Process(htmlMessage);

        var msg = $"Unique words: {wordCounterMap.Count} // words total: {wordCounterMap.Values.Sum(v => v)}";
        var newBlogEntry = new BlogEntry() { Id = post.Id, Message = msg, Title = post.Title.Rendered, WordCounterMap = wordCounterMap };
        _blogEntryRepository.Add(newBlogEntry);
        await _blogHub.Clients.All.NewBlogEntry(newBlogEntry);

        await Task.Delay(1000);
    }
}
