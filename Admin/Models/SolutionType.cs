using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models{
    public class SolutionsType{
        [Key]
        public Guid id {get; set;}
        public string? type_name {get; set;}

        [InverseProperty(nameof(Admin.Models.Solutions.solutions_type))]
        public virtual ICollection<Solutions> Solutions { get; set; }
    }
}