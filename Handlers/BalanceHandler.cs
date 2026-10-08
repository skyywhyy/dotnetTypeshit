using ShopPatternsLab8.Orders;

namespace ShopPatternsLab8.Handlers;

public class BalanceHandler : OrderHandler
{
    protected override bool Check(OrderContext order, out string message)
    {
        bool ok = order.Balance >= order.Total;
        message = ok ? "Средств достаточно." : $"Ошибка: не хватает {order.Total - order.Balance:N2} ₽.";
        Console.WriteLine(message);
        return ok;
    }
}
