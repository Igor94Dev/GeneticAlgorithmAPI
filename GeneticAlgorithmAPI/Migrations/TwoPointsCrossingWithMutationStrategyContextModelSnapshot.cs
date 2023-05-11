﻿// <auto-generated />
using GeneticAlgorithmAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GeneticAlgorithmAPI.Migrations
{
    [DbContext(typeof(TwoPointsCrossingWithMutationStrategyContext))]
    partial class TwoPointsCrossingWithMutationStrategyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GeneticAlgorithmAPI.Entities.Information", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("maxSizeAfterStartStrategy")
                        .HasColumnType("int");

                    b.Property<int>("maxSizeBeforeStartStrategy")
                        .HasColumnType("int");

                    b.Property<int>("maxTimeOfExecutionOfJob")
                        .HasColumnType("int");

                    b.Property<int>("minSizeAfterStartStrategy")
                        .HasColumnType("int");

                    b.Property<int>("minSizeBeforeStartStrategy")
                        .HasColumnType("int");

                    b.Property<int>("minTimeOfExecutionOfJob")
                        .HasColumnType("int");

                    b.Property<int>("numberOfIteration")
                        .HasColumnType("int");

                    b.Property<int>("percentageDifferenceBetweenMax")
                        .HasColumnType("int");

                    b.Property<int>("percentageDifferenceBetweenMin")
                        .HasColumnType("int");

                    b.Property<string>("strategy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("totalNumbersOfJobs")
                        .HasColumnType("int");

                    b.Property<int>("totalNumbersOfMachines")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GeneticAlgorithmData");
                });
#pragma warning restore 612, 618
        }
    }
}
