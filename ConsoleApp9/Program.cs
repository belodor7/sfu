using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

public struct Dust
{
    public double temperature { get; set; }
    public double humidity { get; set; }
    public double density { get; set; }
    public double dust_capacity { get; set; }
    public double particle_size { get; set; }
    public double resistivity { get; set; }
    public string conductivity { get; set; }
    public string dust_dispersiveness { get; set; }
    public string formation { get; set; }
}

public struct Statistics
{
    public double max;
    public double min;
    public double mean;
    public double variance;
    public double stdDeviation;
}

public struct StringStatistics
{
    public Dictionary<string, int> frequency;
}

public struct DustStatistic
{
    public Statistics temperature;
    public Statistics humidity;
    public Statistics density;
    public Statistics dust_capacity;
    public Statistics particle_size;
    public Statistics resistivity;
    public StringStatistics conductivity;
    public StringStatistics dust_dispersiveness;
    public StringStatistics formation;
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string filePath = "C:/Users/kuchm/sfu/ЛАБЫ/dust.csv";
        Dust[] dustArray = ReadCsvFile(filePath);      
        DustStatistic statistics = GetStatistic(dustArray);

        int countToGenerate = 360;
        Stopwatch sw = Stopwatch.StartNew();
        Dust[] generatedDust = GenerateDust(countToGenerate, statistics);
        sw.Stop();
        Console.WriteLine($"Время выполнения GenerateDust: {sw.ElapsedTicks} тик");

        WriteCsvFile("generated_dust.csv", generatedDust);
        Console.WriteLine($"Данные записаны в файл: generated_dust.csv");

