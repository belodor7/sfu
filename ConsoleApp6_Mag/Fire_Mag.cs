namespace ConsoleApp6_Mag;

public class Fire_Mag : Mag
{
    public override double Damage{ get; set;} = 20;
    public override double Health{ get; set;} = 100;
    public override Element Element{ get; init;} = Element.Fire;
    public override string AttackMessage{ get;}  = "СЛАВЯНСКИЙ РОЗЖИГ ДРЕВЛЯНАМИ";
    public override int Train_Count{ get; set;} = 0;
    public override bool IsDeath{ get; set;} = false;
    public override Element KillsElement{ get; init;} = Element.Metal;
    public override void Train()
    {
        Train_Count += 1;
        Damage *= Math.E;
        Console.WriteLine($"Огненный маг прокачался и теперь наносит {Damage} урона и имеет {Health} HP");
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
