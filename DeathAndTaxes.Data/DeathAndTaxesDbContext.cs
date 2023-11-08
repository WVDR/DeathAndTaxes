using DeathAndTaxes.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Data
{
    public class YourDbContext : DbContext
    {
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define a one-to-one relationship between PostalCode and TaxCalculationType
            modelBuilder.Entity<PostalCode>()
                .HasOne(pc => pc.TaxCalculationType)
                .WithMany()
                .HasForeignKey(pc => pc.TaxCalculationTypeId);

            modelBuilder.Entity<TaxCalculationType>()
                .HasMany(tct => tct.PostalCodes)
                .WithOne(pc => pc.TaxCalculationType)
                .HasForeignKey(pc => pc.TaxCalculationTypeId);

            // Seed data TODO: change to manual seed this leads to an issue where these values can not chnage further down the line.
            //modelBuilder.Entity<TaxCalculationType>().HasData(
            //    new TaxCalculationType { TaxCalculationTypeId = 1, Name = "Progressive" },
            //    new TaxCalculationType { TaxCalculationTypeId = 2, Name = "Flat Value" },
            //    new TaxCalculationType { TaxCalculationTypeId = 3, Name = "Flat Rate" }
            //);

            //modelBuilder.Entity<PostalCode>().HasData(
            //    new PostalCode { PostalCodeId = 1, Code = "7441", TaxCalculationTypeId = 1 },
            //    new PostalCode { PostalCodeId = 2, Code = "A100", TaxCalculationTypeId = 2 },
            //    new PostalCode { PostalCodeId = 3, Code = "7000", TaxCalculationTypeId = 3 },
            //    new PostalCode { PostalCodeId = 4, Code = "1000", TaxCalculationTypeId = 1 }
            //);
        }
    }

}
