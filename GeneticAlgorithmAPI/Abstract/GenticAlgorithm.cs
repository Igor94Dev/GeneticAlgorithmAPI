using GeneticAlgorithmAPI.Entities;
using GeneticAlgorithmAPI.Interfaces;

namespace GeneticAlgorithmAPI.Abstract
{
    public abstract class GenticAlgorithm
    {

        protected readonly int numberOfJobs;

        protected readonly int numberOfMachines;

        protected readonly Dictionary<int, List<Job>> listOfJobs;

        protected GenticAlgorithm (int _numberOfJobs, int numberOfMachines)
        {
            this.numberOfJobs = _numberOfJobs;
            this.numberOfMachines = numberOfMachines;
            this.listOfJobs = new Dictionary<int, List<Job>>();
        }


        protected GenticAlgorithm SetJobsInMachines(int minLength, int maxLength)
        {
            return this;
        }

        public virtual GenticAlgorithm CreateMachines(int numberOfMachines)
        {
            return this;
        }

      
        public virtual GenticAlgorithm SetEndStrategy(int numbersOfIteration)
        {
            return this;
        }

    }
}
