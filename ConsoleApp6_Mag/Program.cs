namespace ConsoleApp6_Mag{
    public enum Element{Fire, Wood, Water, Dirt, Metal, Normal, Null}
    class Program
    {
        public static Random rand = new Random();
        public static Mag mag = new Mag();
        public static Fire_Mag fire_mag = new Fire_Mag();
        public static Wood_Mag wood_mag = new Wood_Mag();
        public static Water_Mag water_mag = new Water_Mag();
        public static Dirt_Mag dirt_mag = new Dirt_Mag();
        public static Metal_Mag metal_mag = new Metal_Mag();
        public static Normal_Mag normal_mag = new Normal_Mag();
        static void Main()
        {
            int key;
            while (true)
            {
                Menu();
                while (!int.TryParse(Console.ReadLine(), out key) || key < 1 || key > 4)
                {
                    Console.WriteLine("Некорректный ввод. Введите число от 1 до 4.");
                }
                switch (key)
                {
                    case 1:
                        ShowInfo();
                        break;
                    case 2:
                        Train();
                        break;
                    case 3:
                        Battle();
                        break;
                    case 4:
                        return;
                }
            }
        }
        static void RegenMag()
        {
            fire_mag.Health = 100;
            wood_mag.Health = 100;
            water_mag.Health = 100 * Math.Pow(1.2, water_mag.Train_Count);
            dirt_mag.Health = 100 * Math.Pow(1.33, dirt_mag.Train_Count);
            metal_mag.Health = 100 * Math.Exp(metal_mag.Train_Count);
            normal_mag.Health = 100 * Math.Pow(1.2, normal_mag.Train_Count);
            fire_mag.IsDeath = false;
            wood_mag.IsDeath = false;
            water_mag.IsDeath = false;
            metal_mag.IsDeath = false;
            normal_mag.IsDeath = false;
            dirt_mag.IsDeath = false;
        }
        static void Menu()
        {
            RegenMag();
            Console.Clear();
            Console.WriteLine("1. Посмотреть информацию о маге");
            Console.WriteLine("2. Тренировать мага");
            Console.WriteLine("3. Битва");
            Console.WriteLine("4. Выход");
        }
        static void Train()
        {
            int key;
            Console.Clear();
            ShowAllMag();
            Console.WriteLine("Выберите, какого мага вы хотите потренировать");
            while (!int.TryParse(Console.ReadLine(), out key) || key < 1 || key > 6)
                {
                    Console.WriteLine("Некорректный ввод. Введите число от 1 до 6.");
                }
            switch (key)
            {
                case 1:
                    fire_mag.Train();
                    GoToMenu();
                    break;
                case 2:
                    wood_mag.Train();
                    GoToMenu();
                    break;
                case 3:
                    water_mag.Train();
                    GoToMenu();
                    break;
                case 4:
                    dirt_mag.Train();
                    GoToMenu();
                    break;
                case 5:
                    metal_mag.Train();
                    GoToMenu();
                    break;
                case 6:
                    normal_mag.Train();
                    GoToMenu();
                    break;
            }
        }
        static void ShowAllMag()
        {
            Console.WriteLine("Список магов:");
            int k = 0;
            foreach (Element element in Enum.GetValues(typeof(Element)))
            {
                k++;
                if (k == 7) break;
                Console.WriteLine($"{k}. {mag.StrElement(element)}");
            }
        }
        static void GoToMenu()
        {
            Console.WriteLine("Чтобы перейти в меню, нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();
        }
        static void ShowInfo()
        {
            int key;
            Console.Clear();
            ShowAllMag();
            Console.WriteLine("Выберите, статы какого мага вы хотите посмотреть:");
            while (!int.TryParse(Console.ReadLine(), out key) || key < 1 || key > 6)
                {
                    Console.WriteLine("Некорректный ввод. Введите число от 1 до 6.");
                }
            switch (key)
            {
                case 1:
                    fire_mag.ShowInfo();
                    GoToMenu();
                    break;
                case 2:
                    wood_mag.ShowInfo();
                    GoToMenu();
                    break;
                case 3:
                    water_mag.ShowInfo();
                    GoToMenu();
                    break;
                case 4:
                    dirt_mag.ShowInfo();
                    GoToMenu();
                    break;
                case 5:
                    metal_mag.ShowInfo();
                    GoToMenu();
                    break;
                case 6:
                    normal_mag.ShowInfo();
                    GoToMenu();
                    break;
            }
        }
        static void ASCII_Battle()
        {
            Console.Clear();
            Console.WriteLine("########     ###    ######## ######## ##       ######## ");
            Console.WriteLine("##     ##   ## ##      ##       ##    ##       ## ");
            Console.WriteLine("##     ##  ##   ##     ##       ##    ##       ##   ");
            Console.WriteLine("########  ##     ##    ##       ##    ##       ########");
            Console.WriteLine("##     ## #########    ##       ##    ##       ## ");
            Console.WriteLine("##     ## ##     ##    ##       ##    ##       ##  ");
            Console.WriteLine("########  ##     ##    ##       ##    ######## ######## ");
        }
        static void Battle()
        {
            static Mag CreateRandomMag()
            {
                int enemy = rand.Next(0, 6);
                Mag mag = new Mag();
                switch (enemy)
                {
                    case 0:
                        mag = new Fire_Mag();
                        break;
                    case 1:
                        mag = new Wood_Mag();
                        break;
                    case 2:
                        mag = new Water_Mag();
                        break;
                    case 3:
                        mag = new Dirt_Mag();
                        break;
                    case 4:
                        mag = new Metal_Mag();
                        break;
                    case 5:
                        mag = new Normal_Mag();
                        break;
                }
                return mag;
            }
            static Mag[] ChooseMag()
            {
                static Mag Choose(int key)
                {
                    switch (key - 1)
                    {
                        case 0: return fire_mag;
                        case 1: return wood_mag;
                        case 2: return water_mag;
                        case 3: return dirt_mag;
                        case 4: return metal_mag;
                        case 5: return normal_mag;
                        default: return null;
                    }
                }
                int key;
                Mag m1 = null;
                Mag m2 = null;
                ShowAllMag();
                Console.WriteLine("Выберите первого мага (1-6):");
                while (!int.TryParse(Console.ReadLine(), out key) || key < 1 || key > 6)
                {
                    Console.WriteLine("Некорректный ввод. Введите число от 1 до 6.");
                }
                m1 = Choose(key);
                Console.WriteLine("Выберите второго мага (1-6):");
                while (true)
                {
                    while (!int.TryParse(Console.ReadLine(), out key) || key < 1 || key > 6)
                    {
                        Console.WriteLine("Некорректный ввод. Введите число от 1 до 6.");
                    }
                    m2 = Choose(key);
                    if (m1 != m2)
                        break; 
                    Console.WriteLine("Нельзя выбрать магов одного типа. Перевыберите второго мага.");
                }
                Console.Clear();
                return [m1, m2];
            }
            static void NextMove()
            {
                Console.WriteLine("Чтобы перейти к следующему ходу нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
            static void ASCII_Battle_Start()
            {
                Console.Clear();
                for (int i = 0; i < 4; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(i%2)+1;
                    if (i % 2 == 0)
                    {
                        Console.WriteLine("########     ###    ######## ######## ##       ######## ");
                        Console.WriteLine("##     ##   ## ##      ##       ##    ##       ## ");
                        Console.WriteLine("##     ##  ##   ##     ##       ##    ##       ##   ");
                        Console.WriteLine("########  ##     ##    ##       ##    ##       ########");
                        Console.WriteLine("##     ## #########    ##       ##    ##       ## ");
                        Console.WriteLine("##     ## ##     ##    ##       ##    ##       ##  ");
                        Console.WriteLine("########  ##     ##    ##       ##    ######## ######## ");
                    }
                    else
                    {
                        Console.WriteLine(" ######  ########    ###    ########  ######## ");
                        Console.WriteLine("##    ##    ##      ## ##   ##     ##    ##    ");
                        Console.WriteLine("##          ##     ##   ##  ##     ##    ##    ");
                        Console.WriteLine(" ######     ##    ##     ## ########     ##    ");
                        Console.WriteLine("      ##    ##    ######### ##   ##      ##    ");
                        Console.WriteLine("##    ##    ##    ##     ## ##    ##     ##    ");
                        Console.WriteLine(" ######     ##    ##     ## ##     ##    ##    ");                        
                    }
                    Thread.Sleep(700);
                    Console.Clear();
                    Console.ForegroundColor = (ConsoleColor)15;
                }
                Console.Clear();
            }
            static void ASCII_Win()
            {
                Console.ForegroundColor = (ConsoleColor)10;
                Console.WriteLine("##      ## #### ##    ## ");
                Console.WriteLine("##  ##  ##  ##  ###   ## ");
                Console.WriteLine("##  ##  ##  ##  ####  ## ");
                Console.WriteLine("##  ##  ##  ##  ## ## ## ");
                Console.WriteLine("##  ##  ##  ##  ##  #### ");
                Console.WriteLine("##  ##  ##  ##  ##   ### ");
                Console.WriteLine(" ###  ###  #### ##    ## ");
                Console.ForegroundColor = (ConsoleColor)15;
                GoToMenu();
            }
            static void ASCII_Lose()
            {
                Console.ForegroundColor = (ConsoleColor)4;
                Console.WriteLine("##        #######   ######  ######## ");
                Console.WriteLine("##       ##     ## ##    ## ##       ");
                Console.WriteLine("##       ##     ## ##       ##       ");
                Console.WriteLine("##       ##     ##  ######  #######  ");
                Console.WriteLine("##       ##     ##       ## ##       ");
                Console.WriteLine("##       ##     ## ##    ## ##       ");
                Console.WriteLine("########  #######   ######  ######## ");
                Console.ForegroundColor = (ConsoleColor)15;
                GoToMenu();
            }
            static void SpecAttack(Mag e1, Mag e2, Mag b1, Mag b2)
            {
                Console.WriteLine($"Ваш {mag.StrElement(b1.Element)} первый маг использует специальную способность");
                if (b1.KillsElement == e1.Element)
                {
                    Console.WriteLine($"Первый {mag.StrElement(e1.Element)} вражеский маг убит\n");
                    e1.IsDeath = true;
                }
                else
                {
                    Console.WriteLine("Ваш маг не убил вражеского мага\n");
                    Console.WriteLine($"Вражеский {mag.StrElement(e1.Element)} первый маг использует специальную способность");
                    if (e1.KillsElement == b1.Element)
                    {
                        Console.WriteLine($"Ваш первый {mag.StrElement(b1.Element)} маг убит\n");
                        b1.IsDeath = true;
                    }
                    else
                    {
                        Console.WriteLine("Вражеский маг не убил вашего мага\n");
                    }
                }
                Console.WriteLine($"Вражеский {mag.StrElement(e2.Element)} второй маг использует специальную способность");
                if (e2.KillsElement == b2.Element)
                {
                    Console.WriteLine($"Второй {mag.StrElement(b2.Element)} ваш маг убит\n");
                    b2.IsDeath = true;
                }
                else
                {
                    Console.WriteLine("Вражеский маг не убил вашего мага\n");
                    Console.WriteLine($"Ваш {mag.StrElement(b2.Element)} второй маг использует специальную способность");
                    if (b2.KillsElement == e2.Element)
                    {
                        Console.WriteLine($"Вражеский второй {mag.StrElement(e2.Element)} маг убит\n");
                        e2.IsDeath = true;
                    }
                    else
                    {
                        Console.WriteLine("Ваш маг не убил вражеского мага\n");
                    }
                }
                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
            }
            static void CheckCommandIsDeath(Mag e1, Mag e2, Mag b1, Mag b2)
            {
                if (e1.IsDeath && e2.IsDeath)
                {
                    ASCII_Win();
                }
                else if (b1.IsDeath && b2.IsDeath)
                {
                    ASCII_Lose();
                }
                else
                {
                    return;
                }
            }
            static void AttackMsg(Mag attacker, Mag defender, int who)
            {
                defender.Health -= attacker.Damage;
                switch (who)
                {
                    case 1:
                        if (defender.Health < 0 || defender.Health == 0)
                        {
                            defender.IsDeath = true;
                            Console.WriteLine($"Ваш {mag.StrElement(attacker.Element)} маг использует навык {attacker.AttackMessage} и наносит {attacker.Damage} урона по вражескому {mag.StrElement(defender.Element)} магу\n{mag.StrElement(defender.Element)} вражеский маг УБИТ!\n");
                        }
                        else
                        {
                            Console.WriteLine($"Ваш {mag.StrElement(attacker.Element)} маг использует навык {attacker.AttackMessage} и наносит {attacker.Damage} урона по вражескому {mag.StrElement(defender.Element)} магу\nHP {mag.StrElement(defender.Element)} вражеского мага: {defender.Health}\n");
                        }
                        break;
                    case 2:
                        if (defender.Health < 0 || defender.Health == 0)
                        {
                            defender.IsDeath = true;
                            Console.WriteLine($"Вражеский {mag.StrElement(attacker.Element)} маг использует навык {attacker.AttackMessage} и наносит {attacker.Damage} урона по вашему {mag.StrElement(defender.Element)} магу\nваш {mag.StrElement(defender.Element)} маг УБИТ!\n");
                        }
                        else
                        {
                            Console.WriteLine($"Вражеский {mag.StrElement(attacker.Element)} маг использует навык {attacker.AttackMessage} и наносит {attacker.Damage} урона по вашему {mag.StrElement(defender.Element)} магу\nHP вашего {mag.StrElement(defender.Element)} мага: {defender.Health}\n");
                        }
                        break;
                }
            }
            static void Attack(Mag e1, Mag e2, Mag b1, Mag b2)
            {
                while ((!b1.IsDeath || !b2.IsDeath) && (!e1.IsDeath || !e2.IsDeath))
                {
                    if (!b1.IsDeath)
                    {
                        if (!e1.IsDeath)
                        {
                            AttackMsg(b1, e1, 1);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                        else if (!e2.IsDeath)
                        {
                            AttackMsg(b1, e2, 1);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                    }
                    if (!b2.IsDeath)
                    {
                        if (!e2.IsDeath)
                        {
                            AttackMsg(b2, e2, 1);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                        else if (!e1.IsDeath)
                        {
                            AttackMsg(b2, e1, 1);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                    }
                    if (!e1.IsDeath)
                    {
                        if (!b1.IsDeath)
                        {
                            AttackMsg(e1, b1, 2);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                        else if (!b2.IsDeath)
                        {
                            AttackMsg(e1, b2, 2);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                    }
                    if (!e2.IsDeath)
                    {
                        if (!b2.IsDeath)
                        {
                            AttackMsg(e2, b2, 2);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                        else if (!b1.IsDeath)
                        {
                            AttackMsg(e2, b1, 2);
                            CheckCommandIsDeath(e1, e2, b1, b2);
                        }
                    }
                    if ((!b1.IsDeath || !b2.IsDeath) && (!e1.IsDeath || !e2.IsDeath))
                    {
                        NextMove();
                    }
                }
            }
            static void InfoEnemy(Mag e1, Mag e2)
            {
                Console.WriteLine("Ваши противники:");
                e1.ShowInfo();
                e2.ShowInfo();
                Console.WriteLine();
            }
            static void InfoBoec(Mag b1, Mag b2)
            {
                Console.WriteLine("Ваши бойцы:");
                b1.ShowInfo();
                b2.ShowInfo();
            }
            Mag enemy_1 = CreateRandomMag();
            Mag enemy_2 = CreateRandomMag();
            while (enemy_1.Element == enemy_2.Element){
                enemy_2 = CreateRandomMag();
            }
            Console.Clear();
            InfoEnemy(enemy_1, enemy_2);
            Console.WriteLine("Выберите магов, которые будут биться с противниками");
            Mag[] all_boici = ChooseMag();
            Mag boec_1 = all_boici[0];
            Mag boec_2 = all_boici[1];
            Console.Clear();
            InfoEnemy(enemy_1, enemy_2);
            InfoBoec(boec_1, boec_2);
            Console.WriteLine("Нажмите любую клавишу, чтобы начать битву...");
            Console.ReadKey();
            ASCII_Battle_Start();
            ASCII_Battle();
            SpecAttack(enemy_1, enemy_2, boec_1, boec_2);
            Attack(enemy_1, enemy_2, boec_1, boec_2);
        }  
    }
}