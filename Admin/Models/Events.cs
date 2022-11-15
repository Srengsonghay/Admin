using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models{
    public class Events{
        [Key]
        public Guid id {get; set;}
        public string? event_heading {get; set;}
        public string? event_detail {get; set;}
        public DateTime? event_date {get; set;}

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