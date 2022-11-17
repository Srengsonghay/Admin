
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models
{
    public class Category_customer
    {
        [Key]
        public Guid id { get; set; }
        public string? category_name { get; set; }
        public bool? status { get; set; }
        [InverseProperty(nameof(Admin.Models.Customers.category_customer))]
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
