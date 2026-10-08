using ShopPatternsLab8.Models;
using ShopPatternsLab8.Orders;

namespace ShopPatternsLab8.Handlers;

public class StockHandler : OrderHandler
{
    protected override bool Check(OrderContext order, out string message)
    {
        foreach (CartItem item in order.Cart.Items)
        {
            if (!order.Catalog.TryGetValue(item.ProductId, out Product? product) || item.Quantity > product.Stock)
            {
                message = $"Ошибка: товара #{item.ProductId} недостаточно на складе.";
                Console.WriteLine(message);
                return false;
            }
        }
        message = "Все товары есть на складе.";
        Console.WriteLine(message);
        return true;
    }
}
