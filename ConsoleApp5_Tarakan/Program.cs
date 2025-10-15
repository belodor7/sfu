class Tarakan
{
    Random rnd = new Random();
    private static int id;
    private static string name;
    private static string color;
    private double max_speed;
    private double stamina;
    private int training_count;
    public int Id
    {
        get
        {
            return id;
        }
        init
        {
            id = ;
        }
    }
    public string Name
    {
        get
        {
            return name;
        }
        init
        {
            name = value;
        }
    }
    public string Color
    {
        get
        {
            return color;
        }
        init
        {
            color = value;
        }
    }
    public double Max_speed
    {
        get
        {
            return max_speed;
        }
        set
        {
            max_speed = value;
        }
    }
    public double Stamina
    {
        get
        {
            return stamina;
        }
        set
        {
            stamina = value;
        }
    }
    public Tarakan(string name, string color, int max_speed, int stamina)
    {
        Name = name;
        Color = color;
        this.max_speed = max_speed;
        this.stamina = stamina;
        training_count = 3;
    }
    public Tarakan(string name, int max_speed, int stamina)
    {
        Name = name;
        this.max_speed = max_speed;
        this.stamina = stamina;
        training_count = 3;
    }
    public int Movement()
    {
        int speed = rnd.Next(0, (int)this.max_speed);
        return speed;
    }
    public void Training()
    {
        int death = rnd.Next(0, 4);
        int or = rnd.Next(0, 2);
        if (death == 3)
        {
            Console.WriteLine($"Таракана {Name} придавило штангой. RIP!");
            Program.tarakan_list.Remove(this);
        }
        switch (or)
        {
            case 0:
                Max_speed += (double)Max_speed * 0.20;
                Console.WriteLine($"Вкачали скорость таракану {Name}, теперь max_speed = {Max_speed}");
                break;
            case 1:
                Stamina += Stamina * 0.20;
                Console.WriteLine($"Вкачали выносливость таракану {Name}, теперь stamina = {Stamina}");
                break;
        }
        training_count--;
    }
}
class Totalizator
{
    private int balance;
    private int bid;
    public int Balance
    {
        get
        {
            return balance;
        }
        set
        {
            balance = value;
        }
    }
    public int Bid
    {
        get
        {
            return bid;
        }
        set
        {
            bid = value;
        }
    }
    public Totalizator(int balance, int bid){}
    public void Bet()
    {
        
    }
}

class Program
{
    public static List<Tarakan> tarakan_list = new List<Tarakan>();
    public static void Main()
    {
        
    }
}