using Admin.Models;

namespace Admin.ViewModel
{
    public class HomeViewModel
    {
        public List<Events>? blog_event { get; set; }
        public List<AboutUs>? about_us { get; set; }
        public List<Partners>? partner { get; set; }
        public List<Solutions>? solutions { get; set; }

    }
}
