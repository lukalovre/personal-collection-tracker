using System.Threading.Tasks;
using CollectionTracker.Repositories.External;
using Repositories;

namespace CollectionTracker.Repositories;

public class StandupExternal : IExternal<Standup>
{
    public async Task<Standup> GetItem(string url)
    {
        url = HtmlHelper.CleanUrl(url);

        if (url.Contains(Imdb.UrlIdentifier))
        {
            var item = await Imdb.GetImdbItem<Standup>(url);

            return new Standup
            {
                Title = item.StandupTitle,
                Director = item.Director,
                Runtime = item.Runtime,
                Year = item.Year
            };
        }

        return new Standup();
    }

}
