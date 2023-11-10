﻿// <auto-generated />
using System;
using DeathAndTaxes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeathAndTaxes.Data.Migrations
{
    [DbContext(typeof(DeathAndTaxesDbContext))]
    partial class DeathAndTaxesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeathAndTaxes.Core.Models.FlatRateTax", b =>
                {
                    b.Property<int>("FlatRateTaxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlatRateTaxId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxPercentageRateId")
                        .HasColumnType("int");

                    b.HasKey("FlatRateTaxId");

                    b.HasIndex("TaxPercentageRateId");

                    b.ToTable("FlatRateTaxes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.FlatValueTax", b =>
                {
                    b.Property<int>("FlatValueTaxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlatValueTaxId"));

                    b.Property<double>("Base")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Months")
                        .HasColumnType("int");

                    b.Property<int>("TaxIncomeBracketId")
                        .HasColumnType("int");

                    b.Property<int>("TaxPercentageRateId")
                        .HasColumnType("int");

                    b.HasKey("FlatValueTaxId");

                    b.HasIndex("TaxIncomeBracketId");

                    b.HasIndex("TaxPercentageRateId");

                    b.ToTable("FlatValueTaxes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.PostalCode", b =>
                {
                    b.Property<int>("PostalCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostalCodeId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxCalculationTypeId")
                        .HasColumnType("int");

                    b.HasKey("PostalCodeId");

                    b.HasIndex("TaxCalculationTypeId");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.ProgressiveTax", b =>
                {
                    b.Property<int>("ProgressiveTaxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgressiveTaxId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxIncomeBracketId")
                        .HasColumnType("int");

                    b.Property<int>("TaxPercentageRateId")
                        .HasColumnType("int");

                    b.HasKey("ProgressiveTaxId");

                    b.HasIndex("TaxIncomeBracketId");

                    b.HasIndex("TaxPercentageRateId");

                    b.ToTable("ProgressiveTaxes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxCalculationType", b =>
                {
                    b.Property<int>("TaxCalculationTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxCalculationTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaxCalculationTypeId");

                    b.ToTable("TaxCalculationTypes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxIncomeBracket", b =>
                {
                    b.Property<int>("TaxIncomeBracketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxIncomeBracketId"));

                    b.Property<double>("FromIncomeBracket")
                        .HasColumnType("float");

                    b.Property<double>("ToIncomeBracket")
                        .HasColumnType("float");

                    b.HasKey("TaxIncomeBracketId");

                    b.ToTable("TaxIncomeBrackets");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxPercentageRate", b =>
                {
                    b.Property<int>("TaxPercentageRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxPercentageRateId"));

                    b.Property<double>("PercentageRate")
                        .HasColumnType("float");

                    b.HasKey("TaxPercentageRateId");

                    b.ToTable("TaxPercentageRates");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxScore", b =>
                {
                    b.Property<int>("TaxScoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaxScoreId"));

                    b.Property<DateTime>("DateCaptured")
                        .HasColumnType("datetime2");

                    b.Property<double>("Income")
                        .HasColumnType("float");

                    b.Property<int>("PostalCodeId")
                        .HasColumnType("int");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaxScoreId");

                    b.HasIndex("PostalCodeId");

                    b.ToTable("TaxScores");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.FlatRateTax", b =>
                {
                    b.HasOne("DeathAndTaxes.Core.Models.TaxPercentageRate", "TaxPercentageRate")
                        .WithMany("FlatRateTaxes")
                        .HasForeignKey("TaxPercentageRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxPercentageRate");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.FlatValueTax", b =>
                {
                    b.HasOne("DeathAndTaxes.Core.Models.TaxIncomeBracket", "IncomeBracket")
                        .WithMany("FlatValueTaxes")
                        .HasForeignKey("TaxIncomeBracketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeathAndTaxes.Core.Models.TaxPercentageRate", "TaxPercentageRate")
                        .WithMany("FlatValueTaxes")
                        .HasForeignKey("TaxPercentageRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IncomeBracket");

                    b.Navigation("TaxPercentageRate");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.PostalCode", b =>
                {
                    b.HasOne("DeathAndTaxes.Core.Models.TaxCalculationType", "TaxCalculationType")
                        .WithMany("PostalCodes")
                        .HasForeignKey("TaxCalculationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxCalculationType");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.ProgressiveTax", b =>
                {
                    b.HasOne("DeathAndTaxes.Core.Models.TaxIncomeBracket", "IncomeBracket")
                        .WithMany("ProgressiveTaxes")
                        .HasForeignKey("TaxIncomeBracketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeathAndTaxes.Core.Models.TaxPercentageRate", "TaxPercentageRate")
                        .WithMany("ProgressiveTaxes")
                        .HasForeignKey("TaxPercentageRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IncomeBracket");

                    b.Navigation("TaxPercentageRate");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxScore", b =>
                {
                    b.HasOne("DeathAndTaxes.Core.Models.PostalCode", "PostalCode")
                        .WithMany("TaxScores")
                        .HasForeignKey("PostalCodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostalCode");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.PostalCode", b =>
                {
                    b.Navigation("TaxScores");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxCalculationType", b =>
                {
                    b.Navigation("PostalCodes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxIncomeBracket", b =>
                {
                    b.Navigation("FlatValueTaxes");

                    b.Navigation("ProgressiveTaxes");
                });

            modelBuilder.Entity("DeathAndTaxes.Core.Models.TaxPercentageRate", b =>
                {
                    b.Navigation("FlatRateTaxes");

                    b.Navigation("FlatValueTaxes");

                    b.Navigation("ProgressiveTaxes");
                });
#pragma warning restore 612, 618
        }
    }
}
