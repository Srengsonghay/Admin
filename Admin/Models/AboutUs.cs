using System.ComponentModel.DataAnnotations;

namespace Admin.Models{
    public class AboutUs{
        [Key]
        public Guid id {get; set;}
        public string? about_heading {get; set;}
        public string? about_detail {get; set;}
    }
}