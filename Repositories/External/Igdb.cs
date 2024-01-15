using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Repositories;

namespace AvaloniaApplication1.Repositories.External;

public class Igdb : IExternal<Game>
{
    private const string API_KEY_FILE_NAME = "igdb_keys.txt";
    public static string UrlIdentifier => "igdb.com";

    public Game GetItem(string url)
    {

        using var client = new WebClient();
        client.Headers.Add("user-agent", "...");
        var content = client.DownloadData(url);
        using var stream = new MemoryStream(content);
        string result = System.Text.Encoding.UTF8.GetString(stream.ToArray());
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(result);

        var title = GetTitle(htmlDocument);
        var id = GetID(htmlDocument);
        var year = GetYear(htmlDocument);
        var imageUrl = GetImageUrl(htmlDocument);

        var destinationFile = Paths.GetTempPath<Game>();
        HtmlHelper.DownloadPNG(imageUrl, destinationFile);

        return new Game
        {
            ExternalID = id.ToString(),
            Title = title,
            Year = year
        };
    }

    private int GetYear(HtmlDocument htmlDocument)
    {
        var node = htmlDocument.DocumentNode.SelectSingleNode(
          "//title");

        var titleAndYear = node.InnerText.Trim();

        var split1 = titleAndYear.Split('(').Last();
        var split2 = split1.Split(')').First();

        return HtmlHelper.GetYear(split2);
    }

    private int GetID(HtmlDocument htmlDocument)
    {
        var node = htmlDocument.DocumentNode.SelectSingleNode(
            "//meta[contains(@id, 'pageid')]");

        return int.Parse(node.GetAttributeValue("data-game-id", string.Empty).Trim());
    }

    private string GetImageUrl(HtmlDocument htmlDocument)
    {
        var node = htmlDocument.DocumentNode.SelectSingleNode(
            "//meta[contains(@property, 'og:image')]");

        return node.GetAttributeValue("content", string.Empty).Trim();
    }

    private string GetTitle(HtmlDocument htmlDocument)
    {
        var node = htmlDocument.DocumentNode.SelectSingleNode(
            "//meta[contains(@property, 'og:title')]");

        return node.GetAttributeValue("content", string.Empty).Trim();
    }
}
