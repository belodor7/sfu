namespace ConsoleApp7;

public class Customer
{
    public string FIO;
    public bool License;
    public CustomerType Type;
    public Samokat Samokat;

    public Customer(string fio, bool license, CustomerType type)
    {
        FIO = fio;
        License = license;
        Type = type;
    }
    public static string StrEnum(CustomerType type)
    {
        switch (type)
        {
            case CustomerType.Student: return "Студент";
            case CustomerType.Weak: return "Дрищ";
            case CustomerType.RichKid: return "Мажор";
            case CustomerType.Courier: return "Курьер";
            case CustomerType.AnimeFan: return "Анимешник";
            case CustomerType.Racer: return "Гонщик";
            default: return "";
        }
    }
}