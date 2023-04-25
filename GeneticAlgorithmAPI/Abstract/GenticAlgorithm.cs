using GeneticAlgorithmAPI.Entities;
using PeanutButter.RandomGenerators;
using PeanutButter.Utils;
using System.Runtime.CompilerServices;

namespace GeneticAlgorithmAPI.Abstract
{
    public abstract class GenticAlgorithm
    {

        protected readonly int numberOfJobs;

        protected readonly int numberOfMachines;

        protected readonly int minTimeOfExecutionOfJob;

        protected readonly int maxTimeOfExecutionOfJob;

        protected readonly int iteration;
        protected int maxSizeOfList { get; private set; }

        protected readonly Dictionary<int, List<Job>> listOfJobs;
        
        protected GenticAlgorithm (int _numberOfJobs, int _numberOfMachines, int _minTimeOfExecutionOfJob, int _maxTimeOfExecutionOfJob, int _iteration)
        {
            this.numberOfJobs = _numberOfJobs;
            this.numberOfMachines = _numberOfMachines;
            this.iteration = _iteration;
            this.minTimeOfExecutionOfJob = _minTimeOfExecutionOfJob;
            this.maxTimeOfExecutionOfJob = _maxTimeOfExecutionOfJob;
            this.listOfJobs = new Dictionary<int, List<Job>>(numberOfMachines);

        }
        protected virtual void CreateMachines()
        {
            for (int i = 0; i < numberOfMachines; i++)
            {
                listOfJobs.Add(i, new List<Job>());
            }
        }

        protected virtual void SetJobsInMachines(int minLength, int maxLength)
        {
            int tracker = 0;
            for (int i = 0; i < numberOfJobs; i++)
            {
                listOfJobs[tracker].Add(new Job(RandomValueGen.GetRandomInt(minLength, maxLength), i));
                if (listOfJobs[tracker].Count > maxSizeOfList) maxSizeOfList = listOfJobs[tracker].Count;
                tracker += 1;
                if (tracker == listOfJobs.Count) tracker = 0;
                
            }
        }

        public virtual int GetNumberOfMachines()
        {
            return listOfJobs.Count();
        }

        public virtual int GetNumberOfJobs()
        {
            return listOfJobs.Sum(x => x.Value.Count);
        }

        public List<int> CheckUniqueOfJobs()
        {
            List<int> list = new List<int>();
            //listOfJobs.ForEach(x => list.AddRange(x.Value.Select(q => q.myUniqueNumber)));
            foreach (var myItem in listOfJobs)
            {                
                list.AddRange(myItem.Value.Select(x=>x.myUniqueNumber).ToList());
            }
            list.Sort();
            return list;
        }


    }
}
