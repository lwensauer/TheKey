using System.Collections.Generic;

namespace TheKey.Backend.WordCounter

{
    public interface IWordCounter
    {
        Dictionary<string, int> Process(string input);
    }
}
