namespace ConsoleApp7;

public class Engine
{
    public int ID { get; init; }
    public int Power;
    public decimal Cost;
    public int Mass;
    static int Engine_ID = 0;
    public Engine(int power, decimal cost, int mass)
    {
        ID = ++Engine_ID;
        Power = power;
        Cost = cost;
        Mass = mass;
    }
}