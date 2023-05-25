using GeneticAlgorithmAPI.Abstract;
using GeneticAlgorithmAPI.Entities;
using GeneticAlgorithmAPI.Helper;
using GeneticAlgorithmAPI.Interfaces;
using PeanutButter.RandomGenerators;
using System.Diagnostics.Metrics;

namespace GeneticAlgorithmAPI.Strategies
{
    public class MutationWithTournamentSelectionStrategy : GenticAlgorithm, IStrategy
    {
        private Information infoModel;
        public MutationWithTournamentSelectionStrategy(int _numberOfJobs, int _numberOfMachines, int _minTimeOfExecutionOfJob, int _maxTimeOfExecutionOfJob, int _iteration) : base(_numberOfJobs, _numberOfMachines, _minTimeOfExecutionOfJob, _maxTimeOfExecutionOfJob, _iteration)
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
            int randVal1 = 0;
            int randVal2 = 0;
            int randVal3 = 0;
            List<Job> SavedJobs_Origin_Reference = new List<Job>(maxSizeOfList);
            List<Job> SelectedJobsForMutation_1_REFERENCE = new List<Job>(maxSizeOfList);
            List<Job> SelectedJobsForMutation_2_REFERENCE = new List<Job>(maxSizeOfList);
            List<Job> SavedJobs_Origin_COPY = new List<Job>(maxSizeOfList);
            List<Job> JobsForMutation_1_COPY = new List<Job>(maxSizeOfList);
            List<Job> JobsForMutation_2_COPY = new List<Job>(maxSizeOfList);         
            for (int i = 0; i < iteration; i++)
            {
                randVal1 = RandomValueGen.GetRandomInt(0, listOfJobs.Count - 1);
                randVal2 = randVal1 + 1;
                randVal3 = randVal1 - 1;
                if (randVal2 == listOfJobs.Count)
                {
                   randVal2 = 0;
                }
                if(randVal3 < 0)
                {
                   randVal3 = listOfJobs.Count-1;
                }
                SavedJobs_Origin_COPY.AddRange(listOfJobs[randVal1]);
                JobsForMutation_1_COPY.AddRange(listOfJobs[randVal2]);
                JobsForMutation_2_COPY.AddRange(listOfJobs[randVal3]);
                SavedJobs_Origin_Reference = listOfJobs[randVal1];
                SelectedJobsForMutation_1_REFERENCE = listOfJobs[randVal2];
                SelectedJobsForMutation_2_REFERENCE = listOfJobs[randVal3];
                MutationCalculation(JobsForMutation_1_COPY, JobsForMutation_2_COPY);            
                ValidateAndDecideToSelectNewSolution(SavedJobs_Origin_COPY, JobsForMutation_1_COPY, JobsForMutation_2_COPY, SelectedJobsForMutation_1_REFERENCE, SelectedJobsForMutation_2_REFERENCE, SavedJobs_Origin_Reference);                             
            }
        }

        private void MutationCalculation(List<Job> potencialBetterSolution_1, List<Job> potencialBetterSolution_2)
        {           
            int size = potencialBetterSolution_1.Count + potencialBetterSolution_2.Count;

            int numberOfJobsToByPassedFromSolution_1 = (int)(0.3 * potencialBetterSolution_1.Count);
            int numberOfJobsToTakeFromSolution_2 = (int)(0.5 * potencialBetterSolution_2.Count);
            int numberOfJobsToByPassedFromSolution_2 = (int)(0.3 * potencialBetterSolution_2.Count);

            var mutationForFirstSolution = potencialBetterSolution_1.Take(RandomValueGen.GetRandomInt(0, numberOfJobsToByPassedFromSolution_1)).ToList();
            var mutationForSecondSolution = potencialBetterSolution_2.Take(RandomValueGen.GetRandomInt(0, numberOfJobsToByPassedFromSolution_2)).ToList();
            potencialBetterSolution_1.RemoveRange(0, mutationForFirstSolution.Count);
            potencialBetterSolution_2.RemoveRange(0, mutationForSecondSolution.Count);
            potencialBetterSolution_1.InsertRange(potencialBetterSolution_1.Count % 2 == 0? potencialBetterSolution_1.Count / 2 : (potencialBetterSolution_1.Count - 1)/2, mutationForFirstSolution);
            potencialBetterSolution_2.InsertRange(potencialBetterSolution_2.Count % 2 == 0? potencialBetterSolution_2.Count / 2 : (potencialBetterSolution_2.Count - 1)/2, mutationForSecondSolution);
            mutationForFirstSolution.Clear();
            mutationForSecondSolution.Clear();

            mutationForFirstSolution.InsertRange(0, potencialBetterSolution_1.Take(numberOfJobsToByPassedFromSolution_1));
            potencialBetterSolution_1.RemoveRange(0, numberOfJobsToByPassedFromSolution_1);
            mutationForFirstSolution.InsertRange(mutationForFirstSolution.Count, potencialBetterSolution_1.TakeLast(numberOfJobsToByPassedFromSolution_1));
            potencialBetterSolution_1.RemoveRange(potencialBetterSolution_1.Count - numberOfJobsToByPassedFromSolution_1, numberOfJobsToByPassedFromSolution_1);

            mutationForSecondSolution.InsertRange(0, potencialBetterSolution_2.Skip(numberOfJobsToByPassedFromSolution_2).Take(numberOfJobsToTakeFromSolution_2));
            potencialBetterSolution_2.RemoveRange(numberOfJobsToByPassedFromSolution_2, numberOfJobsToTakeFromSolution_2);

            potencialBetterSolution_1.AddRange(mutationForSecondSolution);
            potencialBetterSolution_2.AddRange(mutationForFirstSolution.Take(numberOfJobsToByPassedFromSolution_1));
            potencialBetterSolution_2.AddRange(mutationForFirstSolution.TakeLast(numberOfJobsToByPassedFromSolution_1));
            mutationForFirstSolution.Clear();
            mutationForSecondSolution.Clear();   

        }

