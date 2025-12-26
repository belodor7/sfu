// //line 71
// //prev ln: DustStatistic statistics = GetStatistic(dustArray); 
//         PrintStatistics(statistics);


// //line 255-...
// //prev GenerateFromFrequency()
//     static void PrintDust(Dust[] dustArray, int count = 5)
//     {
//         int printCount = Math.Min(count, dustArray.Length);
//         Console.WriteLine($"--- Первые {printCount} значений сгенерированной пыли ---");
        
//         for (int i = 0; i < printCount; i++)
//         {
//             Console.WriteLine($"\nЗапись {i + 1}:");
//             Console.WriteLine($"  Температура: {dustArray[i].temperature:F2}°C");
//             Console.WriteLine($"  Влажность: {dustArray[i].humidity:F2}%");
//             Console.WriteLine($"  Плотность: {dustArray[i].density:F2}");
//             Console.WriteLine($"  Емкость пыли: {dustArray[i].dust_capacity:F2}");
//             Console.WriteLine($"  Размер частиц: {dustArray[i].particle_size:F2}");
//             Console.WriteLine($"  УЭС: {dustArray[i].resistivity:F2}");
//             Console.WriteLine($"  Проводимость: {dustArray[i].conductivity}");
//             Console.WriteLine($"  Дисперсность: {dustArray[i].dust_dispersiveness}");
//             Console.WriteLine($"  Способ образования: {dustArray[i].formation}");
//         }
//     }

// //line 269-...
// //prev WriteCsvFile()
// static void PrintStatistics(DustStatistic stats)
//     {
//         Console.WriteLine("--- СТАТИСТИКА РЕАЛЬНЫХ ДАННЫХ ---");
        
//         PrintNumericStatistics("Температура", stats.temperature);
//         PrintNumericStatistics("Влажность", stats.humidity);
//         PrintNumericStatistics("Плотность", stats.density);
//         PrintNumericStatistics("Емкость пыли", stats.dust_capacity);
//         PrintNumericStatistics("Размер частиц", stats.particle_size);
//         PrintNumericStatistics("УЭС", stats.resistivity);
        
//         Console.WriteLine("\n--- ТЕКСТОВЫЕ ПАРАМЕТРЫ ---");
//         PrintStringStatistics("Проводимость", stats.conductivity.frequency);
//         PrintStringStatistics("Дисперсность", stats.dust_dispersiveness.frequency);
//         PrintStringStatistics("Способ образования", stats.formation.frequency);
//     }

//     static void PrintNumericStatistics(string name, Statistics stats)
//     {
//         Console.WriteLine($"\n{name}:");
//         Console.WriteLine($"  Макс: {stats.max:F6}");
//         Console.WriteLine($"  Мин: {stats.min:F6}");
//         Console.WriteLine($"  Среднее: {stats.mean:F6}");
//         Console.WriteLine($"  Дисперсия: {stats.variance:F6}");
//         Console.WriteLine($"  Станд. отклонение: {stats.stdDeviation:F6}");
//     }

//     static void PrintStringStatistics(string name, Dictionary<string, int> frequency)
//     {
//         Console.WriteLine($"\n{name}:");
//         List<KeyValuePair<string, int>> sortedFreq = new List<KeyValuePair<string, int>>(frequency);
//         sortedFreq.Sort((a, b) => b.Value.CompareTo(a.Value));
//         foreach (var kvp in sortedFreq)
//         {
//             Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
//         }
//     }