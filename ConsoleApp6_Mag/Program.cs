namespace ConsoleApp6_Mag{
    public enum Element{Fire, Wood, Water, Dirt, Metal, Normal}
    class Program
    {
        public static Random rand = new Random();
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
                key = Convert.ToInt32(Console.ReadLine());
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
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1. Посмотреть информацию о маге");
            Console.WriteLine("2. Тренировать мага");
            Console.WriteLine("3. Битва");
            Console.WriteLine("4. Выход");
        }
        static void Train()
        {
            Console.Clear();
            ShowAllMag();
            Console.WriteLine("Выберите, какого мага вы хотите потренировать");
            int key = Convert.ToInt32(Console.ReadLine());
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
                default:
                    Console.WriteLine("Такого мага нет!");
                    GoToMenu();
                    break;
            }
        }
        static void ShowAllMag()
        {
            Console.WriteLine("Список магов:");
            Console.WriteLine("1. Огненный маг\n2. Деревянный маг\n3. Водяной маг\n4. Землянной маг\n5. Металлический маг\n6. Обычный маг");
        }
        static void GoToMenu()
        {
            Console.WriteLine("Чтобы перейти в меню, нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();
            Main();
        }
        static void ShowInfo()
        {
            Console.Clear();
            ShowAllMag();
            Console.WriteLine("Выберите, статы какого мага вы хотите посмотреть:");
            int key = Convert.ToInt32(Console.ReadLine());
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
                default:
                    Console.WriteLine("Такого мага нет!");
                    ShowInfo();
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
            static void NextMove()
            {
                Console.WriteLine("Чтобы перейти к следующему ходу нажмите любую клавишу...");
                Console.ReadLine();
            }
            static void ASCII_Win()
            {
                Console.CursorVisible = false;
                Console.Clear();
                for (int i = 0; i < 15; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(i%2)+1;
                    Console.WriteLine("##      ## #### ##    ## ");
                    Console.WriteLine("##  ##  ##  ##  ###   ## ");
                    Console.WriteLine("##  ##  ##  ##  ####  ## ");
                    Console.WriteLine("##  ##  ##  ##  ## ## ## ");
                    Console.WriteLine("##  ##  ##  ##  ##  #### ");
                    Console.WriteLine("##  ##  ##  ##  ##   ### ");
                    Console.WriteLine(" ###  ###  #### ##    ## ");
                    Thread.Sleep(300);
                    Console.Clear();
                }
                Console.Clear();
                Console.ForegroundColor = (ConsoleColor)15;
                GoToMenu();
            }
            static void ASCII_Lose()
            {
                Thread.Sleep(300);
                Console.Clear();
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
                Console.WriteLine($"Ваш {b1.Element} (первый) маг использует специальную способность");
                if (b1.KillsElement == e1.Element)
                {
                    Console.WriteLine($"Первый {e1.Element} вражеский маг убит");
                    e1.IsDeath = true;
                }
                else
                {
                    Console.WriteLine("Ваш маг не убил вражеского мага");
                    Console.WriteLine($"Вражеский {e1.Element} (первый) маг использует специальную способность");
                    if (e1.KillsElement == b1.Element)
                    {
                        Console.WriteLine($"Ваш первый {b1.Element} маг убит");
                        b1.IsDeath = true;
                    }
                    else
                    {
                        Console.WriteLine("Вражеский маг не убил вашего мага");
                    }
                }
                Console.WriteLine($"Ваш {b2.Element} (второй) маг использует специальную способность");
                if (e2.KillsElement == b2.Element)
                {
                    Console.WriteLine($"Второй {b2.Element} ваш маг убит");
                    b2.IsDeath = true;
                }
                else
                {
                    Console.WriteLine("Вражеский маг не убил вашего мага");
                    Console.WriteLine($"Ваш {b2.Element} (второй) маг использует специальную способность");
                    if (b2.KillsElement == e2.Element)
                    {
                        Console.WriteLine($"Ваш второй {b2.Element} маг убит");
                        b2.IsDeath = true;
                    }
                    else
                    {
                        Console.WriteLine("Ваш маг не убил вражеского мага");
                    }
                }
                Console.ReadKey();
            }
            static void Attack(Mag e1, Mag e2, Mag b1, Mag b2)
            {
                while ((!b1.IsDeath || !b2.IsDeath) && (!e1.IsDeath || !e2.IsDeath))
                {
                    if (!b1.IsDeath)
                    {
                        if (!e1.IsDeath)
                        {
                            e1.Health -= b1.Damage;
                            if (e1.Health < 0 || e1.Health == 0)
                            {
                                e1.IsDeath = true;
                                Console.WriteLine($"Ваш {b1.Element} первый маг использует навык {b1.AttackMessage} и наносит {b1.Damage} урона по вражескому {e1.Element} первому магу\nПЕРВЫЙ ВРАЖЕСКИЙ МАГ УБИТ!");
                                if (e1.IsDeath && e2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Win();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Ваш {b1.Element} первый маг использует навык {b1.AttackMessage} и наносит {b1.Damage} урона по вражескому {e1.Element} первому магу\nHP первого злого мага: {e1.Health}");
                            }
                        }
                        else if (!e2.IsDeath)
                        {
                            e2.Health -= b2.Damage;
                            if (e2.Health < 0 || e2.Health == 0)
                            {
                                e2.IsDeath = true;
                                Console.WriteLine($"Ваш {b1.Element} первый маг использует навык {b1.AttackMessage} и наносит {b1.Damage} урона по вражескому {e2.Element} второму магу\nВТОРОЙ ВРАЖЕСКИЙ МАГ УБИТ!");
                                if (e1.IsDeath && e2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Win();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Ваш {b1.Element} первый маг использует навык {b1.AttackMessage} и наносит {b1.Damage} урона по вражескому {e2.Element} второму магу\nHP второго злого мага: {e2.Health}");
                            }
                        }
                    }
                    if (!b2.IsDeath)
                    {
                        if (!e2.IsDeath)
                        {
                            e2.Health -= b2.Damage;
                            if (e2.Health < 0 || e2.Health == 0)
                            {
                                e2.IsDeath = true;
                                Console.WriteLine($"Ваш {b2.Element} второй маг использует навык {b2.AttackMessage} и наносит {b2.Damage} урона по вражескому {e2.Element} второму магу\nВТОРОЙ ВРАЖЕСКИЙ МАГ УБИТ!"); 
                                if (e1.IsDeath && e2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Win();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Ваш {b2.Element} второй маг использует навык {b2.AttackMessage} и наносит {b2.Damage} урона по вражескому {e2.Element} второму магу\nHP второго злого мага: {e2.Health}");                            
                            }
                        }
                        else if (!e1.IsDeath)
                        {
                            e1.Health -= b2.Damage;
                            if (e1.Health < 0 || e1.Health == 0)
                            {
                                e1.IsDeath = true;
                                Console.WriteLine($"Ваш {b2.Element} второй маг использует навык {b2.AttackMessage} и наносит {b2.Damage} урона по вражескому {e1.Element} первому магу\nПЕРВЫЙ ВРАЖЕСКИЙ МАГ УБИТ!");
                                if (e1.IsDeath && e2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Win();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Ваш {b2.Element} второй маг использует навык {b2.AttackMessage} и наносит {b2.Damage} урона по вражескому {e1.Element} первому магу\nHP первого злого мага: {e1.Health}");
                            }
                        }
                    }
                    if (!e1.IsDeath)
                    {
                        if (!b1.IsDeath)
                        {
                            b1.Health -= e1.Damage;
                            if (b1.Health < 0 || b1.Health == 0)
                            {
                                b1.IsDeath = true;
                                Console.WriteLine($"Вражеский {e1.Element} первый маг использует навык {e1.AttackMessage} и наносит {e1.Damage} урона по вашему {b1.Element} первому магу\nПЕРВЫЙ ВАШ МАГ УБИТ!");
                                if (b1.IsDeath && b2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Lose();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Вражеский {e1.Element} первый маг использует навык {e1.AttackMessage} и наносит {e1.Damage} урона по вашему {b1.Element} первому магу\nHP первого вашего мага: {b1.Health}");
                            }
                        }
                        else if (!b2.IsDeath)
                        {
                            b2.Health -= e1.Damage;
                            if (b2.Health < 0 || b2.Health == 0)
                            {
                                b2.IsDeath = true;
                                Console.WriteLine($"Вражеский {e1.Element} первый маг использует навык {e1.AttackMessage} и наносит {e1.Damage} урона по вашему {b2.Element} второму магу\nВТОРОЙ ВАШ МАГ УБИТ!");
                                if (b1.IsDeath && b2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Lose();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Вражеский {e1.Element} первый маг использует навык {e1.AttackMessage} и наносит {e1.Damage} урона по вашему {b2.Element} второму магу\nHP второго вашего мага: {b2.Health}");
                            }
                        }
                    }
                    if (!e2.IsDeath)
                    {
                        if (!b2.IsDeath)
                        {
                            b2.Health -= e2.Damage;
                            if (b2.Health < 0 || b2.Health == 0)
                            {
                                b2.IsDeath = true;
                                Console.WriteLine($"Вражеский {e2.Element} второй использует навык {e2.AttackMessage} и наносит {e2.Damage} урона по вашему {b2.Element} второму магу\nВТОРОЙ ВАШ МАГ УБИТ!");
                                if (b1.IsDeath && b2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Lose();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Вражеский {e2.Element} второй использует навык {e2.AttackMessage} и наносит {e2.Damage} урона по вашему {b2.Element} второму магу\nHP второго вашего мага: {b2.Health}");
                            }
                        }
                        else if (!b1.IsDeath)
                        {
                            b1.Health -= e2.Damage;
                            if (b1.Health < 0 || b1.Health == 0)
                            {
                                b1.IsDeath = true;
                                Console.WriteLine($"Вражеский {e2.Element} второй использует навык {e2.AttackMessage} и наносит {e2.Damage} урона по вашему {b1.Element} первому магу\nПЕРВЫЙ ВАШ МАГ УБИТ!");
                                if (b1.IsDeath && b2.IsDeath)
                                {
                                    Thread.Sleep(2000);
                                    ASCII_Lose();
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Вражеский {e2.Element} второй использует навык {e2.AttackMessage} и наносит {e2.Damage} урона по вашему {b1.Element} первому магу\nHP первого вашего мага: {b1.Health}");
                            }
                        }
                    }
                    NextMove();
                }
            }
            static void InfoEnemy(Mag e1, Mag e2)
            {
                Console.WriteLine("Ваши противники:");
                e1.ShowInfo();
                e2.ShowInfo();
            }
            static void InfoBoec(Mag b1, Mag b2)
            {
                Console.WriteLine("Ваши бойцы:");
                b1.ShowInfo();
                b2.ShowInfo();
            }
            int key;
            int f_enemy = rand.Next(0, 6);
            int s_enemy = rand.Next(0, 6);
            Mag enemy_1 = new Mag();
            Mag enemy_2 = new Mag();
            Mag boec_1 = new Mag();
            Mag boec_2 = new Mag();
            switch (f_enemy)
            {
                case 0:
                    enemy_1 = new Fire_Mag();
                    break;
                case 1:
                    enemy_1 = new Wood_Mag();
                    break;
                case 2:
                    enemy_1 = new Water_Mag();
                    break;
                case 3:
                    enemy_1 = new Dirt_Mag();
                    break;
                case 4:
                    enemy_1 = new Metal_Mag();
                    break;
                case 5:
                    enemy_1 = new Normal_Mag();
                    break;
            }
            switch (s_enemy)
            {
                case 0:
                    enemy_2 = new Fire_Mag();
                    break;
                case 1:
                    enemy_2 = new Wood_Mag();
                    break;
                case 2:
                    enemy_2 = new Water_Mag();
                    break;
                case 3:
                    enemy_2 = new Dirt_Mag();
                    break;
                case 4:
                    enemy_2 = new Metal_Mag();
                    break;
                case 5:
                    enemy_2 = new Normal_Mag();
                    break;
            }
            Console.Clear();
            InfoEnemy(enemy_1, enemy_2);
            Console.WriteLine("Выберите магов, которые будут биться с противниками");
            ShowAllMag();
            key = Convert.ToInt32(Console.ReadLine())-1;
            switch (key)
            {
                case 0:
                    boec_1 = fire_mag;
                    break;
                case 1:
                    boec_1 = wood_mag;
                    break;
                case 2:
                    boec_1 = water_mag;
                    break;
                case 3:
                    boec_1 = dirt_mag;
                    break;
                case 4:
                    boec_1 = metal_mag;
                    break;
                case 5:
                    boec_1 = normal_mag;
                    break;
            }
            Console.Clear();
            InfoEnemy(enemy_1, enemy_2);
            Console.WriteLine("Ваши бойцы:");
            boec_1.ShowInfo();
            ShowAllMag();
            key = Convert.ToInt32(Console.ReadLine())-1;
            switch (key)
            {
                case 0:
                    boec_2 = fire_mag;
                    break;
                case 1:
                    boec_2 = wood_mag;
                    break;
                case 2:
                    boec_2 = water_mag;
                    break;
                case 3:
                    boec_2 = dirt_mag;
                    break;
                case 4:
                    boec_2 = metal_mag;
                    break;
                case 5:
                    boec_2 = normal_mag;
                    break;
            }
            Console.Clear();
            InfoEnemy(enemy_1, enemy_2);
            InfoBoec(boec_1, boec_2);
            Console.WriteLine("ДА НАЧНЕТСЯ БОЙ!");
            Thread.Sleep(300);
            Console.CursorVisible = false;
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = (ConsoleColor)(i%2)+1;
                Console.WriteLine("########     ###    ######## ######## ##       ######## ");
                Console.WriteLine("##     ##   ## ##      ##       ##    ##       ## ");
                Console.WriteLine("##     ##  ##   ##     ##       ##    ##       ##   ");
                Console.WriteLine("########  ##     ##    ##       ##    ##       ########");
                Console.WriteLine("##     ## #########    ##       ##    ##       ## ");
                Console.WriteLine("##     ## ##     ##    ##       ##    ##       ##  ");
                Console.WriteLine("########  ##     ##    ##       ##    ######## ######## ");
                Thread.Sleep(300);
                Console.Clear();
            }
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)15;
            ASCII_Battle();
            SpecAttack(enemy_1, enemy_2, boec_1, boec_2);
            if (boec_1.IsDeath && boec_2.IsDeath)
            {
                ASCII_Lose();
            }
            else if (enemy_1.IsDeath && enemy_2.IsDeath)
            {
                ASCII_Win();
            }
            else
            {
                Attack(enemy_1, enemy_2, boec_1, boec_2);           
            }
        }  
    }
}