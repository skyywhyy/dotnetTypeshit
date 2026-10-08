using ShopPatternsLab8.Cart;
using ShopPatternsLab8.Handlers;
using ShopPatternsLab8.Models;
using ShopPatternsLab8.Orders;

namespace ShopPatternsLab8;

public static class Program
{
    private static readonly Dictionary<int, Product> Catalog = new()
    {
        [1] = new Product(1, "Клавиатура", 3000m, 3),
        [2] = new Product(2, "Мышь", 1500m, 5),
        [3] = new Product(3, "Наушники", 4500m, 2),
    };

    public static void Main()
    {
        var cart = new ShoppingCart();
        var history = new CartHistory();
        decimal balance = 7000m;
        var chain = new NonEmptyCartHandler();
        chain.SetNext(new StockHandler()).SetNext(new BalanceHandler());

        while (true)
        {
            Console.WriteLine("\n=== ИНТЕРНЕТ-МАГАЗИН ===");
            Console.WriteLine($"Баланс: {balance:N2} ₽ | сохранённых состояний: {history.Count}");
            Console.WriteLine("1 — Каталог; 2 — Корзина; 3 — Добавить товар; 4 — Удалить единицу товара");
            Console.WriteLine("5 — Отменить изменение; 6 — Оформить заказ; 0 — Выход");
            Console.Write("Выбор: ");
            string? command = Console.ReadLine();
            try
            {
                switch (command)
                {
                    case "1":
                        foreach (Product p in Catalog.Values)
                            Console.WriteLine($"#{p.Id} {p.Name}: {p.Price:N2} ₽, в наличии: {p.Stock}");
                        break;
                    case "2":
                        if (cart.Items.Count == 0) Console.WriteLine("Корзина пуста.");
                        foreach (CartItem item in cart.Items)
                        {
                            Product p = Catalog[item.ProductId];
                            Console.WriteLine($"{p.Name} x{item.Quantity} = {p.Price * item.Quantity:N2} ₽");
                        }
                        Console.WriteLine($"Итого: {cart.Items.Sum(x => Catalog[x.ProductId].Price * x.Quantity):N2} ₽");
                        break;
                    case "3":
                    {
                        int id = ReadId();
                        if (!Catalog.TryGetValue(id, out Product? p)) { Console.WriteLine("Товар не найден."); break; }
                        CartMemento before = cart.Save();
                        cart.Add(p);
                        history.Push(before);
                        Console.WriteLine("Товар добавлен, предыдущее состояние сохранено.");
                        break;
                    }
                    case "4":
                    {
                        int id = ReadId();
                        CartMemento before = cart.Save();
                        cart.RemoveOne(id);
                        history.Push(before);
                        Console.WriteLine("Количество уменьшено, предыдущее состояние сохранено.");
                        break;
                    }
                    case "5":
                        if (history.TryPop(out CartMemento? snapshot) && snapshot != null)
                        { cart.Restore(snapshot); Console.WriteLine("Корзина восстановлена."); }
                        else Console.WriteLine("Нет изменений для отмены.");
                        break;
                    case "6":
                    {
                        var order = new OrderContext { Cart = cart, Catalog = Catalog, Balance = balance };
                        Console.WriteLine("Проверка заказа:");
                        if (!chain.Handle(order, out _)) { Console.WriteLine("Заказ отклонён."); break; }
                        decimal total = order.Total;
                        balance -= total;
                        foreach (CartItem item in cart.Items)
                        {
                            Product product = Catalog[item.ProductId];
                            Catalog[item.ProductId] = new Product(product.Id, product.Name, product.Price, product.Stock - item.Quantity);
                        }
                        cart.Clear();
                        history.Clear();
                        Console.WriteLine($"Заказ оформлен! Оплачено: {total:N2} ₽.");
                        break;
                    }
                    case "0": return;
                    default: Console.WriteLine("Неизвестная команда."); break;
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException or FormatException)
            { Console.WriteLine(ex.Message); }
        }
    }

    private static int ReadId()
    {
        Console.Write("Введите ID товара: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
            throw new FormatException("Нужно ввести целое число.");
        return id;
    }
}
