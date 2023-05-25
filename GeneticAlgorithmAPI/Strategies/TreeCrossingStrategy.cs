using GeneticAlgorithmAPI.Abstract;
using GeneticAlgorithmAPI.Entities;
using GeneticAlgorithmAPI.Interfaces;
using PeanutButter.RandomGenerators;
using GeneticAlgorithmAPI.Strategies;
using GeneticAlgorithmAPI.Helper;

namespace GeneticAlgorithmAPI.Strategies
{
    public class TreeCrossingStrategy : GenticAlgorithm, IStrategy
    {
        private Information infoModel;
        private const int percentDifference = 15;

        public TreeCrossingStrategy(int _numberOfMachines, int _numberOfJobs, int _minTimeOfExecutionOfJob, int _maxTimeOfExecutionOfJob, int _iteration) : base(_numberOfMachines, _numberOfJobs, _minTimeOfExecutionOfJob, _maxTimeOfExecutionOfJob, _iteration)
        {
            infoModel = new Information();
            infoModel.totalNumbersOfJobs = _numberOfJobs;
            infoModel.totalNumbersOfMachines = _numberOfMachines;
            infoModel.minTimeOfExecutionOfJob = _minTimeOfExecutionOfJob;
            infoModel.maxTimeOfExecutionOfJob = _maxTimeOfExecutionOfJob;
            infoModel.numberOfIteration = _iteration;
        }

        public void RunMyStrategy()
        {
            MutationStrategy(iteration);
        }

        public void SetUpMyStrategy()
        {
            CreateMachines();
            SetJobsInMachines(minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob);
            SetTheMostMinAndMaxSizeOfMachine();
        }

        private void MutationStrategy(int iteration)
        {
            int randVal = 0;
            int randVal2 = 0;
            List<Job> SelectedJobsForMutation_1 = new List<Job>(maxSizeOfList);
            List<Job> SelectedJobsForMutation_2 = new List<Job>(maxSizeOfList);
            List<Job> SavedJobs_1 = new List<Job>(maxSizeOfList);
            List<Job> SavedJobs_2 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution1 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution2 = new List<Job>(maxSizeOfList);
            for (int i = 0; i < iteration; i++)
            {
                randVal = RandomValueGen.GetRandomInt(0, listOfJobs.Count-1);
                randVal2 = RandomValueGen.GetRandomInt(0, listOfJobs.Count-1);
                if(randVal == randVal2)
                {
                    randVal2 -= 1;
                    if (randVal2 < 0) randVal2 += 2;
                }
                SelectedJobsForMutation_1 = listOfJobs[randVal];
                SelectedJobsForMutation_2 = listOfJobs[randVal2];
                SavedJobs_1.AddRange(SelectedJobsForMutation_1);
                SavedJobs_2.AddRange(SelectedJobsForMutation_2);
                TreeSelector(SelectedJobsForMutation_1, SelectedJobsForMutation_2, potencialBetterSolution1, potencialBetterSolution2);
                ValidateAndDecideToSelectNewSolution(SelectedJobsForMutation_1, SelectedJobsForMutation_2, SavedJobs_1, SavedJobs_2, potencialBetterSolution1, potencialBetterSolution2);
            }
        }

        private void TreeSelector(List<Job> jobs1, List<Job> jobs2, List<Job> potencialBetterSolution1, List<Job> potencialBetterSolution2)
        {
            int totalExecutionTimeCounterMachine_1 = 0;
            int totalExecutionTimeCounterMachine_2 = 0;
            int randomSelector = 0;
            while (true)
            {
                if(jobs2.Count > 0)
                {
                    randomSelector = RandomValueGen.GetRandomInt(0, jobs2.Count-1);
                    if (totalExecutionTimeCounterMachine_1 < totalExecutionTimeCounterMachine_2 || totalExecutionTimeCounterMachine_1 == totalExecutionTimeCounterMachine_2)
                    {
                        totalExecutionTimeCounterMachine_1 += jobs2[randomSelector].totalTimeOfExecuteThisJob;
                        potencialBetterSolution1.Add(jobs2[randomSelector]);
                    }
                    else
                    {
                        potencialBetterSolution2.Add(jobs2[randomSelector]);
                        totalExecutionTimeCounterMachine_2 += jobs2[randomSelector].totalTimeOfExecuteThisJob;
                    }
                    jobs2.Remove(jobs2[randomSelector]);
                }
                
                if(jobs1.Count > 0)
                {
                    randomSelector = RandomValueGen.GetRandomInt(0, jobs1.Count-1);
                    if (totalExecutionTimeCounterMachine_2 < totalExecutionTimeCounterMachine_1 || totalExecutionTimeCounterMachine_1 == totalExecutionTimeCounterMachine_2)
                    {
                        totalExecutionTimeCounterMachine_2 += jobs1[randomSelector].totalTimeOfExecuteThisJob;
                        potencialBetterSolution2.Add(jobs1[randomSelector]);
                    }
                    else
                    {
                        potencialBetterSolution1.Add(jobs1[randomSelector]);
                        totalExecutionTimeCounterMachine_1 += jobs1[randomSelector].totalTimeOfExecuteThisJob;
                    }
                    jobs1.Remove(jobs1[randomSelector]);
                }
                if (jobs1.Count == 0 && jobs2.Count == 0) break;
            }
        }

        private void ValidateAndDecideToSelectNewSolution(List<Job> selectedMachineToChange_1, List<Job> selectedMachineToChange_2, List<Job> savedMachineToChange_1, List<Job> savedMachineToChange_2, List<Job> potencialBetterSolution_1, List<Job> potencialBetterSolution_2)
        {
            int jobs1TotalExecutionTimeCounter = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(savedMachineToChange_1);
            int jobs2TotalExecutionTimeCounter = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(savedMachineToChange_2);

            int changedJobs1ExecutionTime = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialBetterSolution_1);
            int changedJobs2ExecutionTime = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialBetterSolution_2);
            double percentDifferenceBetweenMachine_1 = 100;
            double percentDifferenceBetweenMachine_2 = 100;

            if (jobs1TotalExecutionTimeCounter < changedJobs1ExecutionTime)
            {
                double d = (float)jobs1TotalExecutionTimeCounter / (float)(changedJobs1ExecutionTime);
                percentDifferenceBetweenMachine_1 = Math.Round(d,2);
            }

            if(jobs2TotalExecutionTimeCounter < changedJobs2ExecutionTime)
            {
                double d = (float)(jobs2TotalExecutionTimeCounter) / (float)(changedJobs2ExecutionTime);
                percentDifferenceBetweenMachine_2 = Math.Round(d,2);
            }

            if ((jobs1TotalExecutionTimeCounter > changedJobs1ExecutionTime) && (jobs2TotalExecutionTimeCounter > changedJobs2ExecutionTime)
                || (percentDifferenceBetweenMachine_1 < percentDifference || percentDifferenceBetweenMachine_2 < percentDifference))
            {             
                selectedMachineToChange_1.AddRange(potencialBetterSolution_1);
                selectedMachineToChange_2.AddRange(potencialBetterSolution_2);
            }
            else
            {
                selectedMachineToChange_1.AddRange(savedMachineToChange_1);
                selectedMachineToChange_2.AddRange(savedMachineToChange_2);
            }
            savedMachineToChange_1.Clear();
            savedMachineToChange_2.Clear();
            potencialBetterSolution_1.Clear();
            potencialBetterSolution_2.Clear();          
        }
    }
}
