using System.Threading.Tasks;
using CollectionTracker.Repositories.External;
using Repositories;

namespace CollectionTracker.Repositories;

public class GameExtetrnal : IExternal<Game>
{
    public async Task<Game> GetItem(string url)
    {
        url = HtmlHelper.CleanUrl(url);

        if (url.Contains(Igdb.UrlIdentifier))
        {
            var item = await Igdb.GetItem(url);

            return new Game
            {
                ExternalID = item.ExternalID.ToString(),
                Title = item.Title,
                Year = item.Year
            };
        }

        return new Game();
    }
}
