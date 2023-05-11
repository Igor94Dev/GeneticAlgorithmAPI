using GeneticAlgorithmAPI.Entities;

namespace GeneticAlgorithmAPI.Helper
{
    public static class GeneticAlgorithmHelper
    {
        public static int CountTotalExecutionTimeInOfAllJobsInMachine(List<Job> jobs)
        {
            int count = jobs.Sum(x => x.totalTimeOfExecuteThisJob);
            return count;
        }
    }
}