        private void ValidateAndDecideToSelectNewSolution(List<Job> originMachineSelectedForPotencialChange_COPY, List<Job> potencialBetterSolution_1_COPY, List<Job> potencialBetterSolution_2_COPY, List<Job> savedReferenceToSelectedJobs_1_REFERENCE, List<Job> savedReferenceToSelectedJobs_2_REFERENCE, List<Job> savedReferenceToOriginJobs_REFERENCE)
        {
            int countSolution_1 = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialBetterSolution_1_COPY);
            int countSolution_2 = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialBetterSolution_2_COPY);
            int countSolutionForOriginList = GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(originMachineSelectedForPotencialChange_COPY);
            int numberOfJobsToTake_OriginSolution = (int)(0.3 * originMachineSelectedForPotencialChange_COPY.Count);
            List<Job> myJobs = new List<Job>(originMachineSelectedForPotencialChange_COPY.Count);

            if (countSolutionForOriginList > countSolution_1 || countSolutionForOriginList > countSolution_2)
            {
                if(countSolution_1 < countSolution_2)
                {
                    myJobs.AddRange(originMachineSelectedForPotencialChange_COPY.Take(numberOfJobsToTake_OriginSolution));
                    myJobs.AddRange(originMachineSelectedForPotencialChange_COPY.TakeLast(numberOfJobsToTake_OriginSolution));
                    originMachineSelectedForPotencialChange_COPY.RemoveRange(0, numberOfJobsToTake_OriginSolution);
                    originMachineSelectedForPotencialChange_COPY.RemoveRange(originMachineSelectedForPotencialChange_COPY.Count - numberOfJobsToTake_OriginSolution, numberOfJobsToTake_OriginSolution);
                    int numbersOfJobsToSkip = (int)(0.3 * potencialBetterSolution_1_COPY.Count);
                    int takeMiddleOfJobs = (int)(0.4 * potencialBetterSolution_1_COPY.Count);
                    originMachineSelectedForPotencialChange_COPY.AddRange(potencialBetterSolution_1_COPY.Skip(numbersOfJobsToSkip).Take(takeMiddleOfJobs));
                    potencialBetterSolution_1_COPY.RemoveRange(numbersOfJobsToSkip, takeMiddleOfJobs);
                    potencialBetterSolution_1_COPY.AddRange(myJobs);
                }
                else
                {
                    myJobs.AddRange(originMachineSelectedForPotencialChange_COPY.Take(numberOfJobsToTake_OriginSolution));
                    myJobs.AddRange(originMachineSelectedForPotencialChange_COPY.TakeLast(numberOfJobsToTake_OriginSolution));
                    originMachineSelectedForPotencialChange_COPY.RemoveRange(0, numberOfJobsToTake_OriginSolution);
                    originMachineSelectedForPotencialChange_COPY.RemoveRange(originMachineSelectedForPotencialChange_COPY.Count - numberOfJobsToTake_OriginSolution, numberOfJobsToTake_OriginSolution);
                    int numbersOfJobsToSkip = (int)(0.3 * potencialBetterSolution_2_COPY.Count);
                    int takeMiddleOfJobs = (int)(0.4 * potencialBetterSolution_2_COPY.Count);
                    originMachineSelectedForPotencialChange_COPY.AddRange(potencialBetterSolution_2_COPY.Skip(numbersOfJobsToSkip).Take(takeMiddleOfJobs));
                    potencialBetterSolution_2_COPY.RemoveRange(numbersOfJobsToSkip, takeMiddleOfJobs);
                    potencialBetterSolution_2_COPY.AddRange(myJobs);           
                }
                savedReferenceToOriginJobs_REFERENCE.Clear();
                savedReferenceToOriginJobs_REFERENCE.AddRange(originMachineSelectedForPotencialChange_COPY);
            }
           
            myJobs.Clear();            
            savedReferenceToSelectedJobs_1_REFERENCE.Clear();
            savedReferenceToSelectedJobs_2_REFERENCE.Clear();
            savedReferenceToSelectedJobs_1_REFERENCE.AddRange(potencialBetterSolution_1_COPY);
            savedReferenceToSelectedJobs_2_REFERENCE.AddRange(potencialBetterSolution_2_COPY);
            potencialBetterSolution_1_COPY.Clear(); 
            potencialBetterSolution_2_COPY.Clear(); 
            originMachineSelectedForPotencialChange_COPY.Clear();
        }
    }
}
