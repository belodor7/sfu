namespace ConsoleApp7;

public class Base
{
    public int ID;
    public FrameType Type; // Легкая, Средняя, Тяжелая
    public decimal Cost;
    public int Mass;
    public int CountTun;

    static int Base_ID = 0;

    public Base(FrameType type, decimal cost, int mass, int tun)
    {
        ID = ++Base_ID;
        Type = type;
        Cost = cost;
        Mass = mass;
        CountTun = tun;
    }
    public static string StrEnum(FrameType type)
    {
        switch (type)
        {
            case FrameType.Light: return "Легкая";
            case FrameType.Medium: return "Средняя";
            case FrameType.Heavy: return "Тяжелая";
            default: return "";
        }
    }
}