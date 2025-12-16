namespace ConsoleApp7_1;
public class FactoryAF
{
    private List<Customer> customers = new List<Customer>();
    private List<Car> cars = new List<Car>();
    public void InfoFactory()
    {
        Console.WriteLine($"На фабрике АФ СФУ находится {cars.Count} автомобилей.\nЖелающих приобрести автомобиль: {customers.Count}");
    }
    public void SaleCar()
    {
        foreach (var customer in customers)
        {
            foreach (var car in cars)
            {
                if (customer.car == car)
                {
                    cars.Remove(car);
                    Console.WriteLine($"Клиент {customer.FIO} приобрел автомобиль с серийным номером {car.SerialNumber}");
                    break;
                }
            }

        }
    }
    public void AddCustomer(string FIO, int pedalSize)
    {
        Customer customer = new Customer();
        customer.FIO = FIO;
        customer.car = new Car(pedalSize);
        customers.Add(customer);
        Console.WriteLine($"Клиент {customer.FIO} добавлен в очередь на покупку автомобиля с размером педалей {pedalSize}");
    }
    public void AddCar(int pedalSize)
    {
        Car Car = new Car(pedalSize);
        cars.Add(Car);
        Console.WriteLine($"Автомобиль с серийным номером {Car.SerialNumber} и размером педалей {pedalSize} произведен на фабрике АФ СФУ");
    }
}