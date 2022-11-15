using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models{
    public class Partners{
        [Key]
        public Guid id {get; set;}
        public Byte[]? partner_logo { get; set;}
        public string? partner_name {get; set;}
        [NotMapped]
        public IFormFile? logo { get; set; }
        public Guid category_id { get; set;}

        [ForeignKey("category_id")]
        [InverseProperty("Partners")]
        public virtual Category_partner category_partner { get; set; }
        public string? partner_detail {get; set;}
        public string? status {get; set;}

        
    }
}