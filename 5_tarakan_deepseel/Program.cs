using System;
using System.Collections.Generic;
using System.Linq;

class Tarakan
{
    Random rnd = new Random();
    private static int nextId = 1;
    private int id;
    private string name;
    private string color;
    private double max_speed;
    private double stamina;
    private int training_count;

    public int Id
    {
        get { return id; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Color
    {
        get { return color; }
        set { color = value; }
    }

    public double Max_speed
    {
        get { return max_speed; }
        set { max_speed = value; }
    }

    public double Stamina
    {
        get { return stamina; }
        set { stamina = value; }
    }

    public int Training_count
    {
        get { return training_count; }
    }

    public Tarakan(string name, string color, double max_speed, double stamina)
    {
        this.id = nextId++;
        this.name = name;
        this.color = color;
        this.max_speed = max_speed;
        this.stamina = stamina;
        this.training_count = 0;
    }

    public Tarakan(string name, double max_speed, double stamina) : this(name, "Неизвестный", max_speed, stamina)
    {
    }

    public double Movement()
    {
        double speed = rnd.NextDouble() * max_speed;
        return Math.Round(speed, 2);
    }

    public bool Training()
    {
        if (training_count >= 3)
        {
            Console.WriteLine($"Таракан {Name} уже достиг максимального количества тренировок!");
            return false;
        }

        int death = rnd.Next(0, 4);
        if (death == 3)
        {
            Console.WriteLine($"Таракана {Name} придавило штангой. RIP!");
            Program.tarakan_list.Remove(this);
            return false;
        }

        int or = rnd.Next(0, 2);
        switch (or)
        {
            case 0:
                Max_speed += Max_speed * 0.20;
                Console.WriteLine($"Вкачали скорость таракану {Name}, теперь max_speed = {Math.Round(Max_speed, 2)}");
                break;
            case 1:
                Stamina += Stamina * 0.20;
                Console.WriteLine($"Вкачали выносливость таракану {Name}, теперь stamina = {Math.Round(Stamina, 2)}");
                break;
        }
        training_count++;
        return true;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Имя: {Name}, Цвет: {Color}, Скорость: {Math.Round(Max_speed, 2)}, Выносливость: {Math.Round(Stamina, 2)}, Тренировки: {training_count}/3";
    }
}

class Totalizator
{
    private int balance;
    private int bid;
    private int selectedTarakanId;

    public int Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    public int Bid
    {
        get { return bid; }
        set { bid = value; }
    }

    public Totalizator()
    {
        this.balance = 1000;
        this.bid = 0;
        this.selectedTarakanId = -1;
    }

    public bool MakeBet(int betAmount, int tarakanId)
    {
        if (betAmount <= 0)
        {
            Console.WriteLine("Ставка должна быть положительной!");
            return false;
        }

        if (betAmount > balance)
        {
            Console.WriteLine("Недостаточно средств на счете!");
            return false;
        }

        // Поиск таракана только по ID (мертвых тараканов в списке уже нет)
        Tarakan foundTarakan = null;
        
        foreach (Tarakan t in Program.tarakan_list)
        {
            if (t.Id == tarakanId)
            {
                foundTarakan = t;
                break;
            }
        }

        if (foundTarakan == null)
        {
            Console.WriteLine("Таракан с таким ID не найден!");
            return false;
        }

        bid = betAmount;
        balance -= betAmount;
        selectedTarakanId = tarakanId;
        Console.WriteLine($"Ставка {bid} руб. на таракана {foundTarakan.Name} принята!");
        return true;
    }

    public void CalculateResult(bool isWin, int totalTarakanCount)
    {
        if (isWin)
        {
            int winAmount = bid * totalTarakanCount;
            balance += winAmount + bid;
            Console.WriteLine($"Поздравляем! Вы выиграли {winAmount} руб.!");
        }
        else
        {
            Console.WriteLine($"Вы проиграли {bid} руб.");
        }
        
        bid = 0;
        selectedTarakanId = -1;
    }

    public void ShowBalance()
    {
        Console.WriteLine($"Текущий баланс: {balance} руб.");
    }
}

class Program
{
    public static List<Tarakan> tarakan_list = new List<Tarakan>();
    private static Totalizator totalizator = new Totalizator();

    public static void Main()
    {
        tarakan_list.Add(new Tarakan("Спринтер", "Коричневый", 10.0, 8.0));
        tarakan_list.Add(new Tarakan("Усач", "Черный", 8.0, 12.0));
        tarakan_list.Add(new Tarakan("Молния", 12.0, 6.0));

        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTarakan();
                    break;
                case "2":
                    RemoveTarakan();
                    break;
                case "3":
                    TrainTarakan();
                    break;
                case "4":
                    ShowAllTarakans();
                    break;
                case "5":
                    totalizator.ShowBalance();
                    break;
                case "6":
                    StartRace();
                    break;
                case "0":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n=== Имитатор тараканьих бегов ===");
        Console.WriteLine("1. Добавить нового таракана");
        Console.WriteLine("2. Удалить таракана");
        Console.WriteLine("3. Тренировать таракана");
        Console.WriteLine("4. Показать всех тараканов");
        Console.WriteLine("5. Посмотреть состояние счета");
        Console.WriteLine("6. Начать гонку");
        Console.WriteLine("0. Выход");
        Console.Write("Выберите действие: ");
    }

    static void AddTarakan()
    {
        Console.Write("Введите имя таракана: ");
        string name = Console.ReadLine();

        Console.Write("Введите цвет таракана: ");
        string color = Console.ReadLine();

        Console.Write("Введите максимальную скорость: ");
        if (!double.TryParse(Console.ReadLine(), out double maxSpeed))
        {
            Console.WriteLine("Некорректная скорость!");
            return;
        }

        Console.Write("Введите выносливость: ");
        if (!double.TryParse(Console.ReadLine(), out double stamina))
        {
            Console.WriteLine("Некорректная выносливость!");
            return;
        }

        Tarakan newTarakan = new Tarakan(name, color, maxSpeed, stamina);
        tarakan_list.Add(newTarakan);
        Console.WriteLine($"Таракан {name} успешно добавлен!");
    }

    static void RemoveTarakan()
    {
        if (tarakan_list.Count == 0)
        {
            Console.WriteLine("Список тараканов пуст!");
            return;
        }

        ShowAllTarakans();
        Console.Write("Введите ID таракана для удаления: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Tarakan tarakanToRemove = null;
            foreach (Tarakan t in tarakan_list)
            {
                if (t.Id == id)
                {
                    tarakanToRemove = t;
                    break;
                }
            }

            if (tarakanToRemove != null)
            {
                tarakan_list.Remove(tarakanToRemove);
                Console.WriteLine($"Таракан с ID {id} удален!");
            }
            else
            {
                Console.WriteLine("Таракан с таким ID не найден!");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ID!");
        }
    }

    static void TrainTarakan()
    {
        if (tarakan_list.Count == 0)
        {
            Console.WriteLine("Список тараканов пуст!");
            return;
        }

        ShowAllTarakans();
        Console.Write("Введите ID таракана для тренировки: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Tarakan tarakanToTrain = null;
            foreach (Tarakan t in tarakan_list)
            {
                if (t.Id == id)
                {
                    tarakanToTrain = t;
                    break;
                }
            }

            if (tarakanToTrain != null)
            {
                tarakanToTrain.Training();
            }
            else
            {
                Console.WriteLine("Таракан с таким ID не найден!");
            }
        }
        else
        {
            Console.WriteLine("Некорректный ID!");
        }
    }

    static void ShowAllTarakans()
    {
        if (tarakan_list.Count == 0)
        {
            Console.WriteLine("Список тараканов пуст!");
            return;
        }

        Console.WriteLine("\n=== Список всех тараканов ===");
        foreach (var tarakan in tarakan_list)
        {
            Console.WriteLine(tarakan);
        }
    }

    static void StartRace()
    {
        if (tarakan_list.Count < 2)
        {
            Console.WriteLine("Для гонки нужно как минимум 2 таракана!");
            return;
        }

        Console.WriteLine("\n=== НАЧАЛО ГОНКИ ===");
        
        totalizator.ShowBalance();
        Console.Write("Введите размер ставки: ");
        
        if (!int.TryParse(Console.ReadLine(), out int betAmount) || betAmount <= 0)
        {
            Console.WriteLine("Некорректная ставка!");
            return;
        }

        ShowAllTarakans();
        Console.Write("Введите ID таракана, на которого ставите: ");
        
        if (!int.TryParse(Console.ReadLine(), out int tarakanId))
        {
            Console.WriteLine("Некорректный ID!");
            return;
        }

        if (!totalizator.MakeBet(betAmount, tarakanId))
        {
            return;
        }

        // Проводим гонку
        Console.WriteLine("\nГОНКА НАЧИНАЕТСЯ!");
        Dictionary<int, double> results = new Dictionary<int, double>();
        
        foreach (var tarakan in tarakan_list)
        {
            double speed = tarakan.Movement();
            results.Add(tarakan.Id, speed);
            Console.WriteLine($"Таракан {tarakan.Name} развил скорость: {speed}");
        }

        // Определяем победителя
        var winner = results.OrderByDescending(r => r.Value).First();
        var winnerTarakan = tarakan_list.First(t => t.Id == winner.Key);

        Console.WriteLine($"\nПОБЕДИТЕЛЬ: {winnerTarakan.Name} со скоростью {winner.Value}!");

        // Проверяем выигрыш
        bool isWin = winner.Key == tarakanId;
        totalizator.CalculateResult(isWin, tarakan_list.Count);
        totalizator.ShowBalance();
    }
}