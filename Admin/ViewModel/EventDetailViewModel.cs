using Admin.Models;


namespace Admin.ViewModel
{
    public class EventDetailViewModel
    {
        public Events? EventDetail { get; set; }
        public List<Events>? Last5Events { get; set; }
    }


}
