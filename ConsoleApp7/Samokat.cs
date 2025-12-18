namespace ConsoleApp7;

public class Samokat
{
    public Engine Engine;
    public Wheel Wheel;
    public Battery Battery;
    public Base Base;
    public double Mass => Engine.Mass + Wheel.Mass + Battery.Mass + Base.Mass;
    public double Speed => Engine.Power * 10 / Mass;
    public double Range => Battery.Capacity * 10 / Mass;

    public Samokat(Engine engine, Wheel wheel, Battery battery, Base base_r)
    {
        Engine = engine;
        Wheel = wheel;
        Battery = battery;
        Base = base_r;
    }
    public string GetID() 
    { 
        return $"S-{Base.ID}-{Engine.ID}";
    }
    public virtual decimal GetCost()
    {
        return Engine.Cost + Wheel.Cost + Battery.Cost + Base.Cost;
    }
    public virtual void PrintInfo()
    {
        Console.WriteLine($"Информация о самокате ID: {GetID()}");
        Console.WriteLine($"Рама: {Base.Type}, Масса: {Base.Mass}, Цена: {Base.Cost}");
        Console.WriteLine($"Двигатель: {Engine.Power} Вт, Масса: {Engine.Mass}, Цена: {Engine.Cost}");
        Console.WriteLine($"Колеса: {Wheel.Type}, Масса: {Wheel.Mass}, Цена: {Wheel.Cost}");
        Console.WriteLine($"Аккумулятор: {Battery.Capacity} Вт/ч, Масса: {Battery.Mass}, Цена: {Battery.Cost}");
        Console.WriteLine($"Общая масса: {Mass} кг");
        Console.WriteLine($"Стоимость: {GetCost()} руб.");
        Console.WriteLine($"Скорость: {Math.Round(Speed, 2)} км/ч");
        Console.WriteLine($"Дальность: {Math.Round(Range, 2)} км");
    }
}