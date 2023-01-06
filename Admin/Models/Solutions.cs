using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Admin.Models{
    public class Solutions
    {
        [Key]
        public Guid id {get; set;}
        public string? solution_name {get; set;}

        public Guid solution_type_id { get; set; }
        [ForeignKey("solution_type_id")]
        [InverseProperty("Solutions")]
        public virtual SolutionsType solutions_type { get; set; }


        [InverseProperty(nameof(Admin.Models.SolutionsDetail.solutions))]
        public virtual ICollection<SolutionsDetail>? SolutionDetail { get; set; }

        public string? description { get; set; }

    }
}