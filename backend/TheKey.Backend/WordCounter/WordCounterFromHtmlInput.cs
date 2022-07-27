using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace TheKey.Backend.WordCounter;

public class WordCounterFromHtmlInput : IWordCounter
{
    private static readonly string REGEXPR_ALL_WHITESPACES = @"\s+";
    private static readonly string REGEXPR_ALL_WORDS = @"[^\w'+]";
    public Dictionary<string, int> Process(string input)
    {
        var rc = new Dictionary<string, int>();
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(input);
        string stripedInput = StripHtml(doc);
        string clearedFromWhiteSpaces = ReplaceAllWhiteSpaces(stripedInput).Trim();

        var contents = Regex.Split(clearedFromWhiteSpaces, REGEXPR_ALL_WORDS);
        int value;
        foreach (var word in contents)
            rc[word] = rc.TryGetValue(word, out value) ? ++value : 1;

        rc.Remove("");
        return rc;
    }

    private static string ReplaceAllWhiteSpaces(string stripedInput)
    {
        return Regex.Replace(stripedInput, REGEXPR_ALL_WHITESPACES, " ");
    }

    private static string StripHtml(HtmlAgilityPack.HtmlDocument doc)
    {
        return HttpUtility.HtmlDecode(doc.DocumentNode.InnerText);
    }

}
