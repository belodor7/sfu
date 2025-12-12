public enum Brand
{
    BMW,
    Toyota,
    Lada,
    Mercedes
};
class Car
{
    private string color;
    private int avg_speed;
    private string direction;
    private Brand brand;
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
    public Brand Brand
    {
        get
        {
            return brand;
        }
        set
        {
            brand = value;
        }
    }
    public Car(string color, Brand brand)
    {
        this.color = color;
        this.brand = brand;
        this.avg_speed = 80;
        this.direction = "Прямо";
    }
    public void ChangeColor(string newColor)
    {
        Color = newColor;
        Console.WriteLine("Цвет машины изменен на " + newColor);
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
    public static void GetAllBrand()
    {
        Console.WriteLine("Список марок:");
        int k = 0;
        foreach (Brand brand in Enum.GetValues(typeof(Brand)))
        {
            k++;
            Console.WriteLine($"{k}. {brand}");
        }
    }
}
class Program
{
    static Car car = null;
    static void Main()
    {
        int select;
        while (true)
        {
            Menu();
            Console.WriteLine("Ваш выбор:");
            select = Convert.ToInt32(Console.ReadLine());
            switch (select)
            {
                case 1:
                    CreateCar();
                    break;
                case 2:
                    ChangeColor();
                    break;
                case 3:
                    UpgradeCar();
                    break;
                case 4:
                    car.GetDesign();
                    break;
                case 5:
                    ReturnDistance();
                    break;
                case 6:
                    ChangeDirection();
                    break;
                case 7:
                    car.Navigator();
                    break;
                case 8:
                    Car.GetAllBrand();
                    break;
                case 9:
                    return;
            }
        }
    }
    static void Menu()
    {
        Console.WriteLine("1. Создать машину");
        Console.WriteLine("2. Изменить цвет машины");
        Console.WriteLine("3. Улучшить скорость машины");
        Console.WriteLine("4. Получить цвет машины");
        Console.WriteLine("5. Рассчитать пройденное расстояние");
        Console.WriteLine("6. Изменить направление движения");
        Console.WriteLine("7. Показать навигацию");
        Console.WriteLine("8. Показать доступные марки машин");
        Console.WriteLine("9. Выход");
    }
    static void CreateCar()
    {
        if (CheckCar())
        {
            Console.WriteLine("Машина уже создана.");
        }
        else
        {
            string color;
            int brand_sel;
            Console.WriteLine("Введите цвет машины:");
            color = Console.ReadLine();
            Console.WriteLine("Выберите номер марки машины");
            Car.GetAllBrand();
            brand_sel = Convert.ToInt32(Console.ReadLine());
            car = new Car(color, (Brand)(brand_sel-1));
            Console.WriteLine("Машина создана!");
        }
        
    }
    static void ChangeColor()
    {
        if (CheckCar())
        {
            string color;
            Console.WriteLine("Введите новый цвет машины:");
            color = Console.ReadLine();
            car.ChangeColor(color);
        }
        else
        {
            Console.WriteLine("Машина еще не создана");
        }
    }
    static void UpgradeCar()
    {
        if (CheckCar())
        {
            int newSpeed;
            Console.WriteLine("Введите новую скорость машины:");
            newSpeed = Convert.ToInt32(Console.ReadLine());
            car.UpgradeCar(newSpeed);
        }
        else
        {
            Console.WriteLine("Машина еще не создана");
        }
    }
    static void ReturnDistance()
    {
        if (CheckCar())
        {
            int time;
            Console.WriteLine("Введите время в часах:");
            time = Convert.ToInt32(Console.ReadLine());
            car.ReturnDistance(time);
        }
        else
        {
            Console.WriteLine("Машина еще не создана");
        }
    }
    static void ChangeDirection()
    {
        if (CheckCar())
        {
            string direction;
            Console.WriteLine("Введите направление движения:");
            direction = Console.ReadLine();
            car.ChangeDirection(direction);
        }
        else
        {
            Console.WriteLine("Машина еще не создана");
        }
    }
    static bool CheckCar()
    {
        if (car == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
