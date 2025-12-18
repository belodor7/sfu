namespace ConsoleApp7;

public class SuperSamokat : Samokat
{
    public List<Engine> ExtraEngines = new List<Engine>();
    public List<Battery> ExtraBatteries = new List<Battery>();
    public bool HasHeadlight = false;
    public bool HasBaggageRack = false;
    public bool HasStickers = false;
    public decimal StickerCost = 1000;

    public SuperSamokat(Engine engine, Wheel wheel, Battery battery, Base base_r) : base(engine, wheel, battery, base_r)
    {
    }

    public double GetNewMass() 
    { 
        double engMass = 0;
        double batMass = 0;
        foreach (var eng in ExtraEngines) 
        {
            engMass += eng.Mass;
        }
        foreach (var bat in ExtraBatteries)
        {
            batMass += bat.Mass;
        }
        return base.Mass + engMass + batMass; 
    }
    public int GetTotalPower() 
    { 
        int exPow = 0;
        foreach (var eng in ExtraEngines) 
        {
            exPow += eng.Power;
        }
        return base.Engine.Power + exPow; 
    }
    public int GetTotalCapacity() 
    { 
        int exCap = 0;
        foreach (var bat in ExtraBatteries)
        {
            exCap += bat.Capacity;
        }
        return base.Battery.Capacity + exCap; 
    }
    public double GetNewSpeed() 
    { 
        return (GetTotalPower() * 10) / GetNewMass(); 
    }
    public double GetNewRange() 
    { 
        return (GetTotalCapacity() * 10) / GetNewMass(); 
    }
    public override decimal GetCost()
    {
        decimal totalCost = base.GetCost();
        foreach (var eng in ExtraEngines) 
        {
            totalCost += eng.Cost;
        }
        foreach (var bat in ExtraBatteries)
        {
            totalCost += bat.Cost;
        }
        if (HasHeadlight) totalCost += 500;
        if (HasBaggageRack) totalCost += 800;
        if (HasStickers) totalCost += StickerCost;
        return totalCost;
    }

    public override void PrintInfo()
    {
        base.PrintInfo();
        Console.WriteLine("Дополнительные компоненты:");
        Console.WriteLine($"Доп. двигатели: {ExtraEngines.Count}");
        Console.WriteLine($"Доп. аккумуляторы: {ExtraBatteries.Count}");
        Console.WriteLine($"Фара: {HasHeadlight}\nБагажник: {HasBaggageRack}\nСтикеры: {HasStickers}");
        Console.WriteLine($"Итоговая масса: {GetNewMass()} кг");
        Console.WriteLine($"Итоговая стоимость: {GetCost()} руб.");
        Console.WriteLine($"Итоговая скорость: {Math.Round(GetNewSpeed(), 2)} км/ч");
        Console.WriteLine($"Итоговая дальность: {Math.Round(GetNewRange(), 2)} км");
    }
}