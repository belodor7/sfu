using System;

public delegate void ProtectionFallHandler(object sender, ProtectionFallEventArgs e);

public class ProtectionFallEventArgs : EventArgs
{
    public int FalledProtectionLayersNumber { get; }
    public ProtectionSystem System { get; }

    public ProtectionFallEventArgs(int falledCount, ProtectionSystem system)
    {
        FalledProtectionLayersNumber = falledCount;
        System = system;
    }
}

public class ProtectionSystem
{
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public int ProtectionLayerNumber { get; set; }
    public int FalledProtectionLayerNumber { get; set; }

    private static readonly Random Rnd = new();

    public ProtectionSystem(string title, DateTime startDate, int layers)
    {
        Title = title;
        Date = startDate;
        ProtectionLayerNumber = layers;
        FalledProtectionLayerNumber = 0;
    }

    public virtual bool ProtectionCheck()
    {
        Date = Date.AddDays(1);
        return FalledProtectionLayerNumber < ProtectionLayerNumber;
    }

    public virtual void GetAttack()
    {
        if (FalledProtectionLayerNumber < ProtectionLayerNumber && Rnd.NextDouble() < 0.4)
            FalledProtectionLayerNumber++;
    }
}

public class Skyda
{
    public int FalledProtectionLayersNumber { get; set; }
    public int KnownFalledProtectionLayerNumber { get; set; }
    public ProtectionSystem ProtectionSystem { get; }
    public event ProtectionFallHandler ProtectionFall;

    public Skyda(ProtectionSystem system)
    {
        ProtectionSystem = system;
        KnownFalledProtectionLayerNumber = 0;
    }

    public virtual void Attack() => ProtectionSystem.GetAttack();

    public virtual void NotifyProtectionFall()
    {
        ProtectionSystem.ProtectionCheck();
        int currentFalled = ProtectionSystem.FalledProtectionLayerNumber;

        if (currentFalled > KnownFalledProtectionLayerNumber)
        {
            KnownFalledProtectionLayerNumber = currentFalled;
            FalledProtectionLayersNumber = currentFalled;
            ProtectionFall?.Invoke(this, new ProtectionFallEventArgs(currentFalled, ProtectionSystem));
        }
    }
}

public interface IReactProtectionFall
{
    int LayerReactorNumber { get; set; }
    string Message { get; set; }
    void OnProtectionFall(object sender, ProtectionFallEventArgs e);
    void Subscribe(Skyda skyda);
}

public class BasicLayerNotifier : IReactProtectionFall
{
    public int LayerReactorNumber { get; set; }
    public string Message { get; set; }

    public void Subscribe(Skyda skyda) => skyda.ProtectionFall += OnProtectionFall;

    public void OnProtectionFall(object sender, ProtectionFallEventArgs e)
    {
        if (e.FalledProtectionLayersNumber == LayerReactorNumber)
        {
            Message = $"[{e.System.Date:dd.MM.yyyy}] система '{e.System.Title}': пробит слой {LayerReactorNumber}";
            Console.WriteLine(Message);
        }
    }
}

public class EndLayerNotifier : IReactProtectionFall
{
    public int LayerReactorNumber { get; set; }
    public string Message { get; set; }
    private readonly DateTime _startDate;

    public EndLayerNotifier(int layerNumber, DateTime startDate)
    {
        LayerReactorNumber = layerNumber;
        _startDate = startDate;
    }

    public void Subscribe(Skyda skyda) => skyda.ProtectionFall += OnProtectionFall;

    public void OnProtectionFall(object sender, ProtectionFallEventArgs e)
    {
        if (e.FalledProtectionLayersNumber == LayerReactorNumber)
        {
            int daysPassed = (int)(e.System.Date - _startDate).TotalDays;
            Message = $"[{e.System.Date:dd.MM.yyyy}] полный взлом '{e.System.Title}'! дней с начала атаки: {daysPassed}";
            Console.WriteLine(Message);
        }
    }
}

class Program
{
    static void Main()
    {
        var sys = new ProtectionSystem("CoreDB", DateTime.Now, 5);
        var virus = new Skyda(sys);

        var n1 = new BasicLayerNotifier { LayerReactorNumber = 1 };
        var n3 = new BasicLayerNotifier { LayerReactorNumber = 3 };
        var nEnd = new EndLayerNotifier(5, sys.Date);

        n1.Subscribe(virus);
        n3.Subscribe(virus);
        nEnd.Subscribe(virus);

        Console.WriteLine("запускаем симуляцию...");
        int attempts = 0;
        while (sys.FalledProtectionLayerNumber < sys.ProtectionLayerNumber && attempts < 1000)
        {
            virus.Attack();
            virus.NotifyProtectionFall();
            attempts++;
        }
        Console.WriteLine($"симуляция отработала за {attempts} попыток");
    }
}