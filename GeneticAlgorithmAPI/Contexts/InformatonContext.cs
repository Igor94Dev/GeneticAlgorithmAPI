using Microsoft.EntityFrameworkCore;
using GeneticAlgorithmAPI.Entities;

namespace GeneticAlgorithmAPI.Contexts
{

    public class TwoPointsCrossingWithMutationStrategyContext : DbContext
    {
        private const string connectionString = "Server=localhost\\SQLEXPRESS;Database=GeneticAlgorithmDb;Trusted_Connection=True;TrustServerCertificate=true";

        public DbSet<Information> GeneticAlgorithmData { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Information>().Property(x => x.minSizeBeforeStartStrategy).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.maxSizeBeforeStartStrategy).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.minSizeAfterStartStrategy).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.maxSizeAfterStartStrategy).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.totalNumbersOfMachines).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.totalNumbersOfJobs).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.strategy).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.percentageDifferenceBetweenMin).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.percentageDifferenceBetweenMax).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.numberOfIteration).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.minTimeOfExecutionOfJob).IsRequired();
            modelBuilder.Entity<Information>().Property(x => x.maxTimeOfExecutionOfJob).IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    
}
