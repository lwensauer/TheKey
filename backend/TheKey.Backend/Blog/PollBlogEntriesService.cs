using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TheKey.Backend.Blog.Commands;
using TheKey.Backend.Persistence;
using WordPressPCL;

namespace TheKey.Backend.Blog;

public class PollBlogEntriesService : BackgroundService
{
    private readonly IBlogEntryRepository _blogEntryRepository;

    private ISender _mediator;

    public PollBlogEntriesService(ISender mediator, IBlogEntryRepository blogEntryRepository)
    {
        _mediator = mediator;
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
                {
                    var isProcessed = await _mediator.Send(new BlogEntryCommand(post.Id, post.Title.Rendered, post.Content.Rendered));
                    if (isProcessed)
                        Console.WriteLine($"{post.Id} processed");
                }
            }
            await Task.Delay(System.TimeSpan.FromMinutes(1), stoppingToken);
        }

    }
}
