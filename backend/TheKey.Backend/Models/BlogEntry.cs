using System.Collections.Generic;

namespace TheKey.Backend.Models;

public class BlogEntry
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public Dictionary<string, int> WordCounterMap { get; internal set; }
}
