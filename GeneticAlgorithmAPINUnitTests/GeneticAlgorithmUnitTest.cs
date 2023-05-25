using GeneticAlgorithmAPI.Interfaces;
using GeneticAlgorithmAPI.Strategies;

namespace GeneticAlgorithmAPINUnitTests
{
    [TestFixture]
    public class GeneticAlgorithmUnitTest
    {

        private IStrategy? strategy = null;

        [Test]
        [TestCase(2, 8, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm first strategy")]
        [TestCase(9, 887667, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm first strategy")]
        [TestCase(100, 999, 1, 100, 10, Author = "IgorDev", Category = "Genetic algorithm first strategy")]
        [TestCase(10, 10000, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm first strategy")]
        [TestCase(250, 50000, 1, 100, 1000000, Author = "IgorDev", Category = "Genetic algorithm first strategy")]
        public void CheckNumberOfJobsAndMachines_Test(int numberOfMachines, int numberOfJobs, int minTimeOfExecutionOfJob, int maxTimeOfExecutionOfJob, int iteration)
        {
            strategy = new TwoPointsCrossingWithMutationStrategy(numberOfMachines, numberOfJobs, minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob, iteration);
            strategy.SetUpMyStrategy();
            strategy.RunMyStrategy();
            Assert.NotNull(strategy);
            var myStrategy = ((TwoPointsCrossingWithMutationStrategy)strategy);
            Assert.That(numberOfMachines, Is.EqualTo(myStrategy.GetNumberOfMachines()));
            Assert.That(numberOfJobs, Is.EqualTo(myStrategy.GetNumberOfJobs()));          
        }


        [Test]
        [TestCase(10, 100, 1, 100,100, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(1000, 100000, 1, 100, 100, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(555, 700809, 1, 100, 10000, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(10, 995, 1, 100, 8789, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(34, 357, 1, 100, 880, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(145, 1407, 1, 100, 9788, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(28, 777, 1, 100, 21555, Author = "IgorDev", Category = "Unique tests")]
        public void CheckUniqueOfJobsTest(int numberOfMachines, int numberOfJobs, int minTimeOfExecutionOfJob, int maxTimeOfExecutionOfJob, int iteration)
        {
            List<int> myNumbers = new List<int>();
            for (int i = 0; i < numberOfJobs; i++)
            {
                myNumbers.Add(i);
            }
            strategy = new TwoPointsCrossingWithMutationStrategy(numberOfMachines, numberOfJobs, minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob, iteration);
            strategy.SetUpMyStrategy();
            strategy.RunMyStrategy();
            Assert.NotNull(strategy);
            var list = ((TwoPointsCrossingWithMutationStrategy)strategy).CheckUniqueOfJobs();
            for (int i = 0; i < numberOfJobs; i++)
            {
                if (list[i] != myNumbers[i])
                {
                    Assert.Fail("Unique numbers are not equal after running logic of genetic algorithm");
                }
            }          
        }

        [Test]
        [TestCase(10, 100, 1, 100, 100, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(1000, 100000, 1, 100, 100, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(555, 700809, 1, 100, 10000, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(10, 995, 1, 100, 8789, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(34, 357, 1, 100, 880, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(145, 1407, 1, 100, 9788, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(28, 777, 1, 100, 21555, Author = "IgorDev", Category = "Unique tests")]
        public void CheckUniqueOfJobs_TreeStrategy_Test(int numberOfMachines, int numberOfJobs, int minTimeOfExecutionOfJob, int maxTimeOfExecutionOfJob, int iteration)
        {
            List<int> myNumbers = new List<int>();
            for (int i = 0; i < numberOfJobs; i++)
            {
                myNumbers.Add(i);
            }
            strategy = new TreeCrossingStrategy(numberOfMachines, numberOfJobs, minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob, iteration);
            strategy.SetUpMyStrategy();
            strategy.RunMyStrategy();
            Assert.NotNull(strategy);
            var list = ((TreeCrossingStrategy)strategy).CheckUniqueOfJobs();
            for (int i = 0; i < numberOfJobs; i++)
            {
                if (list[i] != myNumbers[i])
                {
                    Assert.Fail("Unique numbers are not equal after running logic of genetic algorithm");
                }
            }
        }

        [Test]
        [TestCase(10, 2000, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(9, 8876, 1, 100, 1000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(100, 999, 1, 23, 77, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(10, 10000, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(250, 50000, 1, 100, 1000000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        public void CheckNumberOfJobsAndMachines_TreeStrategy_Test(int numberOfMachines, int numberOfJobs, int minTimeOfExecutionOfJob, int maxTimeOfExecutionOfJob, int iteration)
        {
            strategy = new TreeCrossingStrategy(numberOfMachines, numberOfJobs, minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob, iteration);
            strategy.SetUpMyStrategy();
            strategy.RunMyStrategy();
            Assert.NotNull(strategy);
            var myStrategy = ((TreeCrossingStrategy)strategy);
            Assert.That(numberOfMachines, Is.EqualTo(myStrategy.GetNumberOfMachines()));
            Assert.That(numberOfJobs, Is.EqualTo(myStrategy.GetNumberOfJobs()));
        }


        [Test]
        [TestCase(10, 2000, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(9, 8876, 1, 100, 1000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(100, 999, 1, 23, 77, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(10, 10000, 1, 100, 10000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        [TestCase(250, 50000, 1, 100, 1000000, Author = "IgorDev", Category = "Genetic algorithm tree strategy")]
        public void CheckNumberOfJobsAndMachines_TournamentSelectionStrategy_Test(int numberOfMachines, int numberOfJobs, int minTimeOfExecutionOfJob, int maxTimeOfExecutionOfJob, int iteration)
        {
            strategy = new MutationWithTournamentSelectionStrategy(numberOfMachines, numberOfJobs, minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob, iteration);
            strategy.SetUpMyStrategy();
            strategy.RunMyStrategy();
            Assert.NotNull(strategy);
            var myStrategy = ((MutationWithTournamentSelectionStrategy)strategy);
            Assert.That(numberOfMachines, Is.EqualTo(myStrategy.GetNumberOfMachines()));
            Assert.That(numberOfJobs, Is.EqualTo(myStrategy.GetNumberOfJobs()));
        }

        [Test]
        [TestCase(10, 100, 1, 100, 100, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(1000, 100000, 1, 100, 100, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(555, 700809, 1, 100, 10000, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(10, 995, 1, 100, 8789, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(34, 357, 1, 100, 880, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(145, 1407, 1, 100, 9788, Author = "IgorDev", Category = "Unique tests")]
        [TestCase(28, 777, 1, 100, 21555, Author = "IgorDev", Category = "Unique tests")]
        public void CheckUniqueOfJobs_TournamentSelectionStrategy_Test(int numberOfMachines, int numberOfJobs, int minTimeOfExecutionOfJob, int maxTimeOfExecutionOfJob, int iteration)
        {
            List<int> myNumbers = new List<int>();
            for (int i = 0; i < numberOfJobs; i++)
            {
                myNumbers.Add(i);
            }
            strategy = new MutationWithTournamentSelectionStrategy(numberOfMachines, numberOfJobs, minTimeOfExecutionOfJob, maxTimeOfExecutionOfJob, iteration);
            strategy.SetUpMyStrategy();
            strategy.RunMyStrategy();
            Assert.NotNull(strategy);
            var list = ((MutationWithTournamentSelectionStrategy)strategy).CheckUniqueOfJobs();
            for (int i = 0; i < numberOfJobs; i++)
            {
                if (list[i] != myNumbers[i])
                {
                    Assert.Fail("Unique numbers are not equal after running logic of genetic algorithm");
                }
            }
        }
    }
}
