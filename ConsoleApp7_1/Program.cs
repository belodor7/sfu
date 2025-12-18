namespace ConsoleApp7_1;
public class Program
{
    public static FactoryAF factoryAF = new FactoryAF();
    public static void Main(string[] args)
    {
        while (true)
        {
            Menu();
        }
    }
    public static void Menu()
    {
        int choice;
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Добавить клиента");
        Console.WriteLine("2. Произвести автомобиль");
        Console.WriteLine("3. Продать всем желающим клиентам автомобили");
        Console.WriteLine("4. Информация о фабрике");
        Console.WriteLine("Введите номер действия:");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
        {
            Console.WriteLine("Некорректный ввод. Введите число от 1 до 4:");
        }
        switch (choice)
        {
            case 1:
                Console.Clear();
                Console.WriteLine("Введите ФИО клиента:");
                string FIO = Console.ReadLine();
                Console.WriteLine("Введите размер педалей автомобиля, который желает приобрести клиент:");
                int pedalSize = Convert.ToInt32(Console.ReadLine());
                factoryAF.AddCustomer(FIO, pedalSize);
                Next();
                break;
            case 2:
                int pedalSizeCar;
                Console.Clear();
                Console.WriteLine("Введите размер педалей автомобиля, который необходимо произвести:");
                while (!int.TryParse(Console.ReadLine(), out pedalSizeCar))
                {
                    Console.WriteLine("Некорректный ввод.");
                }
                factoryAF.AddCar(pedalSizeCar);
                Next();
                break;
            case 3:
                Console.Clear();
                Console.WriteLine("Состояние фабрики до продажи автомобилей:");
                factoryAF.InfoFactory();
                factoryAF.SaleCar();
                Console.WriteLine("Состояние фабрики после продажи автомобилей:");
                factoryAF.InfoFactory();
                Next();
                break;
            case 4:
                Console.Clear();
                factoryAF.InfoFactory();
                Next();
                break;
        }
    }
    public static void Next()
    {
        Console.WriteLine("Чтобы продолжить нажмите любую клавишу...");
        Console.ReadKey();
        Console.Clear();
    }
}
