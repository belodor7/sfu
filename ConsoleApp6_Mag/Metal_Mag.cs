namespace ConsoleApp6_Mag;

public class Metal_Mag : Mag
{
    public override double Damage{ get; set;} = 20;
    public override double Health{ get; set;} = 100;
    public override Element Element{ get; init;} = Element.Metal;
    public override string AttackMessage{ get;}  = "РАЗБРОС ЖИДКИМ МЕТАЛЛОМ";
    public override int Train_Count{ get; set;} = 0;
    public override bool IsDeath{ get; set;} = false;
    public override Element KillsElement{ get; init;} = Element.Wood;
    public override void Train()
    {
        Train_Count += 1;
        Health *= Math.E;
        Console.WriteLine($"Металлический маг прокачался и теперь наносит {Damage} урона и имеет {Health} HP");
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
