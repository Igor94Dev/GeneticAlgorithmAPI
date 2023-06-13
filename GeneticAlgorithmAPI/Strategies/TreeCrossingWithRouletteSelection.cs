using GeneticAlgorithmAPI.Abstract;
using GeneticAlgorithmAPI.Entities;
using GeneticAlgorithmAPI.Helper;
using GeneticAlgorithmAPI.Interfaces;
using PeanutButter.RandomGenerators;

namespace GeneticAlgorithmAPI.Strategies
{
    public class TreeCrossingWithRouletteSelection : GenticAlgorithm, IStrategy
    {
        private Information infoModel;

        public TreeCrossingWithRouletteSelection(int _numberOfJobs, int _numberOfMachines, int _minTimeOfExecutionOfJob, int _maxTimeOfExecutionOfJob, int _iteration) : base(_numberOfJobs, _numberOfMachines, _minTimeOfExecutionOfJob, _maxTimeOfExecutionOfJob, _iteration)
        {
            infoModel = new Information();
            infoModel.totalNumbersOfJobs = _numberOfJobs;
            infoModel.totalNumbersOfMachines = _numberOfMachines;
            infoModel.minTimeOfExecutionOfJob = _minTimeOfExecutionOfJob;
            infoModel.maxTimeOfExecutionOfJob = _maxTimeOfExecutionOfJob;
            infoModel.numberOfIteration = _iteration;
        }

