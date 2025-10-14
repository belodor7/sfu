Random random = new Random();

Console.WriteLine("### ЗАДАНИЕ 1 ###");
int ran = random.Next(1, 11);
int k = 0;
int ran_in;
Console.WriteLine("Введите число:");
ran_in = Convert.ToInt32(Console.ReadLine());
while (ran_in != ran)
{
        k++;
        Console.WriteLine("Число не совпадает!");
        if (ran_in < ran)
        {
            Console.WriteLine("Введённое число меньше загаданного");
        }
        else
        {
            Console.WriteLine("Введённое число больше загаданного");
        }
        ran_in = Convert.ToInt32(Console.ReadLine());
}
k++;
Console.WriteLine($"Число отгадано. Количество затраченных попыток: {k}");

Console.WriteLine("### ЗАДАНИЕ 2 ###");
int n;
int sum = 1;
Console.WriteLine("Введите N > 10");
n = Convert.ToInt32(Console.ReadLine());
while (n < 10 || n == 10)
{
    Console.WriteLine("N должно быть больше 10");
    n = Convert.ToInt32(Console.ReadLine());
}
for (int i = 2; i != n; i++)
{
    if (sum < 501)
    {
        sum = sum + (int)Math.Pow(i, 2);
    }
    if (sum > 500)
    {
        Console.WriteLine($"Сумма квадратов чисел последовательности от 1 до {n} превышает 500!");
        break;
    }
}
if (sum < 501)
{
    Console.WriteLine($"Сумма квадратов чисел последовательности от 1 до {n} равна: {sum}.");
}

Console.WriteLine("### ЗАДАНИЕ 3 ###");
int k_pod;
int max = 0;
int min = 100000;
int mark_3 = 0;
int mark_4 = 0;
int mark_5 = 0;
Console.WriteLine("Введите количество студентов:");
n = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Введите количество подтягиваний у каждого студента:");
for (int i = 0; i != n; i++)
{
    k_pod = Convert.ToInt32(Console.ReadLine());
    if (k_pod < 0)
    {
        Console.WriteLine("Количество подтягиваний не может быть отрицательным. Введите верное значение:");
        k_pod = Convert.ToInt32(Console.ReadLine());
    }
    max = Math.Max(max, k_pod);
    min = Math.Min(min, k_pod);
    if (k_pod == 12 || (k_pod > 12 && k_pod < 14))
    {
        mark_3++;
    }
    if (k_pod == 14 || (k_pod > 14 && k_pod < 16))
    {
        mark_4++;
    }
    if (k_pod >= 16)
    {
        mark_5++;
    }
};
Console.WriteLine($"Максимальное количество подтягиваний: {max}");
Console.WriteLine($"Минимальное количество подтягиваний: {min}");
Console.WriteLine($"Количество студентов, получивших оценку 3: {mark_3}");
Console.WriteLine($"Количество студентов, получивших оценку 4: {mark_4}");
Console.WriteLine($"Количество студентов, получивших оценку 5: {mark_5}");

Console.WriteLine("### ЗАДАНИЕ 4 ###");
double y;
double y_positive = 0;
double y_avg = 0;
double a;
double b;
double c;
double d;
Console.WriteLine("Введите значения A, B, C, D:");
a = Convert.ToDouble(Console.ReadLine());
b = Convert.ToDouble(Console.ReadLine());
c = Convert.ToDouble(Console.ReadLine());
d = Convert.ToDouble(Console.ReadLine());
for (double x = 1; x < 11; x++)
{
    y = (a * Math.Sqrt((b * x) + d)) - (c * x);
    if (y > 0)
    {
        y_positive = y_positive + y;
    }
    y_avg = y_avg + y;
}
y_avg = y_avg / 10;
Console.WriteLine($"Сумма положительных значений y: {y_positive}");
Console.WriteLine($"Среднее значение y: {y_avg}");

Console.WriteLine("### ЗАДАНИЕ 5 ###");
int maxIndex = 0;
int maxValue;
Console.WriteLine("Введите размер массива:");
n = Convert.ToInt32(Console.ReadLine());
int[] array = new int[n];
Console.WriteLine("Введите элементы массива:");
for (int i = 0; i < n; i++)
{
    array[i] = Convert.ToInt32(Console.ReadLine());
}
for (int i = 0; i < n; i++)
{
    if (array[i] > array[maxIndex])
    {
        maxIndex = i;
    }   
}
maxValue = array[maxIndex];
for (int i = maxIndex; i < n - 1; i++)
{
    array[i] = array[i + 1];
}
array[n - 1] = maxValue;
for (int i = 0; i < n; i++)
{
    Console.Write(array[i] + " ");
}
Console.WriteLine();
Console.WriteLine("### ЗАДАНИЕ 6 ###");
double r;
Console.WriteLine("Введите радиус окружности:");
r = Convert.ToDouble(Console.ReadLine());
k = 0;
for (int i = 0; i < 12; i++)
{
    Console.WriteLine($"Введите координаты x и y для центра окружности {i + 1}:");
    double x = Convert.ToDouble(Console.ReadLine());
    y = Convert.ToDouble(Console.ReadLine());
    double dist = Math.Sqrt((x * x) + (y * y));
    if (dist < 2 * r)
    {
        k++;
    }
}
Console.WriteLine($"Количество пересекающихся окружностей: {k}");