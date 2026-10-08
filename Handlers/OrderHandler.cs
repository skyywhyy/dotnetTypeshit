using ShopPatternsLab8.Orders;

namespace ShopPatternsLab8.Handlers;


public abstract class OrderHandler
{
    private OrderHandler? _next;
    public OrderHandler SetNext(OrderHandler next) { _next = next; return next; }
    public bool Handle(OrderContext order, out string message)
    {
        if (!Check(order, out message)) return false;
        if (_next is not null) return _next.Handle(order, out message);
        message = "Все проверки пройдены.";
        return true;
    }
    protected abstract bool Check(OrderContext order, out string message);
}
