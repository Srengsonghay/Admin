using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Models;
using static Admin.Models.Events;

namespace Admin.Data
{

    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options)
        {

        }

        //method convert handle all properties
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
            base.ConfigureConventions(builder);

        }
        public DbSet<Solutions>? Solutions { get; set; }
        public DbSet<SolutionsType>? SolutionsType { get; set; }
        public DbSet<SolutionDetail>? SolutionDetails { get; set; }
        public DbSet<AboutUs>? AboutUs { get; set; }
        public DbSet<Careers>? Careers { get; set; }
        public DbSet<Events>? Events { get; set; }
        public DbSet<Partners>? Partners { get; set; }
        public DbSet<Customers>? Customers { get; set; }
        public DbSet<Category_customer>? Category_Customers { get; set; }
        public DbSet<Category_partner>? Category_partners { get; set; }
    }
}