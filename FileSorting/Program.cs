namespace FileSorting
{
    class Program
    {
        public static void Main(string[] args)
        {
            var fileSortingService = new FileSortingService(args[0], args[1]);
            fileSortingService.PrintMatrix();
            fileSortingService.Sort();
            Console.WriteLine("--------------------------------");
            fileSortingService.PrintMatrix();
            fileSortingService.SaveSortedDataToFile();
        }
    }
}