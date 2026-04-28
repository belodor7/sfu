using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

[AttributeUsage(AttributeTargets.Property)]
public class HideFromReportAttribute : Attribute {}
public class ConsoleReporting
{
    private string GetDisplayValue(object value)
    {
        if (value == null)
        {
            return "Значение отсутствует";
        }
        
        return value.ToString();
    }

    private List<PropertyInfo> GetVisibleProperties<T>(IEnumerable<string> excludeProperties)
    {
        List<string> excludeList;
        if (excludeProperties == null)
        {
            excludeList = new List<string>();
        }
        else
        {
            excludeList = new List<string>(excludeProperties);
        }

        PropertyInfo[] allProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        List<PropertyInfo> visibleProperties = new List<PropertyInfo>();

        foreach (PropertyInfo prop in allProperties)
        {
            object[] attributes = prop.GetCustomAttributes(typeof(HideFromReportAttribute), false);
            bool isHidden = attributes.Length > 0;
            bool isExcluded = false;
            foreach (string excludeName in excludeList)
            {
                if (prop.Name.Equals(excludeName, StringComparison.OrdinalIgnoreCase))
                {
                    isExcluded = true;
                    break;
                }
            }

            if (!isHidden && !isExcluded)
            {
                visibleProperties.Add(prop);
            }
        }

        return visibleProperties;
    }

    public void GenerateReport<T>(IEnumerable<T> collection, IEnumerable<string> excludeProperties = null, string format = "Horizontal")
    {
        List<T> items = [.. collection];
        if (items.Count == 0)
        {
            Console.WriteLine("Коллекция пуста.");
            return;
        }
        List<PropertyInfo> visibleProperties = GetVisibleProperties<T>(excludeProperties);
        if (visibleProperties.Count == 0)
        {
            Console.WriteLine("Нет свойств для отображения.");
            return;
        }
        if (format.Equals("Vertical", StringComparison.OrdinalIgnoreCase))
        {
            OutputVertical(items, visibleProperties);
        }
        else
        {
            OutputHorizontal(items, visibleProperties);
        }
    }
    private void OutputVertical<T>(List<T> items, List<PropertyInfo> properties)
    {
        string typeName = typeof(T).Name;
        Console.WriteLine($"Отчет для {typeName} (вертикальный формат):");

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"Тип объекта: {typeName}");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(items[i]);
                string displayValue = GetDisplayValue(value);
                string propertyType = property.PropertyType.Name;
                Console.WriteLine($"{property.Name} ({propertyType}): {displayValue}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    private void OutputHorizontal<T>(List<T> items, List<PropertyInfo> properties)
    {
        string typeName = typeof(T).Name;
        Console.WriteLine($"\nОтчет для {typeName} (горизонтальный формат):");

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"Тип объекта: {typeName}");
            List<string> propertyStrings = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(items[i]);
                string displayValue = GetDisplayValue(value);
                propertyStrings.Add($"{property.Name}={displayValue}");
            }
            string resultLine = "";
            for (int j = 0; j < propertyStrings.Count; j++)
            {
                resultLine += propertyStrings[j];
                if (j < propertyStrings.Count - 1)
                {
                    resultLine += ", ";
                }
            }

            Console.WriteLine(resultLine);
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
public class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal Price { get; set; }

    [HideFromReport]
    public int Risk { get; set; }

    public string Color { get; set; }

    public Car(string brand, string model, int year, decimal price, int risk, string color)
    {
        Brand = brand;
        Model = model;
        Year = year;
        Price = price;
        Risk = risk;
        Color = color;
    }
}
public class House
{
    public string Address { get; set; }
    public int RoomsCount { get; set; }
    public decimal Area { get; set; }
    public int YearBuilt { get; set; }

    [HideFromReport]
    public int Risk { get; set; }

    public string Material { get; set; }
    public string Condition { get; set; }

    public House(string address, int roomsCount, decimal area, int yearBuilt, int risk, string material, string condition)
    {
        Address = address;
        RoomsCount = roomsCount;
        Area = area;
        YearBuilt = yearBuilt;
        Risk = risk;
        Material = material;
        Condition = condition;
    }
}

class Program
{
    static void Main()
    {
        ConsoleReporting reporting = new ConsoleReporting();
        Console.WriteLine("Страховка автомобилей");
        List<Car> cars = new List<Car>();
        cars.Add(new Car("BMW", "X5", 2022, 5500000, 35, "Черный"));
        cars.Add(new Car("Mercedes", "E-Class", 2021, 6200000, 28, "Серебристый"));
        cars.Add(new Car("Toyota", "Camry", 2023, 3500000, 22, "Белый"));
        cars.Add(new Car("Audi", "A8", 2020, 7100000, 32, "Красный"));

        Console.WriteLine("Горизонтальный формат (все свойства):");
        reporting.GenerateReport(cars, null, "Horizontal");

        Console.WriteLine("Вертикальный формат (без Brand):");
        List<string> excludeBrand = new List<string>();
        excludeBrand.Add("Brand");
        reporting.GenerateReport(cars, excludeBrand, "Vertical");

        Console.WriteLine("Горизонтальный формат (без Color и Price):");
        List<string> excludeColorPrice = new List<string>();
        excludeColorPrice.Add("Color");
        excludeColorPrice.Add("Price");
        reporting.GenerateReport(cars, excludeColorPrice, "Horizontal");

        Console.WriteLine("Страховка домов");
        List<House> houses = new List<House>();
        houses.Add(new House("ул. Ленина, 10", 3, 120.5m, 2010, 45, "Кирпич", "Отличное"));
        houses.Add(new House("пр. Мира, 25", 4, 180.0m, 2015, 30, "Монолит", "Хорошее"));
        houses.Add(new House("ул. Пушкина, 5", 2, 85.3m, 2005, 55, "Панель", "Требует ремонта"));
        houses.Add(new House(null, 5, 200.0m, 2020, 25, "Кирпич", "Идеальное"));

        Console.WriteLine("Вертикальный формат (все свойства):");
        reporting.GenerateReport(houses, null, "Vertical");

        Console.WriteLine("Горизонтальный формат (без Area):");
        List<string> excludeArea = new List<string>();
        excludeArea.Add("Area");
        reporting.GenerateReport(houses, excludeArea, "Horizontal");

        Console.WriteLine("Тест пустой коллекции");
        List<Car> emptyCars = new List<Car>();
        reporting.GenerateReport(emptyCars, null, "Horizontal");
    }
}