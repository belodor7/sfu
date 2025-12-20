namespace ConsoleApp7_1;
public class FactoryAF
{
    private List<Car> cars = new List<Car>();
    public void InfoFactory()
    {
        Console.WriteLine($"На фабрике АФ СФУ находится {cars.Count} автомобилей.\nЖелающих приобрести автомобиль: {Program.customers.Count}");
    }
    public void SaleCar()
    {
        var customersCopy = new List<Customer>(Program.customers);
        var carsCopy = new List<Car>(cars);
        foreach (var customer in customersCopy)
        {
            if (!Program.customers.Contains(customer)) continue;
            foreach (var car in carsCopy)
            {
                if (!cars.Contains(car) || customer.car.Engine.PedalSize != car.Engine.PedalSize) continue;
                cars.Remove(car);
                Program.customers.Remove(customer);
                Console.WriteLine($"Клиент {customer.FIO} приобрел автомобиль с серийным номером {car.SerialNumber}");
                break;
            }
            if ((Program.customers.Count == 0) && (cars.Count > 0))
            {
                Console.WriteLine($"Всем клиентам проданы автомобили.\nОставшиеся остатки ({cars.Count} шт.) на складе утилизируются.");
                cars.Clear();
            }
        }
    }
    public void AddCustomer(string FIO, int pedalSize)
    {
        Customer customer = new Customer();
        customer.FIO = FIO;
        customer.car = new Car(pedalSize);
        Program.customers.Add(customer);
        Console.WriteLine($"Клиент {customer.FIO} добавлен в очередь на покупку автомобиля с размером педалей {pedalSize}");
    }
    public void AddCar(int pedalSize)
    {
        Car Car = new Car(pedalSize);
        cars.Add(Car);
        Console.WriteLine($"Автомобиль с серийным номером {Car.SerialNumber} и размером педалей {pedalSize} произведен на фабрике АФ СФУ");
    }
}