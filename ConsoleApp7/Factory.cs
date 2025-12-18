namespace ConsoleApp7;

public class Factory
{
    public List<Samokat> SamokatsFromChina = new List<Samokat>();
    public List<Engine> Engines = new List<Engine>();
    public List<Wheel> Wheels = new List<Wheel>();
    public List<Battery> Batteries = new List<Battery>();
    public List<SuperSamokat> SamokatWithTuning = new List<SuperSamokat>();

    private decimal losses = 0;
    private decimal profit = 0;
    private int soldSamokat = 0;
    
    public Factory()
    {
        InitializeDetails();
    }
    
    private void InitializeDetails()
    {
        Engines.Add(new Engine(250, 10000, 7));
        Engines.Add(new Engine(500, 13000, 10));
        Engines.Add(new Engine(750, 14800, 15));
        
        Wheels.Add(new Wheel(WheelType.Standard, 1000, 2));
        Wheels.Add(new Wheel(WheelType.LightAlloy, 2000, 1));
        Wheels.Add(new Wheel(WheelType.AllTerrain, 3000, 3));
        
        Batteries.Add(new Battery(1000, 3000, 3));
        Batteries.Add(new Battery(2000, 4500, 5));
    }
    
    public void CreateDetails()
    {
        while (true)
        {
            Console.WriteLine("Выберите тип детали для создания:");
            Console.WriteLine("1. Двигатель");
            Console.WriteLine("2. Колеса");
            Console.WriteLine("3. Батарея");
            Console.WriteLine("4. Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Выберите двигатель:");
                    Console.WriteLine("1. 250 W - 10 000 руб., масса 7 кг");
                    Console.WriteLine("2. 500 W - 13 000 руб., масса 10 кг");
                    Console.WriteLine("3. 750 W - 14 800 руб., масса 15 кг");
                    string engChoice = Console.ReadLine();
                    switch (engChoice)
                    {
                        case "1":
                            Engines.Add(new Engine(250, 10000, 7));
                            Console.WriteLine("Двигатель 250 W добавлен.");
                            break;
                        case "2":
                            Engines.Add(new Engine(500, 13000, 10));
                            Console.WriteLine("Двигатель 500 W добавлен.");
                            break;
                        case "3":
                            Engines.Add(new Engine(750, 14800, 15));
                            Console.WriteLine("Двигатель 750 W добавлен.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("Выберите колеса:");
                    Console.WriteLine("1. Стандартные - 1 000 руб., масса 2 кг");
                    Console.WriteLine("2. Легкосплавные - 2 000 руб., масса 1 кг");
                    Console.WriteLine("3. Вездеходные - 3 000 руб., масса 3 кг");
                    string wheelChoice = Console.ReadLine();
                    switch (wheelChoice)
                    {
                        case "1":
                            Wheels.Add(new Wheel(WheelType.Standard, 1000, 2));
                            Console.WriteLine("Стандартные колеса добавлены.");
                            break;
                        case "2":
                            Wheels.Add(new Wheel(WheelType.LightAlloy, 2000, 1));
                            Console.WriteLine("Легкосплавные колеса добавлены.");
                            break;
                        case "3":
                            Wheels.Add(new Wheel(WheelType.AllTerrain, 3000, 3));
                            Console.WriteLine("Вездеходные колеса добавлены.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;
                case "3":
                    Console.WriteLine("Выберите батарею:");
                    Console.WriteLine("1. 1000 Wh - 3 000 руб., масса 3 кг");
                    Console.WriteLine("2. 2000 Wh - 4 500 руб., масса 5 кг");
                    string batChoice = Console.ReadLine();
                    switch (batChoice)
                    {
                        case "1":
                            Batteries.Add(new Battery(1000, 3000, 3));
                            Console.WriteLine("Батарея 1000 Wh добавлена.");
                            break;
                        case "2":
                            Batteries.Add(new Battery(2000, 4500, 5));
                            Console.WriteLine("Батарея 2000 Wh добавлена.");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор.");
                            break;
                    }
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
    
    public void AutoTuneSamokats(List<Customer> customers)
    {
        SamokatWithTuning.Clear();
        int samokatIndex = 0;
        foreach (Customer cu in customers)
        {
            if (samokatIndex >= SamokatsFromChina.Count) break;
            Samokat baseSamokat = SamokatsFromChina[samokatIndex++];
            var super = new SuperSamokat(baseSamokat.Engine, baseSamokat.Wheel, baseSamokat.Battery, baseSamokat.Base);
            int usedTun = 0;

            switch (cu.Type)
            {
                case CustomerType.Student:
                    break;
                case CustomerType.Weak:
                    if (Wheels.Count > 0)
                    {
                        Wheel lightWheel = Wheels[0];
                        foreach (var w in Wheels)
                        {
                            if (w.Mass < lightWheel.Mass) lightWheel = w;
                        }
                        Wheels.Add(super.Wheel);
                        super.Wheel = lightWheel;
                        Wheels.Remove(lightWheel);
                    }
                    if (Batteries.Count > 0)
                    {
                        Battery lightBattery = Batteries[0];
                        foreach (var b in Batteries)
                        {
                            if (b.Mass < lightBattery.Mass) lightBattery = b;
                        }
                        Batteries.Add(super.Battery);
                        super.Battery = lightBattery;
                        Batteries.Remove(lightBattery);
                    }
                    break;
                case CustomerType.RichKid:
                    bool hasAlloy = false;
                    Wheel alloyWheel = null;
                    foreach (var w in Wheels)
                    {
                        if (w.Type == WheelType.LightAlloy)
                        {
                            alloyWheel = w;
                            hasAlloy = true;
                            break;
                        }
                    }
                    if (hasAlloy)
                    {
                        Wheels.Add(super.Wheel);
                        super.Wheel = alloyWheel;
                        Wheels.Remove(alloyWheel);
                    }
                    if (Engines.Count > 0)
                    {
                        Engine bestEngine = Engines[0];
                        foreach (var e in Engines)
                        {
                            if (e.Power > bestEngine.Power) bestEngine = e;
                        }
                        Engines.Add(super.Engine);
                        super.Engine = bestEngine;
                        Engines.Remove(bestEngine);
                    }
                    if (Batteries.Count > 0)
                    {
                        Battery bestBattery = Batteries[0];
                        foreach (var b in Batteries)
                        {
                            if (b.Capacity > bestBattery.Capacity) bestBattery = b;
                        }
                        Batteries.Add(super.Battery);
                        super.Battery = bestBattery;
                        Batteries.Remove(bestBattery);
                    }
                    int maxExtra = super.Base.CountTun;
                    while (usedTun < maxExtra && Engines.Count > 0)
                    {
                        Engine eng = Engines[0];
                        foreach (var e in Engines)
                        {
                            if (e.Power > eng.Power) eng = e;
                        }
                        super.ExtraEngines.Add(eng);
                        Engines.Remove(eng);
                        usedTun++;
                    }
                    while (usedTun < maxExtra && Batteries.Count > 0)
                    {
                        Battery bat = Batteries[0];
                        foreach (var b in Batteries)
                        {
                            if (b.Capacity > bat.Capacity) bat = b;
                        }
                        super.ExtraBatteries.Add(bat);
                        Batteries.Remove(bat);
                        usedTun++;
                    }
                    if (usedTun < maxExtra)
                    {
                        super.HasBaggageRack = true;
                        usedTun++;
                    }
                    super.HasHeadlight = true;
                    super.HasStickers = true;
                    break;
                case CustomerType.Courier:
                    bool hasTerrain = false;
                    Wheel terrainWheel = null;
                    foreach (var wh in Wheels)
                    {
                        if (wh.Type == WheelType.AllTerrain)
                        {
                            terrainWheel = wh;
                            hasTerrain = true;
                            break;
                        }
                    }
                    if (hasTerrain)
                    {
                        Wheels.Add(super.Wheel);
                        super.Wheel = terrainWheel;
                        Wheels.Remove(terrainWheel);
                    }
                    super.HasBaggageRack = true;
                    usedTun++;
                    if (Batteries.Count > 0 && usedTun < super.Base.CountTun)
                    {
                        Battery bestBattery = Batteries[0];
                        foreach (var bat in Batteries)
                        {
                            if (bat.Capacity > bestBattery.Capacity) bestBattery = bat;
                        }
                        Batteries.Add(super.Battery);
                        super.Battery = bestBattery;
                        Batteries.Remove(bestBattery);
                    }
                    break;
                case CustomerType.AnimeFan:
                    super.HasStickers = true;
                    break;
                case CustomerType.Racer:
                    if (Engines.Count > 0)
                    {
                        Engine bestEngine = Engines[0];
                        foreach (var eng in Engines)
                        {
                            if (eng.Power > bestEngine.Power) bestEngine = eng;
                        }
                        Engines.Add(super.Engine);
                        super.Engine = bestEngine;
                        Engines.Remove(bestEngine);
                    }
                    if (Wheels.Count > 0)
                    {
                        Wheel lightWheel = Wheels[0];
                        foreach (var wh in Wheels)
                        {
                            if (wh.Mass < lightWheel.Mass) lightWheel = wh;
                        }
                        Wheels.Add(super.Wheel);
                        super.Wheel = lightWheel;
                        Wheels.Remove(lightWheel);
                    }
                    if (Batteries.Count > 0)
                    {
                        Battery lightBattery = Batteries[0];
                        foreach (var bat in Batteries)
                        {
                            if (bat.Mass < lightBattery.Mass) lightBattery = bat;
                        }
                        Batteries.Add(super.Battery);
                        super.Battery = lightBattery;
                        Batteries.Remove(lightBattery);
                    }
                    break;
            }

            SamokatWithTuning.Add(super);
        }
    }

    public List<SuperSamokat> MakeTuning(List<Samokat> samokats)
    {
        SamokatWithTuning.Clear();
        foreach (Samokat sm in samokats)
        {
            Console.WriteLine($"Тюнинг самоката {sm.GetID()} (доступно обвесов: {sm.Base.CountTun}):");
            var super = new SuperSamokat(sm.Engine, sm.Wheel, sm.Battery, sm.Base);
            int usedTun = 0;
            
            Console.WriteLine("Хотите тюнить этот самокат? (1/0)");
            string inputTune = Console.ReadLine();
            if (inputTune != null && inputTune == "1")
            {
                Console.WriteLine("Доступные двигатели:");
                for (int i = 0; i < Engines.Count; i++)
                {
                    Console.WriteLine($"{i}: {Engines[i].Power} Вт, {Engines[i].Cost} руб.");
                }
                Console.WriteLine("Выберите двигатель (номер) или -1 для пропуска:");
                if (int.TryParse(Console.ReadLine(), out int engIdx) && engIdx >= 0 && engIdx < Engines.Count)
                {
                    Engines.Add(super.Engine);
                    super.Engine = Engines[engIdx];
                    Engines.RemoveAt(engIdx);
                }
                Console.WriteLine("Доступные колеса:");
                for (int i = 0; i < Wheels.Count; i++)
                {
                    Console.WriteLine($"{i}: {Wheels[i].Type}, {Wheels[i].Cost} руб.");
                }
                Console.WriteLine("Выберите колеса (номер) или -1 для пропуска:");
                if (int.TryParse(Console.ReadLine(), out int wheelIdx) && wheelIdx >= 0 && wheelIdx < Wheels.Count)
                {
                    Wheels.Add(super.Wheel);
                    super.Wheel = Wheels[wheelIdx];
                    Wheels.RemoveAt(wheelIdx);
                }
                Console.WriteLine("Доступные батареи:");
                for (int i = 0; i < Batteries.Count; i++)
                {
                    Console.WriteLine($"{i}: {Batteries[i].Capacity} Вт/ч, {Batteries[i].Cost} руб.");
                }
                Console.WriteLine("Выберите батарею (номер) или -1 для пропуска:");
                if (int.TryParse(Console.ReadLine(), out int batIdx) && batIdx >= 0 && batIdx < Batteries.Count)
                {
                    Batteries.Add(super.Battery);
                    super.Battery = Batteries[batIdx];
                    Batteries.RemoveAt(batIdx);
                }
                Console.WriteLine($"Сколько дополнительных двигателей добавить? (0-{Math.Min(2, super.Base.CountTun - usedTun)})");
                if (int.TryParse(Console.ReadLine(), out int extraEng) && extraEng > 0 && extraEng <= Math.Min(2, super.Base.CountTun - usedTun))
                {
                    for (int i = 0; i < extraEng && Engines.Count > 0 && usedTun < super.Base.CountTun; i++)
                    {
                        Console.WriteLine("Выберите двигатель:");
                        for (int j = 0; j < Engines.Count; j++)
                        {
                            Console.WriteLine($"{j}: {Engines[j].Power} Вт");
                        }
                        if (int.TryParse(Console.ReadLine(), out int k) && k >= 0 && k < Engines.Count)
                        {
                            super.ExtraEngines.Add(Engines[k]);
                            Engines.RemoveAt(k);
                            usedTun++;
                        }
                    }
                }
                Console.WriteLine($"Сколько дополнительных батарей добавить? (0-{Math.Min(2, super.Base.CountTun - usedTun)})");
                if (int.TryParse(Console.ReadLine(), out int extraBat) && extraBat > 0 && extraBat <= Math.Min(2, super.Base.CountTun - usedTun))
                {
                    for (int i = 0; i < extraBat && Batteries.Count > 0 && usedTun < super.Base.CountTun; i++)
                    {
                        Console.WriteLine("Выберите батарею:");
                        for (int j = 0; j < Batteries.Count; j++)
                        {
                            Console.WriteLine($"{j}: {Batteries[j].Capacity} Вт/ч");
                        }
                        if (int.TryParse(Console.ReadLine(), out int k) && k >= 0 && k < Batteries.Count)
                        {
                            super.ExtraBatteries.Add(Batteries[k]);
                            Batteries.RemoveAt(k);
                            usedTun++;
                        }
                    }
                }
                Console.WriteLine("Добавить фару? (1/0)");
                string inputHeadlight = Console.ReadLine();
                super.HasHeadlight = inputHeadlight != null && inputHeadlight == "1";
                Console.WriteLine($"Добавить багажник? (1/0) (осталось обвесов: {super.Base.CountTun - usedTun})");
                string inputBaggage = Console.ReadLine();
                if (inputBaggage != null && inputBaggage == "1" && usedTun < super.Base.CountTun)
                {
                    super.HasBaggageRack = true;
                    usedTun++;
                }
                Console.WriteLine("Добавить стикеры? (1/0)");
                string inputStickers = Console.ReadLine();
                super.HasStickers = inputStickers != null && inputStickers == "1";
            }
            
            SamokatWithTuning.Add(super);
        }
        return SamokatWithTuning;
    }

    public void OTK()
    {
        var SamokatWTuning_copy = SamokatWithTuning;
        foreach (var samokat in SamokatWTuning_copy)
        {
            if (samokat.GetNewSpeed() < 10)
            {
                losses += samokat.GetCost();
                Console.WriteLine($"Самокат ID {samokat.GetID()} не прошел ОТК (скорость < 10 км/ч). Списан в убытки.");
                SamokatWithTuning.Remove(samokat);
            }
        }
    }

    public void Sale(List<Customer> customers, List<Samokat> samokats)
    {
        Samokat sm_for_sale = null;
        foreach (var cu in customers) 
        {
            if (samokats.Count == 0) sm_for_sale = null;
            switch (cu.Type)
            {
                case CustomerType.Student:
                    Samokat cheapest = samokats[0];
                    foreach (var sm in samokats)
                    {
                        if (sm.GetCost() < cheapest.GetCost()) cheapest = sm;
                    }
                    sm_for_sale = cheapest;
                    break;
                case CustomerType.RichKid:
                    Samokat mostExpensive = samokats[0];
                    foreach (var sm in samokats)
                    {
                        if (sm.Wheel.Type == WheelType.LightAlloy && sm.GetCost() > mostExpensive.GetCost())  mostExpensive = sm;
                    }
                    sm_for_sale = mostExpensive;
                    break;
                case CustomerType.Weak:
                    Samokat lightest = samokats[0];
                    foreach (var sm in samokats)
                    {
                        if (sm.Mass <= 30 && sm.Mass < lightest.Mass) lightest = sm;
                    }
                    sm_for_sale = lightest;
                    break;
                case CustomerType.Courier:
                    foreach (var sm in samokats)
                    {
                        if (sm.Wheel.Type == WheelType.AllTerrain) sm_for_sale = sm;
                    }
                    sm_for_sale = null;
                    break;
                case CustomerType.Racer:
                    Samokat fastest = samokats[0];
                    foreach (var sm in samokats)
                    {
                        if (sm.Speed > fastest.Speed) fastest = sm;
                    }
                    sm_for_sale = fastest;
                    break;
                default: 
                    sm_for_sale = samokats[0];
                    break;
            }
            if (sm_for_sale != null)
            {
                Console.WriteLine($"{cu.FIO} приобрел самокат с ID {sm_for_sale.GetID()}");
                profit += sm_for_sale.GetCost();
                soldSamokat++;
                samokats.Remove(sm_for_sale);
            }
            else
            {
                Console.WriteLine($"{cu.FIO} не приобрел самокат");
            }
        }
    }

    public void Balance()
    {
        Console.WriteLine("\n========== Отчет ==========");
        Console.WriteLine($"Проданных самокатов: {soldSamokat} шт.");
        Console.WriteLine($"Прибыль: {profit} руб.");
        Console.WriteLine($"Убытки: {losses} руб.");
        Console.WriteLine($"Чистая прибыль: {profit - losses} руб.");
        Console.WriteLine("=============================");
    }
}