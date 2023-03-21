namespace GeneticAlgorithmAPI.Entities
{
    public class Information
    {
        public int minSizeBeforeStartStrategy { get; set; }

        public int maxSizeBeforeStartStrategy { get; set; }

        public int minSizeAfterStartStrategy { get; set; }

        public int maxSizeAfterStartStrategy { get; set; }

        public int totalNumbersOfMachines { get; set; }

        public int totalNumbersOfJobs { get; set; }

        public string? strategy { get; set; }

        public int percentageDifferenceBetweenMin { get; set; }

        public int percentageDifferenceBetweenMax { get; set; }

    }
}
