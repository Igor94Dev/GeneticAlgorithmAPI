namespace GeneticAlgorithmAPI.Entities
{
    public class Job
    {
        public Job(int _totalTimeOfExecuteThisJob) 
        {
            totalTimeOfExecuteThisJob = _totalTimeOfExecuteThisJob;
        }

        public int totalTimeOfExecuteThisJob { get; set; }

        public int numberInQueue { get; set; }
    }
}
