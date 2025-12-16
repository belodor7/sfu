namespace ConsoleApp7_1;
public class Car
{
    public Engine Engine { get; set; }
    public int SerialNumber { get; set; }
    public static int totalCars = 0;
    public Car(int pedalSize)
    {
        SerialNumber = ++totalCars;
        Engine = new Engine();
        Engine.PedalSize = pedalSize;
    }
}