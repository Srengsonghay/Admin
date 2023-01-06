using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models{
    public class Events{
        [Key]
        public Guid id {get; set;}
        public Byte[]? event_image { get; set; }
        public string? event_heading {get; set;}
        public string? event_detail {get; set;}
        public DateTime? event_date {get; set;}
        [NotMapped]
        public IFormFile? logo { get; set; }

        //[NotMapped]
        //public string slug => event_heading.Replace(' ', '-').ToLower().ToString();
        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter()
                : base(dateOnly =>
                        dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime))
            { }
        }
    }
}