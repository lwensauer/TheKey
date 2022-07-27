using Microsoft.AspNetCore.SignalR;

namespace TheKey.Backend.Hubs;
public class BlogHub : Hub<IBlogEntryClient>
{ }
