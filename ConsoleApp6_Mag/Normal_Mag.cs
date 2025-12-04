namespace ConsoleApp6_Mag;

public class Normal_Mag : Mag
{
    public override double Damage{ get; set;} = 20;
    public override double Health{ get; set;} = 100;
    public override Element Element{ get; init;} = Element.Normal;
    public override string AttackMessage{ get;}  = "НОРМИСНЫЙ КРИК";
    public override int Train_Count{ get; set;} = 0;
    public override bool IsDeath{ get; set;} = false;
    public override Element KillsElement { get; init;} = Element.Null;
    public override void Train()
    {
        Train_Count += 1;
        Damage *= 1.2;
        Health *= 1.2;
        Console.WriteLine($"Обычный маг прокачался и теперь наносит {Damage} урона и имеет {Health} HP");
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
