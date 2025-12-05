namespace ConsoleApp6_Mag;

public class Wood_Mag : Mag
{
    public override double Damage{ get; set;} = 20;
    public override double Health{ get; set;} = 100;
    public override Element Element{ get; init;} = Element.Wood;
    public override string AttackMessage{ get;} = "ЗАНОЗЫ";
    public override bool IsDeath{ get; set;} = false;
    public override Element KillsElement{ get; init;} = Element.Dirt;
    public override void Train()
    {
        Console.WriteLine("Деревянный маг не любит тренировки.");
    }
    public override void ShowInfo()
    {
        base.ShowInfo();
    }
    public override string StrElement(Element element)
    {
        return base.StrElement(element);
    }
}
