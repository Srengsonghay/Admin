using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models
{
    public class JobApplication
    {
        [Key]
        public Guid id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public string? email { get; set; }
        public string? contact { get; set; }
        public Guid career_id { get; set; }

        [ForeignKey("career_id")]
        [InverseProperty("JobApplication")]
        public virtual Careers? Career { get; set; }
        public float experience { get; set; }
        public string? education { get; set; }
        public string? skill { get; set; }
        //public float year { get; set; }
        public string? comment { get; set; }
        public string? referral_name { get; set; }
        public Byte[]? file { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }
        public bool? agree { get; set; }
        public DateTime send_date { get; set; }
    }
}
