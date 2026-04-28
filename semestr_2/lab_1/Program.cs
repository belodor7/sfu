namespace UHarchi_Simple
{
    public enum Nutrient
    {
        Proteins,
        Fats,
        Carbohydrates
    }
    public abstract class Thing
    {
        public string Name;

        protected Thing(string name)
        {
            Name = name;
        }
    }
    public class NonFood : Thing
    {
        public NonFood(string name) : base(name) { }
    }
    public abstract class Food : Thing
    {
        public Nutrient Nutrient;

        protected Food(string name, Nutrient nutrient) : base(name)
        {
            Nutrient = nutrient;
        }
    }
    public abstract class ISnacks : Food
    {
        protected ISnacks(string name, Nutrient nutrient) : base(name, nutrient) { }
    }

    public abstract class ISemiFinishedFood : Food
    {
        protected ISemiFinishedFood(string name, Nutrient nutrient) : base(name, nutrient) { }
    }

    public abstract class IHealthyFood : Food
    {
        protected IHealthyFood(string name, Nutrient nutrient) : base(name, nutrient) { }
    }
    public class Crisps : ISnacks
    {
        public Crisps() : base("Чипсы", Nutrient.Fats) { }
    }

    public class ChocolateBar : ISnacks
    {
        public ChocolateBar() : base("Шоколадный батончик", Nutrient.Carbohydrates) { }
    }

    public class BalykCheese : ISnacks
    {
        public BalykCheese() : base("Сыр балыковый", Nutrient.Proteins) { }
    }

    public class DumplingsMeat : ISemiFinishedFood
    {
        public DumplingsMeat() : base("Пельмени", Nutrient.Proteins) { }
    }

    public class OliveOil : IHealthyFood
    {
        public OliveOil() : base("Оливковое масло", Nutrient.Fats) { }
    }

    public class Fruit : IHealthyFood
    {
        public Fruit() : base("Фрукт", Nutrient.Carbohydrates) { }
    }

    public class Chicken : IHealthyFood
    {
        public Chicken() : base("Курица", Nutrient.Proteins) { }
    }
    public class UMarket
    {
        public List<Thing> Things = new List<Thing>();

        public void Add(Thing thing)
        {
            Things.Add(thing);
        }

        public T FindFoodByNutrient<T>(Nutrient nutrient) where T : Food
        {
            for (int i = 0; i < Things.Count; i++)
            {
                Food f = Things[i] as Food;
                if (f == null) continue;

                T typed = f as T;
                if (typed == null) continue;

                if (typed.Nutrient == nutrient)
                    return typed;
            }
            return null;
        }
    }

    public class Cart<T> where T : Food
    {
        private List<T> items = new List<T>();
        private UMarket market;

        public Cart(UMarket market)
        {
            this.market = market;
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Show()
        {
            Console.WriteLine("Корзина:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(" - " + items[i].Name + " (" + items[i].Nutrient + ")");
            }
        }

        private bool HasNutrient(Nutrient nutrient)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Nutrient == nutrient)
                    return true;
            }
            return false;
        }

        public void Balance()
        {
            Nutrient[] required = new Nutrient[]
            {
                Nutrient.Proteins,
                Nutrient.Fats,
                Nutrient.Carbohydrates
            };

            for (int i = 0; i < required.Length; i++)
            {
                Nutrient need = required[i];

                if (!HasNutrient(need))
                {
                    T candidate = market.FindFoodByNutrient<T>(need);

                    if (candidate == null)
                    {
                        Console.WriteLine("Невозможно сбалансировать " + typeof(T).Name + " по " + need);
                        return;
                    }

                    items.Add(candidate);
                    Console.WriteLine("Добавлено: " + candidate.Name + " (" + need + ")");
                }
            }

            Console.WriteLine("Продуктовая корзина сбалансирована!");
        }
    }
    class Program
    {
        static void Main()
        {
            UMarket market = new UMarket();
            market.Add(new Crisps());
            market.Add(new ChocolateBar());
            market.Add(new BalykCheese());
            market.Add(new DumplingsMeat());
            market.Add(new OliveOil());
            market.Add(new Fruit());
            market.Add(new NonFood("Ручка"));

            Cart<ISnacks> cart = new Cart<ISnacks>(market);

            cart.Add(new ChocolateBar());
            cart.Add(new Crisps());

            cart.Show();
            Console.WriteLine();

            cart.Balance();
            Console.WriteLine();

            cart.Show();
        }
    }
}