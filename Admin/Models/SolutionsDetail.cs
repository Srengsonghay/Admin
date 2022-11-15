using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models{
    public class SolutionDetail{
        [Key]
        public Guid id {get; set;}
        
        public Guid solution_id {get; set;}
        [ForeignKey("solution_id")]
        [InverseProperty("SolutionDetail")]
        public virtual Solutions solutions { get; set;}
        public string? remark { get; set;}
        
    }
}