        Console.WriteLine("--- Проверка совпадения средних значений ---");
        VerifyStatistics(dustArray, generatedDust);
    }

    static Dust[] ReadCsvFile(string fileName)
    {
        var culture = System.Globalization.CultureInfo.InvariantCulture;
        List<Dust> dustList = new List<Dust>();
        string[] lines = File.ReadAllLines(fileName);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(';');
            if (parts.Length < 10) continue;
            
            if (string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]) || string.IsNullOrWhiteSpace(parts[2]) || string.IsNullOrWhiteSpace(parts[3]) || string.IsNullOrWhiteSpace(parts[4]) || string.IsNullOrWhiteSpace(parts[5])) continue;
                
            Dust dust = new Dust
            {
                resistivity = double.Parse(parts[0].Replace(",", "."), culture),
                temperature = double.Parse(parts[1].Replace(",", "."), culture),
                humidity = double.Parse(parts[2].Replace(",", "."), culture),
                density = double.Parse(parts[3].Replace(",", "."), culture),
                dust_capacity = double.Parse(parts[4].Replace(",", "."), culture),
                particle_size = double.Parse(parts[5].Replace(",", "."), culture),
                conductivity = parts[7].Trim(),
                dust_dispersiveness = parts[8].Trim(),
                formation = parts[9].Trim()
            };

            dustList.Add(dust);
        }
        return dustList.ToArray();
    }

    static DustStatistic GetStatistic(Dust[] dustArray)
    {
        DustStatistic stat = new DustStatistic();

        double[] tempValues = new double[dustArray.Length];
        double[] humidValues = new double[dustArray.Length];
        double[] densValues = new double[dustArray.Length];
        double[] capacValues = new double[dustArray.Length];
        double[] partValues = new double[dustArray.Length];
        double[] resistValues = new double[dustArray.Length];
        string[] condValues = new string[dustArray.Length];
        string[] dispValues = new string[dustArray.Length];
        string[] formValues = new string[dustArray.Length];

        for (int i = 0; i < dustArray.Length; i++)
        {
            tempValues[i] = dustArray[i].temperature;
            humidValues[i] = dustArray[i].humidity;
            densValues[i] = dustArray[i].density;
            capacValues[i] = dustArray[i].dust_capacity;
            partValues[i] = dustArray[i].particle_size;
            resistValues[i] = dustArray[i].resistivity;
            condValues[i] = dustArray[i].conductivity;
            dispValues[i] = dustArray[i].dust_dispersiveness;
            formValues[i] = dustArray[i].formation;
        }

        stat.temperature = CalculateStatistics(tempValues);
        stat.humidity = CalculateStatistics(humidValues);
        stat.density = CalculateStatistics(densValues);
        stat.dust_capacity = CalculateStatistics(capacValues);
        stat.particle_size = CalculateStatistics(partValues);
        stat.resistivity = CalculateStatistics(resistValues);

        stat.conductivity.frequency = CalculateFrequency(condValues);
        stat.dust_dispersiveness.frequency = CalculateFrequency(dispValues);
        stat.formation.frequency = CalculateFrequency(formValues);

        return stat;
    }

    static Statistics CalculateStatistics(double[] values)
    {
        Statistics stats = new Statistics();
        
        stats.max = values.Max();
        stats.min = values.Min();
        stats.mean = values.Average();
        
        double variance = 0;
        foreach (double value in values)
        {
            variance += (value - stats.mean) * (value - stats.mean);
        }
        stats.variance = variance / values.Length;
        stats.stdDeviation = Math.Sqrt(stats.variance);
        return stats;
    }

    static Dictionary<string, int> CalculateFrequency(string[] values)
    {
        Dictionary<string, int> frequency = new Dictionary<string, int>();
        
        foreach (string value in values)
        {
            if (string.IsNullOrWhiteSpace(value)) continue;
            
            if (frequency.ContainsKey(value))
                frequency[value]++;
            else
                frequency[value] = 1;
        }

        return frequency;
    }

    static Dust[] GenerateDust(int count, DustStatistic statistics)
    {
        Dust[] generatedDust = new Dust[count];

        for (int i = 0; i < count; i++)
        {
            generatedDust[i] = new Dust
            {
                temperature = GenerateNormalRandom(statistics.temperature),
                humidity = GenerateNormalRandom(statistics.humidity),
                density = GenerateNormalRandom(statistics.density),
                dust_capacity = GenerateNormalRandom(statistics.dust_capacity),
                particle_size = GenerateNormalRandom(statistics.particle_size),
                resistivity = GenerateNormalRandom(statistics.resistivity),
                conductivity = GenerateFromFrequency(statistics.conductivity.frequency),
                dust_dispersiveness = GenerateFromFrequency(statistics.dust_dispersiveness.frequency),
                formation = GenerateFromFrequency(statistics.formation.frequency)
            };
        }

        return generatedDust;
    }

    static double GenerateNormalRandom(Statistics stats)
    {
        Random random = new Random();
        double u1 = random.NextDouble();
        double u2 = random.NextDouble();
        double z0 = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
        
        double value = stats.mean + z0 * stats.stdDeviation;
        
        if (value < stats.min) value = stats.min;
        if (value > stats.max) value = stats.max;

        return value;
    }

    static string GenerateFromFrequency(Dictionary<string, int> frequency)
    {
        Random random = new Random();

        if (frequency.Count == 0) return "";

        int totalCount = frequency.Values.Sum();
        int randomValue = random.Next(0, totalCount);
        int currentSum = 0;

        foreach (var kvp in frequency)
        {
            currentSum += kvp.Value;
            if (randomValue < currentSum)
                return kvp.Key;
        }

        foreach (var kvp in frequency)
            return kvp.Key;

        return "";
    }

    static void WriteCsvFile(string fileName, Dust[] dustArray)
    {
        List<string> lines = new List<string>();
        lines.Add("resistivity;temperature;humidity;density;dust_capacity;particle_size;conductivity;dust_dispersiveness;formation");

        foreach (Dust dust in dustArray)
        {
            string line = $"{dust.resistivity:F4};{dust.temperature:F2};{dust.humidity:F2};{dust.density:F6};{dust.dust_capacity:F6};{dust.particle_size:F6};{dust.conductivity};{dust.dust_dispersiveness};{dust.formation}";
            lines.Add(line.Replace(".", ","));
        }

        File.WriteAllLines(fileName, lines);
    }

    static void VerifyStatistics(Dust[] original, Dust[] generated)
    {
        DustStatistic origStats = GetStatistic(original);
        DustStatistic genStats = GetStatistic(generated);

        Console.WriteLine($"{"Параметр",-15} {"Оригинал",-15} {"Сгенерировано",-15} {"Разница %",-10}");
        Console.WriteLine(new string('-', 60));

        VerifyValue("Температура", origStats.temperature.mean, genStats.temperature.mean);
        VerifyValue("Влажность", origStats.humidity.mean, genStats.humidity.mean);
        VerifyValue("Плотность", origStats.density.mean, genStats.density.mean);
        VerifyValue("Емкость пыли", origStats.dust_capacity.mean, genStats.dust_capacity.mean);
        VerifyValue("Размер частиц", origStats.particle_size.mean, genStats.particle_size.mean);
        VerifyValue("УЭС", origStats.resistivity.mean, genStats.resistivity.mean);
    }

    static void VerifyValue(string name, double original, double generated)
    {
        double diff = Math.Abs(original - generated) / original * 100;
        Console.WriteLine($"{name,-15} {original,-15:F6} {generated,-15:F6} {diff:F2}%");
    }
}