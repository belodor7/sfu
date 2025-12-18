namespace ConsoleApp7;

public class Wheel
{
    public WheelType Type = WheelType.Standard;
    public decimal Cost;
    public int Mass;

    public Wheel(WheelType type, decimal cost, int mass)
    {
        Type = type;
        Cost = cost;
        Mass = mass;
    }
    public static string StrEnum(WheelType type)
    {
        switch (type)
        {
            case WheelType.Standard: return "Стандартные";
            case WheelType.LightAlloy: return "Легкосплавные";
            case WheelType.AllTerrain: return "Вездеходные";
            default: return "";
        }
    }
}
