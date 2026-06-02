using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_4
{
	enum CuisineType { Japanese, Italian }

	class Dish
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public CuisineType CuisineType { get; set; }
		public decimal Price { get; set; }

		public override string ToString() => $"{Id}: {Name} ({CuisineType}) - {Price}р";
	}

	class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}

	class Order
	{
		private static int _nextId =1;
		public Order()
		{
			Id = _nextId++;
			Dishes = new List<Dish>();
		}

		public int Id { get; }
		public int TableNumber { get; set; }
		public int CustomerId { get; set; }
		public int WaiterId { get; set; }
		public List<Dish> Dishes { get; }
		public decimal TotalPrice => Dishes.Sum(d => d.Price);

		public void AddDish(Dish d) => Dishes.Add(d);
	}

	class Chef
	{
		public string Name { get; set; } = string.Empty;
		public int OrdersHandled { get; set; }
		public CuisineType CuisineType { get; set; }
	}

	class Waiter
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int OrdersHandled { get; set; }

		public override string ToString() => $"{Id}: {Name} (обработано {OrdersHandled})";
	}

	class Restaurant
	{
		public List<Chef> Chefs { get; } = new();
		public List<Order> Orders { get; } = new();
		public List<Dish> Dishes { get; } = new();
		public List<Waiter> Waiters { get; } = new();
		public List<Customer> Customers { get; } = new();

		public Dish? FindDish(int id) => Dishes.FirstOrDefault(d => d.Id == id);


		public void TakeOrder(Order order)
		{
			Orders.Add(order);

			// Увеличиваем счетчик у официанта
			var waiter = Waiters.FirstOrDefault(w => w.Id == order.WaiterId);
			if (waiter != null) waiter.OrdersHandled++;

			foreach (var dish in order.Dishes)
			{
				var suitable = Chefs.Where(c => c.CuisineType == dish.CuisineType)
					.OrderBy(c => c.OrdersHandled)
					.FirstOrDefault();
				if (suitable != null)
				{
					suitable.OrdersHandled++;
					Console.WriteLine($"Блюдо '{dish.Name}' назначено повару {suitable.Name} ({suitable.CuisineType})");
				}
				else
				{
					Console.WriteLine($"Нет поваров для кухни {dish.CuisineType} для блюда {dish.Name}");
				}
			}

			Console.WriteLine($"Заказ {order.Id} принят. Сумма: {order.TotalPrice}р\n");
		}

		public void ShowPopularDishes()
		{
			var items = Orders.SelectMany(o => o.Dishes)
				.GroupBy(d => d.Name)
				.Select(g => new { Name = g.Key, Count = g.Count() })
				.OrderByDescending(x => x.Count)
				.ToList();

			if (!items.Any())
			{
				Console.WriteLine("Нет заказанных блюд.");
				return;
			}

			Console.WriteLine("Самые популярные блюда:");
			foreach (var it in items)
				Console.WriteLine($"{it.Name} - заказов: {it.Count}");
		}

		public void ShowHardworkingWaits()
		{
			var items = Waiters.Where(w => w.OrdersHandled >0).OrderByDescending(w => w.OrdersHandled).ToList();
			if (!items.Any())
			{
				Console.WriteLine("Нет данных по работе официантов.");
				return;
			}

			Console.WriteLine("Официанты (по количеству выполненных заказов):");
			foreach (var w in items)
				Console.WriteLine($"{w.Name} - выполнено заказов: {w.OrdersHandled}");
		}

		public void ShowReportAboutCustomers()
		{
			var report = Orders.GroupBy(o => o.CustomerId)
				.Select(g => new
				{
					CustomerId = g.Key,
					TotalDishes = g.Sum(o => o.Dishes.Count),
					TotalOrders = g.Count(),
					TotalSum = g.Sum(o => o.TotalPrice)
				})
				.ToList();

			if (!report.Any())
			{
				Console.WriteLine("Нет данных по клиентам.");
				return;
			}

			Console.WriteLine("Отчет по клиентам:");
			foreach (var r in report)
			{
				var customer = Customers.FirstOrDefault(c => c.Id == r.CustomerId);
				var name = customer?.Name ?? $"Клиент {r.CustomerId}";
				Console.WriteLine($"{name} - сумма заказов: {r.TotalSum}р, число заказов: {r.TotalOrders}, блюд: {r.TotalDishes}");
			}
		}
	}

	internal static class Program
	{
		static void Main()
		{
			var rest = CreateSampleRestaurant();

			while (true)
			{
				Console.WriteLine("1. Сделать заказ");
				Console.WriteLine("2. Показать самые популярные блюда");
				Console.WriteLine("3. Показать отчет о работе официантов");
				Console.WriteLine("4. Показать информацию о клиентах");
				Console.WriteLine("5. Выход");
				Console.Write("Выберите пункт меню: ");
				var key = Console.ReadLine();

				switch (key)
				{
					case "1": DoOrder(rest); break;
					case "2": rest.ShowPopularDishes(); break;
					case "3": rest.ShowHardworkingWaits(); break;
					case "4": rest.ShowReportAboutCustomers(); break;
					case "5": return;
					default: Console.WriteLine("Неверный пункт меню\n"); break;
				}

				Console.WriteLine();
			}
		}

		static Restaurant CreateSampleRestaurant()
		{
			var r = new Restaurant();

			r.Chefs.AddRange(new[] {
				new Chef { Name = "Иван", CuisineType = CuisineType.Japanese },
				new Chef { Name = "Акира", CuisineType = CuisineType.Japanese },
				new Chef { Name = "Марко", CuisineType = CuisineType.Italian }
			});

			r.Waiters.AddRange(new[] {
				new Waiter { Id = 1, Name = "Ольга" },
				new Waiter { Id = 2, Name = "Павел" }
			});

			r.Customers.AddRange(new[] {
				new Customer { Id = 1, Name = "Иванов" },
				new Customer { Id = 2, Name = "Петров" }
			});

			int id = 1;
			r.Dishes.AddRange(new[] {
				new Dish { Id = id++, Name = "Суши", CuisineType = CuisineType.Japanese, Price = 300 },
				new Dish { Id = id++, Name = "Пицца Маргарита", CuisineType = CuisineType.Italian, Price = 450 },
				new Dish { Id = id++, Name = "Ролл Филадельфия", CuisineType = CuisineType.Japanese, Price = 350 },
				new Dish { Id = id++, Name = "Паста Карбонара", CuisineType = CuisineType.Italian, Price = 400 }
			});

			return r;
		}

		static void DoOrder(Restaurant r)
		{
			Console.Write("Введите номер стола: ");
			if (!int.TryParse(Console.ReadLine(), out var table)) { Console.WriteLine("Неверный ввод."); return; }

			Console.WriteLine("Доступные официанты:");
			foreach (var w in r.Waiters) Console.WriteLine(w);
			Console.Write("Введите id официанта: ");
			if (!int.TryParse(Console.ReadLine(), out var waiterId) || r.Waiters.All(w => w.Id != waiterId)) { Console.WriteLine("Неверный официант."); return; }

			Console.WriteLine("Доступные клиенты:");
			foreach (var c in r.Customers) Console.WriteLine($"{c.Id}: {c.Name}");
			Console.Write("Введите id клиента: ");
			if (!int.TryParse(Console.ReadLine(), out var customerId) || r.Customers.All(c => c.Id != customerId)) { Console.WriteLine("Неверный клиент."); return; }

			var order = new Order { TableNumber = table, WaiterId = waiterId, CustomerId = customerId };

			Console.WriteLine("Список блюд (введите id блюда по одному,0 - готово):");
			foreach (var d in r.Dishes) Console.WriteLine(d);

			while (true)
			{
				Console.Write("id блюда: ");
				var s = Console.ReadLine();
				if (!int.TryParse(s, out var dishId)) { Console.WriteLine("Неверный ввод"); continue; }
				if (dishId ==0) break;
				var dish = r.FindDish(dishId);
				if (dish == null) { Console.WriteLine("Блюдо не найдено"); continue; }
				order.AddDish(dish);
				Console.WriteLine($"Добавлено: {dish.Name}");
			}

			if (!order.Dishes.Any()) { Console.WriteLine("Пустой заказ не принят."); return; }

			r.TakeOrder(order);
		}
	}
}
