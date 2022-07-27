using System.Collections.Generic;
using TheKey.Backend.Models;

namespace TheKey.Backend.Persistence;

public interface IBlogEntryRepository
{
    IEnumerable<BlogEntry> GetAll();
    public void Add(BlogEntry blogEntry);
    public bool AlreadyExists(int id);
}

