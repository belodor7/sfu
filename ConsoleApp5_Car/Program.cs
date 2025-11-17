class Car
{
    private string color;
    private int avg_speed;
    public enum Brand
    {
        BMW = 0,
        Toyota = 1,
        Lada = 2,
        Mercedes = 3
    };
    private string direction;
    public string Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
        }
    }
    public int AverageSpeedPerHour
    {
        get
        {
            return avg_speed;
        }
        set
        {
            avg_speed = value;
        }
    }
    public string Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }
    public Car(string color, string brand)
    {
        this.color = color;
        this.brand = brand;
        this.avg_speed = 80;
        this.direction = "Прямо";
    }
    public void ChangeColor(string newColor)
    {
        Color = color;
        Console.WriteLine("Цвет машины изменен на " + color);
    }
    public void UpgradeCar(int newSpeed)
    {
        AverageSpeedPerHour = newSpeed;
        Console.WriteLine($"Теперь скорость машины равна {AverageSpeedPerHour} км/ч");
    }
    public void GetDesign()
    {
        Console.WriteLine("Цвет машины: " + color);
    }
    public void ReturnDistance(int time)
    {
        Console.WriteLine($"Машина проехала {time * AverageSpeedPerHour} км");
    }
    public void ChangeDirection(string newDirection)
    {
        Direction = newDirection;
        Console.WriteLine($"Теперь машина едет в направлении {Direction}");
    }
    public void Navigator()
    {
        Console.WriteLine($"Сейчас мы едем {Direction} со средней скоростью {AverageSpeedPerHour} км/ч");
    }
    public void GetAllBrand()
    {
        Array values = Enum.GetValues(typeof(Brand));
        Console.WriteLine("Список марок:");
        foreach (Brand brand in values)
        {
            Console.WriteLine(brand);
        }
    }
}
class Program
{


    static void Main()
    {
        int menu;
        while (true)
        {
            Menu();
            menu = Convert.ToInt32(Console.ReadLine());
            switch (menu)
            {
                case 1:
                    CreateCar();
                    break;
                case 2:
                    ChangeColor();
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    return;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }
    static void Menu()
    {
        Console.WriteLine("1. Создать машину");
        Console.WriteLine("2. Изменить цвет машины");
        Console.WriteLine("3. Улучшиить скорость машины");
        Console.WriteLine("4. Получить цвет машины");
        Console.WriteLine("5. Рассчитать пройденное расстояние");
        Console.WriteLine("6. Изменить направление движения");
        Console.WriteLine("7. Показать навигацию");
        Console.WriteLine("8. Показать доступные марки машин");
        Console.WriteLine("9. Выход");
        Console.WriteLine("Выберите действие: ");
        return;
    }
    static void CreateCar()
    {

    }
    static void ChangeColor()
    {
        Console.WriteLine("Выберите новый цвет:");
        string newColor = Console.ReadLine();
    }
}