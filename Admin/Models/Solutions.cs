using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



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


        [InverseProperty(nameof(Admin.Models.SolutionDetail.solutions))]
        public virtual ICollection<SolutionDetail>? SolutionDetail { get; set; }
        public string? description { get; set; }

    }
}