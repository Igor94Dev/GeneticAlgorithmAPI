using GeneticAlgorithmAPI.Abstract;
using GeneticAlgorithmAPI.Entities;
using GeneticAlgorithmAPI.Interfaces;
using PeanutButter.RandomGenerators;
using System.Runtime.CompilerServices;

namespace GeneticAlgorithmAPI.Strategies
{
    public class TwoPointsCrossingWithMutationStrategy : GenticAlgorithm, IStrategy
    {
        private Information infoModel;

        public TwoPointsCrossingWithMutationStrategy(int _numberOfMachines, int _numberOfJobs, int _minTimeOfExecutionOfJob, int _maxTimeOfExecutionOfJob,  int _iteration) : base(_numberOfJobs, _numberOfMachines, _minTimeOfExecutionOfJob, _maxTimeOfExecutionOfJob, _iteration)
        {
            infoModel = new Information();
            infoModel.totalNumbersOfJobs = _numberOfJobs;
            infoModel.totalNumbersOfMachines = _numberOfMachines;
            infoModel.numberOfIteration= _iteration;
        }

        public void RunMyStrategy()
        {          
            MutationStrategy(iteration);          
        }

        public void SetUpMyStrategy()
        {
            CreateMachines();
            SetJobsInMachines(minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob);
        }

        private void MutationStrategy(int iteration)
        {
            int jobsToTake1 = 0;
            int jobsToTake2 = 0;
            int randVal = 0;
            int randVal2 = 0;
            int tracker = 0;
            List<Job> SelectedJobsForMutation_1 = new List<Job>(maxSizeOfList);
            List<Job> SelectedJobsForMutation_2 = new List<Job>(maxSizeOfList);
            List<Job> halfJobsToMutate1 = new List<Job>(maxSizeOfList);
            List<Job> halfJobsToMutate2 = new List<Job>(maxSizeOfList);
            Job jobToMove = null;
            Job jobToMove_2 = null;
            for (int i = 0; i < iteration; i++)
            {              
                SelectedJobsForMutation_1 = listOfJobs[tracker];
                SelectedJobsForMutation_2 = listOfJobs[((tracker + 1) == listOfJobs.Count) ? 0 : (tracker + 1)];
                jobsToTake1 = SelectedJobsForMutation_1.Count % 2 == 0 ? SelectedJobsForMutation_1.Count / 2 : (SelectedJobsForMutation_1.Count + 1) / 2;
                jobsToTake2 = SelectedJobsForMutation_2.Count % 2 == 0 ? SelectedJobsForMutation_2.Count / 2 : (SelectedJobsForMutation_2.Count + 1) / 2;
                halfJobsToMutate1 = SelectedJobsForMutation_1.Take(jobsToTake1).ToList();
                halfJobsToMutate2 = SelectedJobsForMutation_2.TakeLast(jobsToTake2).ToList();
                SelectedJobsForMutation_1.RemoveRange(0, jobsToTake1);
                SelectedJobsForMutation_2.RemoveRange(SelectedJobsForMutation_2.Count - jobsToTake2, jobsToTake2);
                SelectedJobsForMutation_1.AddRange(halfJobsToMutate2);
                SelectedJobsForMutation_2.AddRange(halfJobsToMutate1);               
                randVal = RandomValueGen.GetRandomInt(0, SelectedJobsForMutation_2.Count - 1);
                jobToMove = SelectedJobsForMutation_2[randVal];
                randVal2 = RandomValueGen.GetRandomInt(0, SelectedJobsForMutation_2.Count - 1);
                jobToMove_2 = SelectedJobsForMutation_2[randVal2];
                SelectedJobsForMutation_2[randVal] = jobToMove_2;
                SelectedJobsForMutation_2[randVal2] = jobToMove;
                jobToMove = null; jobToMove_2 = null; SelectedJobsForMutation_1 = null;SelectedJobsForMutation_2 = null;halfJobsToMutate1 = null; halfJobsToMutate2 = null;
            }
        }
    }
}
