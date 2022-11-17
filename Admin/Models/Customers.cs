using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models
{
    public class Customers
    {
        [Key]
        public Guid id { get; set; }
        public Byte[]? customer_logo { get; set; }
        public string? customer_name { get; set; }
        [NotMapped]
        public IFormFile? logo { get; set; }
        public Guid category_id { get; set; }
        [ForeignKey("category_id")]
        [InverseProperty("Customers")]
        public virtual Category_customer category_customer { get; set; }        
        public string? customer_detail { get; set; }
        public bool? status { get; set; }
    }
}
