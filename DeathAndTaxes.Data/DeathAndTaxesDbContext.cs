using DeathAndTaxes.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Data
{
    public class DeathAndTaxesDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DeathAndTaxesDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=DeathAndTaxes;User id=DeathAndTaxes;Password=123;TrustServerCertificate=True;");
        }

        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<TaxCalculationType> TaxCalculationTypes { get; set; }
        public DbSet<ProgressiveTax> ProgressiveTaxes { get; set; }
        public DbSet<FlatValueTax> FlatValueTaxes { get; set; }
        public DbSet<FlatRateTax> FlatRateTaxes { get; set; }
        public DbSet<TaxPercentageRate> TaxPercentageRates { get; set; }
        public DbSet<TaxIncomeBracket> TaxIncomeBrackets { get; set; }        
        public DbSet<TaxScore> TaxScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //PK

            modelBuilder.Entity<PostalCode>()
                .HasKey(p => p.PostalCodeId);

            modelBuilder.Entity<TaxCalculationType>()
                .HasKey(tct => tct.TaxCalculationTypeId);

            modelBuilder.Entity<ProgressiveTax>()
                .HasKey(pt => pt.ProgressiveTaxId);

            modelBuilder.Entity<FlatValueTax>()
                .HasKey(fvt => fvt.FlatValueTaxId);

            modelBuilder.Entity<FlatRateTax>()
                .HasKey(frt => frt.FlatRateTaxId);

            modelBuilder.Entity<TaxPercentageRate>()
                .HasKey(tpr => tpr.TaxPercentageRateId);

            modelBuilder.Entity<TaxIncomeBracket>()
                .HasKey(tib => tib.TaxIncomeBracketId);

            modelBuilder.Entity<TaxScore>()
                .HasKey(ts => ts.TaxScoreId);

            //FK

            //PostalCode
            modelBuilder.Entity<PostalCode>()
                .HasOne(pc => pc.TaxCalculationType)
                .WithMany()
                .HasForeignKey(pc => pc.TaxCalculationTypeId);

            modelBuilder.Entity<TaxCalculationType>()
                .HasMany(tct => tct.PostalCodes)
                .WithOne(pc => pc.TaxCalculationType)
                .HasForeignKey(pc => pc.TaxCalculationTypeId);

            //ProgressiveTax
            modelBuilder.Entity<ProgressiveTax>()
                .HasOne(pt => pt.TaxPercentageRate)
                .WithMany()
                .HasForeignKey(pt => pt.TaxPercentageRateId);

            modelBuilder.Entity<TaxPercentageRate>()
                .HasMany(tpr => tpr.ProgressiveTaxes)
                .WithOne(pt => pt.TaxPercentageRate)
                .HasForeignKey(pt => pt.TaxPercentageRateId);

            modelBuilder.Entity<ProgressiveTax>()
                .HasOne(pt => pt.IncomeBracket)
                .WithMany()
                .HasForeignKey(pt => pt.TaxIncomeBracketId);

            modelBuilder.Entity<TaxIncomeBracket>()
                .HasMany(tib => tib.ProgressiveTaxes)
                .WithOne(pt => pt.IncomeBracket)
                .HasForeignKey(pt => pt.TaxIncomeBracketId);

            //Flat Value tax
            modelBuilder.Entity<FlatValueTax>()
                .HasOne(fvt => fvt.TaxPercentageRate)
                .WithMany()
                .HasForeignKey(fvt => fvt.TaxPercentageRateId);

            modelBuilder.Entity<TaxPercentageRate>()
                .HasMany(tpr => tpr.FlatValueTaxes)
                .WithOne(fvt => fvt.TaxPercentageRate)
                .HasForeignKey(fvt => fvt.TaxPercentageRateId);

            modelBuilder.Entity<FlatValueTax>()
                .HasOne(pc => pc.IncomeBracket)
                .WithMany()
                .HasForeignKey(pc => pc.TaxIncomeBracketId);

            modelBuilder.Entity<TaxIncomeBracket>()
                .HasMany(tib => tib.FlatValueTaxes)
                .WithOne(fvt => fvt.IncomeBracket)
                .HasForeignKey(fvt => fvt.TaxIncomeBracketId);

            //Flat Rate tax
            modelBuilder.Entity<FlatRateTax>()
                .HasOne(frt => frt.TaxPercentageRate)
                .WithMany()
                .HasForeignKey(frt => frt.TaxPercentageRateId);

            modelBuilder.Entity<TaxPercentageRate>()
                .HasMany(tpr => tpr.FlatRateTaxes)
                .WithOne(frt => frt.TaxPercentageRate)
                .HasForeignKey(frt => frt.TaxPercentageRateId);

            //TaxScore
            modelBuilder.Entity<TaxScore>()
                .HasOne(ts => ts.PostalCode)
                .WithMany()
                .HasForeignKey(ts => ts.PostalCodeId);

            modelBuilder.Entity<PostalCode>()
                .HasMany(pc => pc.TaxScores)
                .WithOne(ts => ts.PostalCode)
                .HasForeignKey(ts => ts.PostalCodeId);
        }
    }

}
