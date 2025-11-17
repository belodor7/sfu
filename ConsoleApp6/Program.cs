class Employee
{
    public string Name { get; init; }
    public DateTime DateOfEmployment { get; init; }
    public int Salary { get; init; }
    public Employee(string name, DateTime dateTime, int salary)
    {
        Name = name;
        DateOfEmployment = dateTime;
        Salary = salary;
    }
    public string Say()
    {
        return Name;
    }
    public virtual int WorkTime()
    {
        return 0;
    }
    public virtual string WhatYouDo()
    {
        return "";
    }
}
class Cashier : Employee
{
    public override int WorkTime()
    {
        return (DateTime.Today - DateOfEmployment).Days;
    }
    public override string WhatYouDo()
    {
        return "Пополняю транспортные карты";
    }
    public Cashier(string name, DateTime dateTime, int salary): base(name, dateTime, salary)
    {
        
    }
}
class Operator : Employee
{
    public override int WorkTime()
    {
        return (DateTime.Today - DateOfEmployment).Days / 30;
    }
    public override string WhatYouDo()
    {
        return "Ищу посылку";
    }
    public Operator(string name, DateTime dateTime, int salary): base(name, dateTime, salary)
    {
        
    }
}
class Postman : Employee
{
    public override int WorkTime()
    {
        return (DateTime.Today - DateOfEmployment).Days / 365;
    }
    public override string WhatYouDo()
    {
        return "Разношу почту, не мешайте";
    }
    public Postman(string name, DateTime dateTime, int salary): base(name, dateTime, salary)
    {
        
    }
}
class PostOffice
{
    public static List<Employee> Employees { get; set; } = new List<Employee>();
    public static void Poll()
    {
        foreach (Employee employee in Employees)
        {
            Console.WriteLine("А как вас зовут?");
            Console.WriteLine(employee.Say());
            Console.WriteLine("А что вы делаете?");
            Console.WriteLine(employee.WhatYouDo());
            Console.WriteLine("И давно вы тут работаете?");
            Console.WriteLine(employee.WorkTime());
            Console.WriteLine("Сколько вы получаете?");
            Console.WriteLine(employee.Salary);
        }
    }
    public static void Stat()
    {
        int count_cashiers = 0;
        int sum_cashiers = 0;
        int count_operators = 0;
        int sum_operators = 0;
        int count_postmans = 0;
        int sum_postmans = 0;
        foreach (Employee employee in Employees)
        {
            if (employee is Cashier)
            {
                sum_cashiers = sum_cashiers + employee.Salary;
                count_cashiers++;
            }
            if (employee is Operator)
            {
                sum_operators = sum_operators + employee.Salary;
                count_operators++;
            }
            if (employee is Postman)
            {
                sum_postmans = sum_postmans + employee.Salary;
                count_postmans++;
            }
        }
        Console.WriteLine($"Средняя зарплата кассиров: {sum_cashiers / count_cashiers}");
        Console.WriteLine($"Средняя зарплата операторов: {sum_operators / count_operators}");
        Console.WriteLine($"Средняя зарплата почтальонов: {sum_postmans / count_postmans}");
    }
}
class Program
{
    static void Main()
    {
        int select;
        while (true)
        {
            Console.WriteLine("1. Добавить сотрудника");
            Console.WriteLine("2. Показать статистику");
            Console.WriteLine("3. Провести опрос");
            Console.WriteLine("4. Выход");
            select = Convert.ToInt32(Console.ReadLine());
            switch (select)
            {
                case 1:
                    AddPostWorker();
                    break;
                case 2:
                    PostOffice.Stat();
                    break;
                case 3:
                    PostOffice.Poll();
                    break;
                case 4:
                    return;
            }
        }
    }
    static void AddPostWorker()
    {
        int select;
        string name;
        DateTime dateTime;
        int salary;
        Console.WriteLine("Добавьте нового сотрудника");
        Console.WriteLine("Введите имя сотрудника");
        name = Console.ReadLine();
        Console.WriteLine("Введите дату приема на работу (формат: ДД.ММ.ГГГГ)");
        dateTime = Convert.ToDateTime(Console.ReadLine());
        Console.WriteLine("Введите зарплату сотрудника");
        salary = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Выберите должность сотрудника");
        Console.WriteLine("1. Кассир");
        Console.WriteLine("2. Оператор");
        Console.WriteLine("3. Почтальон");
        select = Convert.ToInt32(Console.ReadLine());
        switch (select)
        {
            case 1:
                PostOffice.Employees.Add(new Cashier(name, dateTime, salary));
                break;
            case 2:
                PostOffice.Employees.Add(new Operator(name, dateTime, salary));
                break;
            case 3:
                PostOffice.Employees.Add(new Postman(name, dateTime, salary));
                break;
        }
    }
}