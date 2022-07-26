﻿using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using TheKey.Backend.Blog.Commands;
using TheKey.Backend.Persistence;
using WordPressPCL;

namespace TheKey.Backend.Blog;

public class PollBlogEntryBackgroundService : BackgroundService
{
    private readonly IBlogEntryRepository _blogEntryRepository;

    private ISender _mediator;

    public PollBlogEntryBackgroundService(ISender mediator, IBlogEntryRepository blogEntryRepository)
    {
        _mediator = mediator;

        // Als Query (CQRS)
        _blogEntryRepository = blogEntryRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine($"15 Sekunden warten bis zum Start der Verarbeitung...");
        await Task.Delay(System.TimeSpan.FromSeconds(15), stoppingToken);
        while (!stoppingToken.IsCancellationRequested)
        {
            var client = new WordPressClient("https://internate.org/wp-json/");
            var posts = await client.Posts.GetAllAsync();

            foreach (var post in posts)
            {
                var postId = post.Id;
                if (!_blogEntryRepository.AlreadyExists(postId))
                {
                    var isProcessed = await _mediator.Send(new NewBlogEntryCommand(post.Id, post.Title.Rendered, post.Content.Rendered));
                    if (isProcessed)
                        Console.WriteLine($"Post {post.Id} verarbeitet");
                }
            }
            await Task.Delay(System.TimeSpan.FromMinutes(1), stoppingToken);
        }

    }
}
