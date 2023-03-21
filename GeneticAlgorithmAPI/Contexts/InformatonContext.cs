using Microsoft.EntityFrameworkCore;
using GeneticAlgorithmAPI.Entities;

namespace GeneticAlgorithmAPI.Contexts
{
    public class InformatonContext: DbContext
    {
        public DbSet <Information> informationContext { get; set; }
    }
}
