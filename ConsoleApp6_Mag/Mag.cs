namespace ConsoleApp6_Mag;

public class Mag
{
    public virtual double Damage{ get; set;}
    public virtual double Health{ get; set;}
    public virtual Element Element{ get; init;}
    public virtual string AttackMessage{ get; } = "";
    public virtual int Train_Count{ get; set;}
    public virtual bool IsDeath{ get; set;}
    public virtual Element KillsElement{ get; init;}
    public virtual void Train(){}
    public virtual void ShowInfo()
    {
        Console.WriteLine($"{StrElement(Element)} маг. Наносит {Damage} урона, имеет {Health} HP, спец.способность {AttackMessage}");
    }
    public virtual string StrElement(Element element)
    {
        switch (element)
        {
            case Element.Fire: return "Огненный";
            case Element.Wood: return "Деревянный";
            case Element.Water: return "Водный";
            case Element.Dirt: return "Земляной";
            case Element.Metal: return "Металлический";
            case Element.Normal: return "Обычный";
            default: return "";
        }
    }
}