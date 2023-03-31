using GeneticAlgorithmAPI.Abstract;
using GeneticAlgorithmAPI.Entities;
using GeneticAlgorithmAPI.Interfaces;

namespace GeneticAlgorithmAPI.Strategies
{
    public class TwoPointsCrossingWithMutationStrategy : GenticAlgorithm, IStrategy
    {
        public TwoPointsCrossingWithMutationStrategy(int _numberOfJobs, int numberOfMachines) : base(_numberOfJobs, numberOfMachines)
        {
        }

        public void RunMyStrategy()
        {
            
        }

      
    }
}
