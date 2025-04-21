using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string inputFile = Path.Combine(desktopPath, "numbers.txt");
        string outputFile = Path.Combine(desktopPath, "output.txt");

        GenerateRandomNumbersFile(inputFile, 20, -100, 100);

        ProcessNumbersFile(inputFile, outputFile);
    }
    static void GenerateRandomNumbersFile(string filePath, int count, int minValue, int maxValue) // метод для генерации файла с случ числами 
    {
        Random random = new Random();
        // Открытие потока для записи в файл
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                // Генерация случайного числа в указанном диапазоне
                int randomNumber = random.Next(minValue, maxValue + 1);
                writer.WriteLine(randomNumber);
            }
        }
        Console.WriteLine($"Сгенерированы числа и записаны в файл {filePath}");
    }
    static void ProcessNumbersFile(string inputFilePath, string outputFilePath)
    {
        int lastNegativeNumber = 0;
        bool foundNegative = false;
        string[] lines = File.ReadAllLines(inputFilePath);

        foreach (var line in lines)
        {
            if (int.TryParse(line, out int number))
            {
                if (number < 0)
                {
                    lastNegativeNumber = number;
                    foundNegative = true;
                }
            }
        }
        if (!foundNegative)
        {
            Console.WriteLine("Отрицательные числа не найдены в файле.");
            return;
        }
        double halfOfLastNegative = lastNegativeNumber / 2.0;

        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            foreach (var line in lines)
            {
                if (int.TryParse(line, out int number))
                {
                    writer.WriteLine(number + halfOfLastNegative);
                }
            }
        }
        Console.WriteLine($"Выходной файл {outputFilePath} успешно создан.");
    }
}
