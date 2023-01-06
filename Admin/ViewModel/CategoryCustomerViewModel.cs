using Admin.Models;

namespace Admin.ViewModel
{
    public class CategoryCustomerViewModel
    {
        public List<Customers>? customer { get; set; }
        public List<Category_customer>? category_Customer { get; set; }
    }
}
