using Admin.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Text;
namespace Admin.ViewModel
{
    public class EventViewModel
    {
        public List<Events>? Post { get; set; }
        public List<Events>? LastPost { get; set; }

        
    }


    public static class alterUrl
    {
        public static string ChangeUrl(this string url)
        {
            string ChangeUrl = SlugGenerator.SlugGenerator.GenerateSlug(url);
            return ChangeUrl;
        }
    }
}
