using System.Threading.Tasks;
using CollectionTracker.Models;
using CollectionTracker.Repositories.External;

namespace CollectionTracker.Repositories;

public class MusicVideoExternal : IExternal<MusicVideo>
{
    public async Task<MusicVideo> GetItem(string url)
    {
        if (url.Contains(YouTube.UrlIdentifier))
        {
            var item = await YouTube.GetYoutubeItem<MusicVideo>(url);

            return new MusicVideo
            {
                Title = item.Title,
                ExternalID = item.Link,
                Year = item.Year,
                Runtime = item.Runtime,
                Author = item.Author
            };
        }

        return new MusicVideo();
    }
}
