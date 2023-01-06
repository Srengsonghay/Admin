using Admin.Models;

namespace Admin.ViewModel
{
    public class SolutionDetailViewModel
    {
        public SolutionsDetail solutionDetail { get; set; }
        public List<Solutions> relatedSolution { get; set; }
    }
}
