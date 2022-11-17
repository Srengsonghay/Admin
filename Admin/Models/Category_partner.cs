using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models 
{
    public class Category_partner
    {
        [Key]
        public Guid id { get; set; }
        public string? category_name { get; set; }
        public bool? status { get; set; }

        [InverseProperty(nameof(Admin.Models.Partners.category_partner))]
        public virtual ICollection<Partners> Partners { get; set; }
    }
}
