// 1 задание
Console.WriteLine("1 задание");
Console.WriteLine("Как вас зовут?");
string name = Console.ReadLine();
Console.WriteLine("Какой язык программирования вы раньше изучали?");
string lang = Console.ReadLine();

Console.WriteLine($"Я {name}, я уже знаю {lang}!");

// 2 задание
Console.WriteLine("2 задание");
Console.WriteLine("Введите координату X, затем координату Y:");
float x = Convert.ToSingle(Console.ReadLine());
float y = Convert.ToSingle(Console.ReadLine());

double angle = Math.Atan2(y, x) * 180 / Math.PI;
Console.WriteLine($"Угол наклона в градусах составляет: {angle}");

// 3 задание
Console.WriteLine("3 задание");
int len = 163;
Console.WriteLine("Введите скорость автомобиля:");
float speed = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Введите время движения автомобиля:");
float time = Convert.ToSingle(Console.ReadLine());
float dist = speed * time;
int mark = (int)dist;
int lap = 0;
while (mark > len)
    {
    lap++;
    mark = mark - len;
    }
Console.WriteLine($"Автомобиль остановился на отметке {mark} км\nПройденых кругов: {lap}");

// 4 задание
Console.WriteLine("4 задание");
Random random = new Random();

Console.WriteLine("Введите первое число:");
int first = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите второе число:");
int second = Convert.ToInt32(Console.ReadLine());
int ran = random.Next(first, second);
Console.WriteLine($"Случайное число между {first} и {second} - {ran}");

// 5 задание
Console.WriteLine("5 задание");
Console.WriteLine("Введите число a:");
float a = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Введите число b:");
float b = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Введите число c:");
float c = Convert.ToSingle(Console.ReadLine());
Console.WriteLine("Введите число d:");
float d = Convert.ToSingle(Console.ReadLine());

float z = ((a / c) * (b / d)) - (((a * b) - c) / (c * d)) + (float)Math.Sqrt(d);
Console.WriteLine(z);

// 6 задание
Console.WriteLine("6 задание");
Console.WriteLine("Введите длины сторон треугольника:");
float triangle_a = Convert.ToSingle(Console.ReadLine());
float triangle_b = Convert.ToSingle(Console.ReadLine());
float triangle_c = Convert.ToSingle(Console.ReadLine());
if (triangle_a < triangle_b + triangle_c && triangle_b < triangle_a + triangle_c && triangle_c < triangle_a + triangle_b)
{
    float half_perimeter = (triangle_a + triangle_b + triangle_c) / 2;
    float square = (float)Math.Sqrt(half_perimeter * (half_perimeter - triangle_a) * (half_perimeter - triangle_b) * (half_perimeter - triangle_c));
    Console.WriteLine(square);
}
else
{
    Console.WriteLine("Треугольник с указанными сторонами не существует!");
}

