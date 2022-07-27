using System.Threading.Tasks;
using TheKey.Backend.Models;

namespace TheKey.Backend.Hubs;

public interface IBlogEntryClient
{
    Task NewBlogEntry(BlogEntry blogEntry);
}
