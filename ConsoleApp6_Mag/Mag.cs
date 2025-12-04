namespace ConsoleApp6_Mag;

public class Mag
{
    public virtual double Damage{ get; set;}
    public virtual double Health{ get; set;}
    public virtual Element Element{ get; init;}
    public virtual string AttackMessage{ get;}
    public virtual int Train_Count{ get; set;}
    public virtual bool IsDeath{ get; set;}
    public virtual Element KillsElement{ get; init;}
    public virtual Element DeathElement{ get; init;}
    public virtual void Train()
    {
        Damage *= 1.2;
        Health *= 1.2;
        Console.WriteLine($"Стандартный маг прокачался и теперь наносит {Damage} урона и имеет {Health} HP");
    }
    public virtual void ShowInfo()
    {
        Console.WriteLine($"Маг c элементом {Element}, наносит {Damage} урона, имеет {Health} HP, спец.способность {AttackMessage}");
    }
}
