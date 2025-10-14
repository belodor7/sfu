//1 задание
Console.WriteLine("1 задание");
Console.WriteLine("Укажите, каким по счёту стоит Серёжа:");
int n = Convert.ToInt32(Console.ReadLine());
int num = n % 2;
switch (num)
{
    case 1:
        Console.WriteLine($"Первый");
        break;
    case 0:
        Console.WriteLine($"Второй");
        break;
}

//2 задание
Console.WriteLine("2 задание");
Console.WriteLine("Введите длины сторон треугольника:");
float t_a = Convert.ToSingle(Console.ReadLine());
float t_b = Convert.ToSingle(Console.ReadLine());
float t_c = Convert.ToSingle(Console.ReadLine());
double p_a = Math.Pow(t_a, 2);
double p_b = Math.Pow(t_b, 2);
double p_c = Math.Pow(t_c, 2);
double cos_c = (p_b + p_c - p_a) / (2 * t_b * t_c);
if (t_a < t_b + t_c && t_b < t_a + t_c && t_c < t_a + t_b)
{
    if ((p_a == p_b + p_c) || (p_b == p_a + p_c) || (p_c == p_a + p_b))
    {
        Console.WriteLine("Треугольник является прямоугольным");
    }
    else
    {
        Console.WriteLine($"Угол C равен: {(float)(Math.Acos(cos_c)*180/Math.PI)}");
    }
}
else
{
    Console.WriteLine("Треугольник с указанными сторонами не существует!");
}

//3 задание
int max_queue = 48;
int for_guest = 20;
int time = 0;
Console.WriteLine("3 задание");
Console.WriteLine("Введите номер Сергея в очереди:");
int queue = Convert.ToInt32(Console.ReadLine());
if (queue > max_queue)
{
    Console.WriteLine("Сергею нецелесообразно стоять в очереди.");
}
else
{
    while (queue >= 2)
    {
        time += for_guest;
        queue -= 2;
    }
    int hours = time / 60;
    time = time - (hours * 60);
    Console.WriteLine($"Сергей простоит в очереди {hours} часов {time} минут.");
}

//4 задание
Console.WriteLine("4 задание");
Console.WriteLine("Выберите вид вклада:");
Console.WriteLine("1 - Вклад на 1 год под 7% годовых\n2 - Вклад на 3 года под 8% годовых\n3 - Вклад на 5 лет под 10% годовых");
int key = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите сумму вклада:");
int deposit_0 = Convert.ToInt32(Console.ReadLine());
double deposit_1 = deposit_0;
switch (key)
{
    case 1:
        deposit_1 = deposit_1 * 0.07;
        break;
    case 2:
        for (int i = 0; i != 3; i++)
        {
            deposit_1 *= 1.08;
        }
        break;
    case 3:
        for (int i = 0; i != 5; i++)
        {
            deposit_1 *= 1.1;
        }
        break;
}
Console.WriteLine($"Прибыль составит: {(float)deposit_1 - (float)deposit_0}");

//5 задание
int max_speed = 90;
Console.WriteLine("5 задание");
Console.WriteLine("Введите скорость автомобиля:");
int delta_speed = Convert.ToInt32(Console.ReadLine()) - max_speed;
if (delta_speed < 20)
{
    Console.WriteLine("Скорость автомобиля допустима на данном участке");
}
else if ((20 <= delta_speed) && (delta_speed < 40))
{
    Console.WriteLine("Превышение допустимой скорости от 20 км/ч до 40 км/ч. Штраф - 500 рублей.");
}
else if ((40 <= delta_speed) && (delta_speed < 60))
{
    Console.WriteLine("Превышение допустимой скорости от 40 км/ч до 60 км/ч. Штраф - 1500 рублей.");
}
else if ((60 <= delta_speed) && (delta_speed < 80))
{
    Console.WriteLine("Превышение допустимой скорости от 60 км/ч до 80 км/ч. Штраф - 2500 рублей или лишение прав на 4 месяца.");
}
else
{
    Console.WriteLine("Превышение допустимой скорости более, чем на 80 км/ч. Штраф - 5000 рублей или лишение прав на 6 месяца.");
}