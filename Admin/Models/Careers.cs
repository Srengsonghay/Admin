using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System.ComponentModel.DataAnnotations;

namespace Admin.Models{
    public class Careers{
        [Key]
        public Guid id {get; set;}
        public string? career_name {get; set;}
        public string? career_detail {get; set;}
        public DateTime? career_date{get; set;}
        //public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        //{
        //    public DateOnlyConverter()
        //        : base(dateOnly =>
        //                dateOnly.ToDateTime(TimeOnly.MinValue),
        //            dateTime => DateOnly.FromDateTime(dateTime))
        //    { }
        //}
    }
}