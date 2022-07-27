using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using TheKey.Backend.Hubs;
using TheKey.Backend.Models;
using TheKey.Backend.Persistence;

namespace TheKey.Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class BlogEntryController : ControllerBase
{
    private readonly IHubContext<BlogHub, IBlogEntryClient> _chatHub;
    private readonly IBlogEntryRepository _blogEntryRepository;

    public BlogEntryController(IHubContext<BlogHub, IBlogEntryClient> chatHub, IBlogEntryRepository blogEntryRepository)
    {
        _chatHub = chatHub;
        _blogEntryRepository = blogEntryRepository;
    }
    [HttpGet]
    /// <summary>
    /// Zeigt alle verarbeiteten Blog-Einträge an
    /// </summary>
    /// <returns>Liste an Blog-Einträgen mit ID, Titel, Inhalt und einer Word-Count-Map </returns>
    public IEnumerable<BlogEntry> Get()
    {
        // Test was bei Bedarf die SignalR-Client was zu schicken 
        // await _chatHub.Clients.All.NewBlogEntry(new BlogEntry() { Id = 34, Message = "msg" });

        return _blogEntryRepository.GetAll();
    }
}

