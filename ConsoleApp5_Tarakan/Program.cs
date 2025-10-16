using System;
class Tarakan
{
    Random rnd = new Random();
    private static int add_id = 1;
    private int id;
    private string name;
    private string color;
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
            id = value;
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
    public int Training_count
    {
        get
        {
            return training_count;
        }
        set
        {
            training_count = value;
        }
    }
    public Tarakan(string name, string color, double max_speed, double stamina)
    {
        this.id = add_id++;
        this.name = name;
        this.color = color;
        this.max_speed = max_speed;
        this.stamina = stamina;
        this.training_count = 3;
    }
    public double Movement()
    {
        double speed = rnd.NextDouble() * Max_speed;
        return Math.Round(speed, 2);
    }
    public void Training()
    {
        if (training_count == 0 || training_count < 0)
        {
            Console.WriteLine($"Запас тренировок исчерпан. Таракан {Name} больше не может тренироваться.");
        }
        else
        {
            int death = rnd.Next(0, 4);
            if (death == 3)
            {
                Console.WriteLine($"Таракана {Name} придавило штангой. RIP!");
                Program.tarakan_list.Remove(this);
            }
            else
            {
                int or = rnd.Next(0, 2);
                switch (or)
                {
                    case 0:
                        Max_speed += Max_speed * 0.20;
                        Console.WriteLine($"Вкачали скорость таракану {Name}, теперь max_speed = {Math.Round(Max_speed, 2)}");
                        Training_count--;
                        break;
                    case 1:
                        Stamina += Stamina * 0.20;
                        Console.WriteLine($"Вкачали выносливость таракану {Name}, теперь stamina = {Math.Round(Stamina, 2)}");
                        Training_count--;
                        break;
                }
            }
        }
    }
    public void InfoID(int id)
    {
        bool found = false;
        foreach (var t in Program.tarakan_list)
        {
            if (t.Id == id)
            {
                Console.WriteLine($"ID: {t.Id}; Имя: {t.Name}; Цвет: {t.Color}; Макс. скорость: {t.Max_speed}; Выносливость: {t.Stamina}; Запас тренировок: {t.Training_count}");
                found = true;
            }
        }
        if (found == false)
        {
            Console.WriteLine("Такого таракана с таким ID нет!");
        }
    }

}
class Totalizator
{
    private int balance = 1000;
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
    public void Bet()
    {
        int bid;
        int id;
        bool found = false;
        Console.WriteLine("Введите ставку:");
        bid = Convert.ToInt32(Console.ReadLine());
        while ((bid > balance) || (bid <= 0))
        {
            if (bid <= 0)
            {
                Console.WriteLine("Ставка должна быть положительной!");
            }
            else
            {
                Console.WriteLine("Недостаточно средств на счете!");
            }
            Console.WriteLine("Введите ставку:");
            bid = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("Введите ID таракана, на которого производится ставка:");
        id = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < Program.tarakan_list.Count; i++)
        {
            if (Program.tarakan_list[i].Id == id)
            {
                Console.WriteLine("Информация о таракане:");
                Program.tarakan_list[i].InfoID(id);
                balance -= bid;
                Console.WriteLine($"Ставка {bid} рублей на таракана {id} сделана!");
                found = true;
                StartRace(id, bid);
            }
        }
        if (!found)
        {
            Console.WriteLine("Таракана с таким ID нет!");
        }
    }
    public void StartRace(int id, int bid)
    {
        double max_speed = 0;
        int id_win = -1;
        Console.WriteLine("### ГОНКА НАЧАЛАСЬ! ###");
        foreach (var t in Program.tarakan_list)
        {
            double speed = t.Movement();
            Console.WriteLine($"Таракан {t.Name} (ID: {t.Id}): {speed} км/ч");
            if (speed > max_speed)
            {
                max_speed = speed;
                id_win = t.Id;
            }
            if (t.Id == id)
            {
                Console.WriteLine($"Таракан {t.Name} с ID {t.Id} начал гонку!");
                Console.WriteLine($"Скорость таракана: {Math.Round(speed, 2)}");
            }
        }
        Console.WriteLine("### ГОНКА ЗАВЕРШЕНА! ###");
        Console.WriteLine($"Таракан с ID {id_win} выиграл гонку!");
        if (id_win == id)
        {
            Console.WriteLine($"Вы выиграли {bid * Program.tarakan_list.Count} рублей!");
            balance += bid * Program.tarakan_list.Count;
        }
        else
        {
            Console.WriteLine("Ваша ставка не сыграла!");
            Console.WriteLine($"Вы проиграли {bid} рублей!");
        }
    }
    public void ShowBalance()
    {
        Console.WriteLine($"Баланс: {balance} рублей");
    }
}

