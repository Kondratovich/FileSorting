namespace FileSorting
{
    class Program
    {
        private static readonly string CsvFilesPath = @$"{Directory.GetCurrentDirectory()}\FileData\";
        public static void Main(string[] args)
        {
            var fileSortingService = new FileSortingService($"{CsvFilesPath}in.txt", $"{CsvFilesPath}out.txt");
            fileSortingService.PrintMatrix();
            fileSortingService.Sort();
            Console.WriteLine("--------------------------------");
            fileSortingService.PrintMatrix();
            fileSortingService.SaveSortedDataToFile();
        }
    }
}