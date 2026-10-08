using ShopPatternsLab8.Orders;

namespace ShopPatternsLab8.Handlers;

public class NonEmptyCartHandler : OrderHandler
{
    protected override bool Check(OrderContext order, out string message)
    {
        bool ok = order.Cart.Items.Count > 0;
        message = ok ? "Корзина не пустая." : "Ошибка: корзина пустая.";
        Console.WriteLine(message);
        return ok;
    }
}
