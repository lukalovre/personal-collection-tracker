using System.Threading.Tasks;
using CollectionTracker.Models;
using CollectionTracker.Repositories.External;

namespace CollectionTracker.Repositories;

public class ClipsExternal : IExternal<Clip>
{
    public async Task<Clip> GetItem(string url)
    {
        if (url.Contains(YouTube.UrlIdentifier))
        {
            var item = await YouTube.GetYoutubeItem<Clip>(url);

            return new Clip
            {
                Title = item.Title,
                ExternalID = item.Link,
                Year = item.Year,
                Runtime = item.Runtime,
                Author = item.Author
            };
        }

        return new Clip();
    }
}
