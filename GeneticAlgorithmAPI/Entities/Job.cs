namespace GeneticAlgorithmAPI.Entities
{
    public class Job
    {
        public Job(int _totalTimeOfExecuteThisJob, int _myUniqueNumber) 
        {
            totalTimeOfExecuteThisJob = _totalTimeOfExecuteThisJob;
            myUniqueNumber = _myUniqueNumber;
        }

        public int totalTimeOfExecuteThisJob { get; set; }

        public readonly int myUniqueNumber;
    }

    public struct Job2
    {
        public Job2(int _totalTimeOfExecuteThisJob)
        {
            totalTimeOfExecuteThisJob = _totalTimeOfExecuteThisJob;
        }

        public int totalTimeOfExecuteThisJob { get; set; }

        public int numberInQueue { get; set; }
    }
}