class Program
{
    public static List<Tarakan> tarakan_list = new List<Tarakan>();
    public static Totalizator totalizator = new Totalizator();
    public static void Main()
    {
        int menu;
        while (true)
        {
            Console.WriteLine("Меню:\n1. Добавить таракана\n2. Удалить таракана\n3. Показать всех тараканов\n4. Потренировать таракана\n5. Сделать ставку и запустить гонку\n6. Посмотреть состояние счета\n7. Выход");
            menu = Convert.ToInt32(Console.ReadLine());
            switch (menu)
            {
                case 1:
                    AddTarakan();
                    break;
                case 2:
                    DeleteTarakan();
                    break;
                case 3:
                    ShowAllTarakans();
                    break;
                case 4:
                    TrainTarakan();
                    break;
                case 5:
                    MakeBet();
                    break;
                case 6:
                    totalizator.ShowBalance();
                    break;
                case 7:
                    return;
            }
        }
    }
    public static void AddTarakan()
    {
        string name;
        string color;
        int max_speed;
        int stamina;
        Console.WriteLine("Введите имя таракана:");
        name = Console.ReadLine();
        while (name == "")
        {
            Console.WriteLine("Поле не может быть пустым!");
            Console.WriteLine("Введите имя таракана:");
            name = Console.ReadLine();
        }
        Console.WriteLine("Введите цвет таракана:");
        color = Console.ReadLine();
        while (color == "")
        {
            Console.WriteLine("Поле не может быть пустым!");
            Console.WriteLine("Введите цвет таракана:");
            color = Console.ReadLine();
        }
        Console.WriteLine("Введите максимальную скорость таракана:");
        max_speed = Convert.ToInt32(Console.ReadLine());
        while (max_speed <= 0)
        {
            Console.WriteLine("Значение не может быть меньше или равно 0!");
            Console.WriteLine("Введите максимальную скорость таракана:");
            max_speed = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("Введите выносливость таракана:");
        stamina = Convert.ToInt32(Console.ReadLine());
        while (stamina <= 0)
        {
            Console.WriteLine("Значение не может быть меньше или равно 0!");
            Console.WriteLine("Введите выносливость таракана:");
            stamina = Convert.ToInt32(Console.ReadLine());
        }
        tarakan_list.Add(new Tarakan(name, color, max_speed, stamina));
        Console.WriteLine("Таракан создан! Присвоен ID: " + tarakan_list[tarakan_list.Count - 1].Id);
    }
    public static void DeleteTarakan()
    {
        int id;
        bool found = false;
        Console.WriteLine("Введите ID таракана для удаления:");
        id = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < tarakan_list.Count; i++)
        {
            if (tarakan_list[i].Id == id)
            {
                tarakan_list.RemoveAt(i);
                Console.WriteLine($"Таракан с ID {id} удален.");
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("Таракана с таким ID нет.");
        }
    }
    public static void ShowAllTarakans()
    {
        if (tarakan_list.Count == 0)
        {
            Console.WriteLine("Список тараканов пуст!");
        }
        else
        {
            foreach (var t in tarakan_list)
            {
                Console.WriteLine($"ID: {t.Id}; Имя: {t.Name}; Цвет: {t.Color}; Макс. скорость: {t.Max_speed}; Выносливость: {t.Stamina}; Запас тренировок: {t.Training_count}");
            }
        }
    }
    public static void TrainTarakan()
    {
        int id;
        bool found = false;
        Console.WriteLine("Введите ID таракана для тренировки:");
        Console.WriteLine("Чтобы посмотреть список тараканов, введите 0:");
        id = Convert.ToInt32(Console.ReadLine());
        if (id == 0)
        {
            ShowAllTarakans();
            TrainTarakan();
        }
        foreach (var t in tarakan_list)
        {
            if (t.Id == id)
            {
                t.Training();
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("Таракана с таким ID нет.");
        }
    }
    public static void MakeBet()
    {
        if (tarakan_list.Count < 2)
        {
            Console.WriteLine("Недостаточно тараканов для гонки!");
        }
        else
        {
            Console.WriteLine("Количество тараканов допустимо для гонки: " + tarakan_list.Count);
            totalizator.Bet();
        }
    }
}