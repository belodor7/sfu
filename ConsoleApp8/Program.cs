abstract public class Discipline
{
    public string Name { get; set; }
    public abstract string Check(Student student);
}

public interface IHaveAngryTeacher { }

public interface IHavePractice
{
    int PracticeCount { get; }
}

public interface IHaveFinalControl
{
    int PassingScore { get; }
}

public class Programming : Discipline, IHavePractice, IHaveAngryTeacher
{
    public int PracticeCount => 5;
    public override string Check(Student student)
    {
        if (this is IHaveAngryTeacher) return "Получить автомат невозможно.";
        if (student.Practices.ContainsKey(this) && student.Practices[this] >= PracticeCount) return "Студент получает автомат.";
        else return "Не все практические работы сданы.";
    }
}

public class History : Discipline, IHavePractice, IHaveFinalControl
{
    public int PracticeCount => 3;
    public int PassingScore => 70;
    public override string Check(Student student)
    {
        if (this is IHaveAngryTeacher) return "Получить автомат невозможно.";
        if (student.Practices.ContainsKey(this) && student.Practices[this] >= PracticeCount)
        {
            if (student.FinalControl.ContainsKey(this) && student.FinalControl[this] >= PassingScore) return "Студент получает автомат.";
            else return "Финальный тест не сдан.";
        }
        else return "Не все практические работы сданы.";
    }
}

public class MathAnalysis : Discipline, IHaveFinalControl
{
    public int PassingScore => 80;
    public override string Check(Student student)
    {
        if (this is IHaveAngryTeacher) return "Получить автомат невозможно.";
        if (student.FinalControl.ContainsKey(this) && student.FinalControl[this] >= PassingScore) return "Студент получает автомат.";
        else return "Финальный тест не сдан.";
    }
}

public class Student
{
    public string Name { get; set; }
    public Dictionary<Discipline, int> Practices { get; set; } = new Dictionary<Discipline, int>();
    public Dictionary<Discipline, int> FinalControl { get; set; } = new Dictionary<Discipline, int>();
}

class Program
{
    static void Main(string[] args)
    {
        var programming = new Programming { Name = "Программирование" };
        var history = new History { Name = "История" };
        var mathAnalysis = new MathAnalysis { Name = "Математический анализ" };
        Discipline[] disciplines = { programming, history, mathAnalysis };

        var student1 = new Student { Name = "Филипп Киркоров" };
        var student2 = new Student { Name = "Лариса Долина" };
        var student3 = new Student { Name = "Тупогубенький Бычок" };
        var student4 = new Student { Name = "Добавить Оператора" };
        var student5 = new Student { Name = "Семён Разумов"};
        Student[] students = { student1, student2, student3, student4, student5 };

        student1.Practices[programming] = 5;
        student1.Practices[history] = 3;
        student1.FinalControl[history] = 75;
        student1.FinalControl[mathAnalysis] = 85;

        student2.Practices[programming] = 4;
        student2.Practices[history] = 2;
        student2.FinalControl[history] = 60;
        student2.FinalControl[mathAnalysis] = 90;

        student3.Practices[programming] = 6;
        student3.Practices[history] = 4;
        student3.FinalControl[mathAnalysis] = 80;
        student3.FinalControl[history] = 80;

        student4.Practices[programming] = 1;
        student4.Practices[history] = 9;
        student4.FinalControl[mathAnalysis] = 80;
        student4.FinalControl[history] = 20;

        student5.Practices[programming] = 0;
        student5.Practices[history] = 5;
        student5.FinalControl[mathAnalysis] = 30;
        student5.FinalControl[history] = 1000;

        foreach (var student in students)
        {
            Console.WriteLine($"Отчёт для студента {student.Name}:");
            foreach (var discipline in disciplines)
            {
                Console.WriteLine($"{discipline.Name}: {discipline.Check(student)}");
            }
            Console.WriteLine();
        }
    }
} 