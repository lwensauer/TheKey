using System.Collections.Generic;
using TheKey.Backend.Models;

namespace TheKey.Backend.Persistence;

public class BlogEntryRepository : IBlogEntryRepository
{
    private static readonly List<BlogEntry> _blogEntries = new();

    public bool AlreadyExists(int id)
    {
        return _blogEntries.Exists(blog => blog.Id == id);
    }

    public void Add(BlogEntry blogEntry)
    {
        _blogEntries.Add(blogEntry);
    }

    public IEnumerable<BlogEntry> GetAll()
    {
        return _blogEntries;
    }
}
