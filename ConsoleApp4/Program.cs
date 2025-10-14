Console.WriteLine("### ЗАДАНИЕ 1 ###");
static int[] MaxMinInArray(int[] a){
    int max = 0;
    int min = 100000;
    int len;
    len = a.Length;
    Console.WriteLine();
    foreach (var i in a)
    {
        if (i > max)
        {
            max = i;
        }
        if (i < min)
        {
            min = i;
        }
    }
    return [max, min];
}
static int MultiplyElements(int[] a)
{
    int a_1 = a[0];
    int a_2 = a[1];
    return a_1 * a_2;
}
Console.WriteLine("Введите длину массива: ");
int n = Convert.ToInt32(Console.ReadLine());
int[] array = new int[n];
Console.WriteLine("Введите элементы массива: ");
for (int i = 0; i < n; i++)
{
    array[i] = Convert.ToInt32(Console.ReadLine());
}
Console.WriteLine($"Произведение максимального и минимального элементов массива: {MultiplyElements(MaxMinInArray(array))}");

Console.WriteLine("### ЗАДАНИЕ 2 ###");
Dictionary<string, int> name_money = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
{
    {"Маша", 10000},
    {"Петя", 30000},
    {"Вася", 100000}
};
static void Deposit(string name, Dictionary<string, int> name_money)
{
    double balance = name_money[name];
    double income = 0;
    for (int i = 0; i != 3; i++)
    {
        income += balance * 1.17;
    }
    Console.WriteLine($"Вы можете воспользоваться стандартным вкладом нашего банка!\nВложив сумму остатка {balance} на 3 года под 17% годовых, вы получите прибыль в размере {income-balance} рублей\nДля активации вклада войдите в мобильное приложение!");
}
Console.WriteLine("Введите имя:");
string try_name = Console.ReadLine();
Console.WriteLine("Введите сумму, которую хотите положить на карту:");
int add_money = Convert.ToInt32(Console.ReadLine());
if (name_money.ContainsKey(try_name))
{
    name_money[try_name] += add_money;
    Console.WriteLine($"{try_name}, ваш баланс счёта изменен! Текущий баланс {name_money[try_name]} рублей.");
}
else
{
    name_money.Add(try_name, add_money);
    Console.WriteLine($"Благодарим, что вы стали клиентом нашего банка!\n{try_name}, ваш баланс счёта изменен! Текущий баланс {name_money[try_name]} рублей.");
}
Deposit(try_name, name_money);

Console.WriteLine("### ЗАДАНИЕ 3 ###");
static string GetLongestWord(string sentence)
{
    if (sentence == "")
    {
        return "";
    }
    string[] words = sentence.Split(' ');
    string longest_word = "";
    int max_length = 0;
    foreach (string word in words)
    {
        if (word.Length > max_length)
        {
            max_length = word.Length;
            longest_word = word;
        }
    }
    return longest_word;
}
Console.WriteLine("Введите предложение:");
string sentence = Console.ReadLine();
Console.WriteLine($"Самое длинное слово в предложении: {GetLongestWord(sentence)}");

Console.WriteLine("### ЗАДАНИЕ 4 ###");
static bool CheckPassword(string password)
{
    bool isCorrect = false;
    bool hasLower = false;
    bool hasUpper = false;
    bool hasDigit = false;
    bool hasSpecial = false;
    if (password.Length < 6 || password.Length > 12)
    {
        isCorrect = false;
    }
    else isCorrect = true;
    foreach (char symbol in password)
    {
        if (char.IsLower(symbol)) hasLower = true;
        else if (char.IsUpper(symbol)) hasUpper = true;
        else if (char.IsDigit(symbol)) hasDigit = true;
        else hasSpecial = true;
    }
    return isCorrect && hasLower && hasUpper && hasDigit && hasSpecial;
}
Console.WriteLine("Введите пароль для проверки:");
string password = Console.ReadLine();
if (CheckPassword(password))
{
    Console.WriteLine("Пароль подходит");
}
else
{
    Console.WriteLine("Пароль не подходит");
}