        private void SelectMachinesToChange(int rolledNumberOfMachine, List<Job> originJobToChange, ref int referenceMainSolution, List<Job> potencialBetterSolution1, ref int referencePositionInListSolution1, List<Job> potencialBetterSolution2, ref int referencePositionInListSolution2, List<Job> potencialBetterSolution3,
            ref int referencePositionInListSolution3, List<Job> potencialBetterSolution4, ref int referencePositionInListSolution4, List<Job> potencialBetterSolution5, ref int referencePositionInListSolution5, List<Job> potencialBetterSolution6, ref int referencePositionInListSolution6)
        {
            List<int> referenceNumbersInList = new List<int>(numberOfMachines);
            originJobToChange.AddRange(listOfJobs[rolledNumberOfMachine]);               
            referenceMainSolution = rolledNumberOfMachine;
            listOfJobs[rolledNumberOfMachine].Clear();
            referenceNumbersInList.Add(referenceMainSolution);
            int selector = 0;            
            while(true)
            {
                rolledNumberOfMachine += 1;
                if (rolledNumberOfMachine >= listOfJobs.Count)
                {
                    rolledNumberOfMachine = 0;
                }
                if(!referenceNumbersInList.Contains(rolledNumberOfMachine))               
                {                   
                    if (selector == 0)
                    {
                        potencialBetterSolution1.AddRange(listOfJobs[rolledNumberOfMachine]);
                        referencePositionInListSolution1 = rolledNumberOfMachine;
                        listOfJobs[rolledNumberOfMachine].Clear();
                    }                       
                    else if (selector == 1)
                    {
                        potencialBetterSolution2.AddRange(listOfJobs[rolledNumberOfMachine]);
                        referencePositionInListSolution2 = rolledNumberOfMachine;
                        listOfJobs[rolledNumberOfMachine].Clear();
                    }                      
                    else if (selector == 2)
                    {
                        potencialBetterSolution3.AddRange(listOfJobs[rolledNumberOfMachine]);
                        referencePositionInListSolution3 = rolledNumberOfMachine;
                        listOfJobs[rolledNumberOfMachine].Clear();
                    }
                    else if (selector == 3)
                    {
                        potencialBetterSolution4.AddRange(listOfJobs[rolledNumberOfMachine]);
                        referencePositionInListSolution4 = rolledNumberOfMachine;
                        listOfJobs[rolledNumberOfMachine].Clear();
                    }                      
                    else if (selector == 4)
                    {
                        potencialBetterSolution5.AddRange(listOfJobs[rolledNumberOfMachine]);
                        referencePositionInListSolution5 = rolledNumberOfMachine;
                        listOfJobs[rolledNumberOfMachine].Clear();
                    }                   
                    else if (selector == 5)
                    {
                        potencialBetterSolution6.AddRange(listOfJobs[rolledNumberOfMachine]);
                        referencePositionInListSolution6 = rolledNumberOfMachine;
                        listOfJobs[rolledNumberOfMachine].Clear();
                    }                       
                    else break;
                    selector += 1;
                    referenceNumbersInList.Add(rolledNumberOfMachine);
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        private void MutationStrategy(int iteration)
        {
            int randVal = 0;
            List<Job> SavedJobsToSwitch = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution1 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution2 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution3 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution4 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution5 = new List<Job>(maxSizeOfList);
            List<Job> potencialBetterSolution6 = new List<Job>(maxSizeOfList);

            int referenceMainSolution = 0;
            int referenceSolution_1 = 0;
            int referenceSolution_2 = 0;
            int referenceSolution_3 = 0;
            int referenceSolution_4 = 0;
            int referenceSolution_5 = 0;
            int referenceSolution_6 = 0;

            List<Job> container1 = new List<Job>();
            List<Job> container2 = new List<Job>();

            for (int i = 0; i < iteration; i++)
            {
                
                randVal = RandomValueGen.GetRandomInt(0, listOfJobs.Count - 1);
                SelectMachinesToChange(randVal, SavedJobsToSwitch, ref referenceMainSolution, potencialBetterSolution1, ref referenceSolution_1, potencialBetterSolution2, ref referenceSolution_2, potencialBetterSolution3, ref referenceSolution_3, potencialBetterSolution4, ref referenceSolution_4, potencialBetterSolution5, ref referenceSolution_5, potencialBetterSolution6, ref referenceSolution_6);
                TreeSelectBetweenTwoMachines(potencialBetterSolution1,potencialBetterSolution6, container1, container2);
                TreeSelectBetweenTwoMachines(potencialBetterSolution2, potencialBetterSolution5, container1, container2);
                TreeSelectBetweenTwoMachines(potencialBetterSolution3, potencialBetterSolution4, container1, container2);
                ValidateAndDecideToSelectNewSolution(SavedJobsToSwitch, potencialBetterSolution1, potencialBetterSolution2, potencialBetterSolution3, potencialBetterSolution4, potencialBetterSolution5, potencialBetterSolution6, container1, container2);
                AssignMachinesToReference(SavedJobsToSwitch, referenceMainSolution, potencialBetterSolution1, referenceSolution_1, potencialBetterSolution2, referenceSolution_2, potencialBetterSolution3, referenceSolution_3, 
                    potencialBetterSolution4, referenceSolution_4, potencialBetterSolution5, referenceSolution_5, potencialBetterSolution6, referenceSolution_6);
               
            }
        }

        private void ValidateAndDecideToSelectNewSolution(List<Job> originSolution, List<Job> potencialSolution1,List<Job> potencialSolution2,List<Job> potencialSolution3,
            List<Job> potencialSolution4, List<Job> potencialSolution5, List<Job> potencialSolution6, List<Job> container1, List<Job> container2)
        {           
            List<Tuple<int, List<Job>>> sortedList = new List<Tuple<int, List<Job>>>();
            sortedList.Add(Tuple.Create(GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialSolution1), potencialSolution1));
            sortedList.Add(Tuple.Create(GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialSolution2), potencialSolution2));
            sortedList.Add(Tuple.Create(GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialSolution3), potencialSolution3));
            sortedList.Add(Tuple.Create(GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialSolution4), potencialSolution4));
            sortedList.Add(Tuple.Create(GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialSolution5), potencialSolution5));
            sortedList.Add(Tuple.Create(GeneticAlgorithmHelper.CountTotalExecutionTimeInOfAllJobsInMachine(potencialSolution6), potencialSolution6));
            sortedList = sortedList.OrderBy(x=>x.Item1).ToList();

