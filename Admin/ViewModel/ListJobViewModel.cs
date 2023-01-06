using Admin.Models;

namespace Admin.ViewModel
{
    public class ListJobViewModel
    {
        public List<JobApplication> Job { get; set; }
        public List<Careers> career { get; set; }
        public Guid? select_career { get; set; }
        
    }
}
