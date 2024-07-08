using System.Threading.Tasks;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Repositories.External;
using Repositories;

namespace AvaloniaApplication1.Repositories;

public class MovieExternal : IExternal<Movie>
{

    public async Task<Movie> GetItem(string url)
    {
        url = HtmlHelper.CleanUrl(url);

        if (url.Contains(Imdb.UrlIdentifier))
        {
            var item = await Imdb.GetImdbItem<Movie>(url);

            return new Movie
            {
                Title = item.Title,
                Runtime = item.Runtime,
                Year = item.Year,
                ExternalID = item.ExternalID,
                Director = item.Director
            };
        }

        return new Movie();
    }

}
