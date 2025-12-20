namespace ConsoleApp7;

class Program
{
    static Factory factory = new Factory();
    static List<Customer> customers = new List<Customer>();
    static List<Samokat> samokats= new List<Samokat>();
    static void Main(string[] args)
    {
        int key;
        while (true)
        {
            Menu();
            while (!int.TryParse(Console.ReadLine(), out key))
            {
                Console.WriteLine("Некорректный ввод.");
            }
            switch (key)
            {
                case 1:
                    AddCustomer();
                    break;
                case 2:
                    AddSamokat();
                    break;
                case 3:
                    factory.Sale(customers, samokats);
                    Next();
                    break;
                case 4:
                    Console.WriteLine("Выберите режим тюнинга: 1 - Ручной, 2 - Автоматический");
                    string mode = Console.ReadLine();
                    if (mode == "1")
                    {
                        factory.MakeTuning(samokats);
                    }
                    else if (mode == "2")
                    {
                        factory.AutoTuneSamokats(customers);
                    }
                    Next();
                    break;
                case 5:
                    factory.CreateDetails();
                    Next();
                    break;
                case 6:
                    AllCustomers();
                    break;
                case 7:
                    AllSamokat();
                    break;
                case 8:
                    AllDetails();
                    break;
                case 9:
                    factory.OTK();
                    Next();
                    break;
                case 0:
                    return;
            }
        }
    }
    static void Menu()
    {
        Console.Clear();
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Добавить клиента");
        Console.WriteLine("2. Произвести самокат");
        Console.WriteLine("3. Продать всем желающим клиентам самокаты");
        Console.WriteLine("4. Тюнинговать самокат");
        Console.WriteLine("5. Произвести детали");
        Console.WriteLine("6. Список клиентов");
        Console.WriteLine("7. Список самокатов");
        Console.WriteLine("8. Список деталей");
        Console.WriteLine("9. ОТК");
        Console.WriteLine("0. Выход");
    }
    static void AddCustomer()
    {
        string FIO;
        bool License;
        int k;
        CustomerType type;
        Console.Clear();
        Console.WriteLine("Введите ФИО клиента: ");
        FIO = Console.ReadLine();
        Console.WriteLine("Есть ли у клиента права? (0 - нет, 1 - да)");
        while (!int.TryParse(Console.ReadLine(), out k) || k > 1)
        {
            Console.WriteLine("Некорректный ввод.");
        }
        License = Convert.ToBoolean(k);
        k = 0;
        Console.WriteLine("Выберите тип клиента:");
        foreach (CustomerType type_all in Enum.GetValues(typeof(CustomerType)))
        {
            k++;
            Console.WriteLine($"{k}. {Customer.StrEnum(type_all)}");
        }
        k = 0;
        while (!int.TryParse(Console.ReadLine(), out k) || k > 6)
        {
            Console.WriteLine("Некорректный ввод.");
        }
        switch (k)
        {
            case 1:
                type = CustomerType.Student;
                break;
            case 2:
                type = CustomerType.Weak;
                break;
            case 3:
                type = CustomerType.RichKid;
                break;
            case 4:
                type = CustomerType.Courier;
                break;
            case 5:
                type = CustomerType.AnimeFan;
                break;
            case 6:
                type = CustomerType.Racer;
                break;
            default:
                type = CustomerType.Student;
                break;
        }
        customers.Add(new Customer(FIO, License, type));
        Next();
    }
    static void AllCustomers()
    {
        foreach (var customer in customers)
        {
            Console.WriteLine($"ФИО: {customer.FIO}\tНаличие прав: {customer.License}\tТип: {Customer.StrEnum(customer.Type)}");
        }
        Next();
    }
    static void AddSamokat()
    {
        int pow_eng = 0;
        decimal price_eng = 0;
        int mass_eng = 0;
        WheelType wheelType;
        decimal price_wh = 0;
        int mass_wh = 0;
        int bat_cap = 0;
        decimal price_bat = 0;
        int mass_bat = 0;
        FrameType frameType;
        decimal price_fr = 0;
        int mass_fr = 0;
        int obv_fr = 0;
        int k;
        Console.Clear();
        Console.WriteLine("Выберите мощность двигателя:\n1. 250 W - 10 000 руб.\n2. 500 W - 13 000 руб.\n3. 750 W - 14 800 руб.");
        while (!int.TryParse(Console.ReadLine(), out k) || k > 3)
        {
            Console.WriteLine("Некорректный ввод.");
        }
        switch (k)
        {
            case 1:
                pow_eng = 250;
                price_eng = 10000;
                mass_eng = 7;
                break;
            case 2:
                pow_eng = 500;
                price_eng = 13000;
                mass_eng = 10;
                break;
            case 3:
                pow_eng = 750;
                price_eng = 14800;
                mass_eng = 15;
                break;
        }
        Console.Clear();
        k = 0;
        Console.WriteLine("Выберите тип колеса:\n1. Стандартное - 7 000 руб.\n2. Легкославное - 10 000 руб.\n3. Вездеходное - 9 000 руб.");
        while (!int.TryParse(Console.ReadLine(), out k) || k > 3)
        {
            Console.WriteLine("Некорректный ввод.");
        }
        switch (k)
        {
            case 1:
                wheelType = WheelType.Standard;
                price_wh = 7000;
                mass_wh = 4;
                break;
            case 2:
                wheelType = WheelType.LightAlloy;
                price_wh = 10000;
                mass_wh = 3;
                break;
            case 3:
                wheelType = WheelType.AllTerrain;
                price_wh = 9000;
                mass_wh = 6;
                break;
            default:
                wheelType = WheelType.Standard;
                price_wh = 7000;
                mass_wh = 4;
                break;
        }
        Console.Clear();
        k = 0;
        Console.WriteLine($"Выберите ёмкость аккумулятора:\n1. 1000 Wh - 3 000 руб.\n2. 2000 Wh - 4 500 руб.");
        while (!int.TryParse(Console.ReadLine(), out k) || k > 2)
        {
            Console.WriteLine("Некорректный ввод.");
        }
        switch (k)
        {
            case 1:
                bat_cap = 1000;
                price_bat = 3000;
                mass_bat = 3;
                break;
            case 2:
                bat_cap = 2000;
                price_bat = 4500;
                mass_bat = 5;
                break;
        }
        Console.Clear();
        k = 0;
        Console.WriteLine($"Выберите тип рамы:\n1. Легкая (3 обвеса) - 5 000 руб.\n2. Средняя (5 обвесов) - 7 000 руб.\n3. Тяжелая (7 обвесов)- 10 000 руб.");
        while (!int.TryParse(Console.ReadLine(), out k) || k > 3)
        {
            Console.WriteLine("Некорректный ввод.");
        }
        switch (k)
        {
            case 1:
                frameType = FrameType.Light;
                price_fr = 5000;
                mass_fr = 7;
                obv_fr = 3;
                break;
            case 2:
                frameType = FrameType.Medium;
                price_fr = 7000;
                mass_fr = 10;
                obv_fr = 5;
                break;
            case 3:
                frameType = FrameType.Heavy;
                price_fr = 10000;
                mass_fr = 15;
                obv_fr = 7;
                break;
            default:
                frameType = FrameType.Medium;
                price_fr = 7000;
                mass_fr = 10;
                obv_fr = 5;
                break;
        }
        Console.Clear();
        Console.WriteLine($"Характеристики выбранного самоката:\nМощность двигателя: {pow_eng} W\nЁмкость аккумулятора: {bat_cap} Wh\nТип колес: {Wheel.StrEnum(wheelType)}\nТип рамы: {Base.StrEnum(frameType)}");
        samokats.Add(new Samokat(new Engine(pow_eng, price_eng, mass_eng), new Wheel(wheelType, price_wh, mass_wh), new Battery(bat_cap, price_bat, mass_bat), new Base(frameType, price_fr, mass_fr, obv_fr)));
        Console.WriteLine("Самокат произведен!");
        Next();
    }
    static void AllSamokat()
    {
        Console.Clear();
        Console.WriteLine("Список самокатов:");
        foreach (Samokat sm in samokats)
        {
            sm.PrintInfo();
            Console.WriteLine();
        }
        if (factory.SamokatWithTuning.Count > 0)
        {
            Console.WriteLine("Список тюнингованных самокатов:");
            foreach (var sm_tun in factory.SamokatWithTuning)
            {
                sm_tun.PrintInfo();
                Console.WriteLine();
            }
        }
        Next();
    }
    static void AllDetails()
    {
        Console.Clear();
        Console.WriteLine("Список доступных деталей:");
        Console.WriteLine("Двигатели:");
        foreach (var eng in factory.Engines)
        {
            Console.WriteLine($"Мощность: {eng.Power} W, Цена: {eng.Cost} руб., Масса: {eng.Mass} кг");
        }
        Console.WriteLine("Колеса:");
        foreach (var wh in factory.Wheels)
        {
            Console.WriteLine($"Тип: {Wheel.StrEnum(wh.Type)}, Цена: {wh.Cost} руб., Масса: {wh.Mass} кг");
        }
        Console.WriteLine("Батареи:");
        foreach (var bat in factory.Batteries)
        {
            Console.WriteLine($"Ёмкость: {bat.Capacity} Wh, Цена: {bat.Cost} руб., Масса: {bat.Mass} кг");
        }
        Next();
    }
    static void Next()
    {
        Console.WriteLine("Чтобы продолжить нажмите любую клавишу...");
        Console.ReadKey();
        Console.Clear();
    }
    
}