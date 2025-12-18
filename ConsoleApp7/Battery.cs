namespace ConsoleApp7;

public class Battery
{
    public int Capacity; // 500, 1000, 2000, 3000 Вт/ч
    public decimal Cost;
    public int Mass;

    public Battery(int capacity, decimal cost, int mass)
    {
        Capacity = capacity;
        Cost = cost;
        Mass = mass;
    }
}