            double range = Random.Shared.NextDouble(); // może sorted list
            if(range < 0.5) { TreeSelectBetweenTwoMachines(sortedList[0].Item2, originSolution, container1, container2);}
            else if(range > 0.5 && range < 0.65) { TreeSelectBetweenTwoMachines(sortedList[1].Item2, originSolution, container1, container2); }
            else if(range >= 0.65 && range < 0.80) { TreeSelectBetweenTwoMachines(sortedList[2].Item2, originSolution, container1, container2); }
            else if(range >= 0.80 && range < 0.90) { TreeSelectBetweenTwoMachines(sortedList[3].Item2, originSolution, container1, container2); }
            else if(range >= 0.90 && range < 0.95) { TreeSelectBetweenTwoMachines(sortedList[4].Item2, originSolution, container1, container2); }
            else { TreeSelectBetweenTwoMachines(sortedList[5].Item2, originSolution, container1, container2); }
        }

        private void AssignMachinesToReference(List<Job> originSolution, int referenceMainSolution, List<Job> potencialSolution1, int referenceSolution_1, List<Job> potencialSolution2, int referenceSolution_2, List<Job> potencialSolution3, int referenceSolution_3,
            List<Job> potencialSolution4, int referenceSolution_4, List<Job> potencialSolution5, int referenceSolution_5, List<Job> potencialSolution6, int referenceSolution_6)
        {
            listOfJobs[referenceMainSolution].AddRange(originSolution);
            originSolution.Clear();

            listOfJobs[referenceSolution_1].AddRange(potencialSolution1);
            potencialSolution1.Clear();

            listOfJobs[referenceSolution_2].AddRange(potencialSolution2);
            potencialSolution2.Clear();

            listOfJobs[referenceSolution_3].AddRange(potencialSolution3);
            potencialSolution3.Clear();

            listOfJobs[referenceSolution_4].AddRange(potencialSolution4);
            potencialSolution4.Clear();

            listOfJobs[referenceSolution_5].AddRange(potencialSolution5);
            potencialSolution5.Clear();

            listOfJobs[referenceSolution_6].AddRange(potencialSolution6);
            potencialSolution6.Clear();        
        }


        private void TreeSelectBetweenTwoMachines(List<Job> Solution1, List<Job> Solution2, List<Job> container1, List<Job> container2)
        {
            int totalExecutionTimeCounterMachine_1 = 0;
            int totalExecutionTimeCounterMachine_2 = 0;
            int randomSelector = 0;          
            while (true)
            {
                if (Solution1.Count > 0)
                {
                    randomSelector = RandomValueGen.GetRandomInt(0, Solution1.Count - 1);
                    if (totalExecutionTimeCounterMachine_1 < totalExecutionTimeCounterMachine_2 || totalExecutionTimeCounterMachine_1 == totalExecutionTimeCounterMachine_2)
                    {
                        totalExecutionTimeCounterMachine_1 += Solution1[randomSelector].totalTimeOfExecuteThisJob;
                        container1.Add(Solution1[randomSelector]);
                    }
                    else
                    {
                        container2.Add(Solution1[randomSelector]);
                        totalExecutionTimeCounterMachine_2 += Solution1[randomSelector].totalTimeOfExecuteThisJob;
                    }
                    Solution1.Remove(Solution1[randomSelector]);
                }
                if (Solution2.Count > 0)
                {
                    randomSelector = RandomValueGen.GetRandomInt(0, Solution2.Count - 1);
                    if (totalExecutionTimeCounterMachine_2 < totalExecutionTimeCounterMachine_1 || totalExecutionTimeCounterMachine_1 == totalExecutionTimeCounterMachine_2)
                    {
                        totalExecutionTimeCounterMachine_2 += Solution2[randomSelector].totalTimeOfExecuteThisJob;
                        container2.Add(Solution2[randomSelector]);
                    }
                    else
                    {
                        container1.Add(Solution2[randomSelector]);
                        totalExecutionTimeCounterMachine_1 += Solution2[randomSelector].totalTimeOfExecuteThisJob;
                    }
                    Solution2.Remove(Solution2[randomSelector]);
                }
                if (Solution1.Count == 0 && Solution2.Count == 0) break;
            }
           
            Solution1.AddRange(container2);
            Solution2.AddRange(container1);
            container1.Clear();
            container2.Clear();
           
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
    }
}
