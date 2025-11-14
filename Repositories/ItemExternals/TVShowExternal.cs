using System.Threading.Tasks;
using CollectionTracker.Repositories.External;
using Repositories;

namespace CollectionTracker.Repositories;

public class TVShowExternal : IExternal<TVShow>
{
    public async Task<TVShow> GetItem(string url)
    {
        if (url.Contains(YouTube.UrlIdentifier))
        {
            var item = await YouTube.GetYoutubeItem<TVShow>(url);

            return new TVShow
            {
                Title = item.Title,
                ExternalID = item.Link,
                Year = item.Year,
                Runtime = item.Runtime
            };
        }

        url = HtmlHelper.CleanUrl(url);

        if (url.Contains(Imdb.UrlIdentifier))
        {
            var item = await Imdb.GetImdbItem<TVShow>(url);

            return new TVShow
            {
                Title = item.Title,
                Runtime = item.Runtime,
                Year = item.Runtime,
                ExternalID = item.ExternalID,
                Director = item.Director
            };
        }

        return new TVShow();
    }
}
