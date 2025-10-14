class Cat{
    private string name;
    private int age;
    private int health;
    private int mood = 100;
    private string color;

    public string Name{
        get
        {
            return name;
        }
    }
    public int Age{
        get
        {
            return age;
        }
        init
        {
            age = value;
        }
    }
    public string Color{
        get
        {
            if (health < 0)
            {
                color = "white";
            }
            else color = "green";
            return color;
        }
    }
    public int Health{
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public int Mood{
        get
        {
            return mood;
        }
        set
        {
            mood = value;
        }
    }
    public Cat(string name, int age, int health)
    {
        this.name = name;
        Age = age;
        this.health = health;
    }
    public void Feed(int foodCount)
    {
        Health += foodCount;
        Mood += foodCount / 2;
        CheckMood();
    }
    public void Punish(int hit)
    {
        Health -= hit;
        Mood -= hit * 2;
        CheckMood();
    }
    public void Play(int moodCount)
    {
        Mood += moodCount;
        CheckMood();
    }
    private void CheckMood()
    {
        if (Mood < 0){
            Console.WriteLine($"{Name} нагадил в тапки. Возмездие за испорченное настроение!");
        }
        else if (Mood > 50){
            Console.WriteLine($"{Name} мурлычет. Настроение хозяина = 100");
        }
    }
    public void GetColor()
    {
        Console.WriteLine($"Цвет кошки: {Color}");
    }
}

class Program
{
    static void Main()
    {
        string name;
        int age;
        int health;
        int mood;
        int food;
        int punish;
        Console.WriteLine("Создайте своего кота! Введите поочередно имя, возраст, здоровье");
        name = Console.ReadLine();
        age = Convert.ToInt32(Console.ReadLine());
        health = Convert.ToInt32(Console.ReadLine());
        Cat cat = new Cat(name, age, health);
        while (cat.Health > 0){
            Console.WriteLine($"{cat.Name} говорит:\n- Поиграй со мной!\nСколько единиц настроения вы хотите добавить коту?");
            mood = Convert.ToInt32(Console.ReadLine());
            cat.Play(mood);
            Console.WriteLine($"{cat.Name} говорит:\n- Поиграли... Теперь я хочу есть!\nСколько еды вы хотите добавить коту?");
            food = Convert.ToInt32(Console.ReadLine());
            cat.Feed(food);
            Console.WriteLine($"{cat.Name} опять изодрал весь диван.\nНакажите {cat.Name}. Введите единицы наказания:");
            punish = Convert.ToInt32(Console.ReadLine());
            cat.Punish(punish);
            cat.GetColor();
        }
    }
}