using System.IO.Compression;

class Car
{
    private string color;
    private int avg_speed;
    public enum Brand
    {
        BMW,
        Toyota,
        Lada,
        Mercedes
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
