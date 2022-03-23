using System.Globalization;

namespace FileSorting
{
    class FileSortingService
    {
        private string inFilePath;
        private string outFilePath;
        private string[,] matrix;

        public FileSortingService(string inFilePath, string outFilePath)
        {
            this.inFilePath = inFilePath;
            this.outFilePath = outFilePath;
            loadDataFromFile(inFilePath);
        }

        public void PrintMatrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
        }

        public void Sort()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    for (int k = 0; k < matrix.GetLength(1) - 1 && matrix[i, k] == matrix[j, k]; k++)
                    {
                        if (compareStrings(matrix[i, k + 1], matrix[j, k + 1]))
                        {
                            Swap(i, j);
                            continue;
                        }
                    }

                    if (compareStrings(matrix[i, 0], matrix[j, 0]))
                    {
                        Swap(i, j);
                    }
                }
            }
        }

        public void SaveSortedDataToFile()
        {
            using (StreamWriter writer = new StreamWriter(outFilePath))
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        writer.WriteAsync($"{matrix[i, j]}\t");
                    }
                    writer.WriteLine();
                }
            }
        }

        private void loadDataFromFile(string inFilePath)
        {
            var dimensionSizes = getDimensionSizes(inFilePath);
            matrix = new string[dimensionSizes.linesCount, dimensionSizes.columnsCount];

            for (int i = 0; i < dimensionSizes.linesCount; i++)
            {
                for (int j = 0; j < dimensionSizes.columnsCount; j++)
                {
                    matrix[i, j] = "";
                }
            }

            try
            {
                using (StreamReader sr = new StreamReader(inFilePath))
                {
                    for (int i = 0; i < dimensionSizes.linesCount; i++)
                    {
                        var line = sr.ReadLine();
                        var lineStrings = line.Split('\t');

                        for (int j = 0; j < lineStrings.Length; j++)
                        {
                            matrix[i, j] = lineStrings[j];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private (int linesCount, int columnsCount) getDimensionSizes(string inFilePath)
        {
            var dimensionsSize = new List<int>();

            try
            {
                using (StreamReader sr = new StreamReader(inFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var columnsNumber = line.Split('\t').Length;
                        dimensionsSize.Add(columnsNumber);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var linesCount = dimensionsSize.Count();
            var columnsCount = dimensionsSize.Max();

            return (linesCount, columnsCount);
        }

        private bool compareStrings(string firstStr, string secondStr)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (Double.TryParse(firstStr, NumberStyles.Any, provider, out double i))
            {
                if (Double.TryParse(secondStr, NumberStyles.Any, provider, out double j))
                {
                    if (i > j)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            else if (Double.TryParse(secondStr, NumberStyles.Any, provider, out double j))
            {
                return true;
            }

            if (firstStr.CompareTo(secondStr) > 0)
                return true;
            return false;
        }

        private void Swap(int firstStringIndex, int secondStringIndex)
        {
            string[] temp = new string[matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                temp[i] = matrix[firstStringIndex, i];
                matrix[firstStringIndex, i] = matrix[secondStringIndex, i];
                matrix[secondStringIndex, i] = temp[i];
            }
        }
    }
}
