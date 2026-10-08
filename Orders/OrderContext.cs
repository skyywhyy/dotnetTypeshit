using ShopPatternsLab8.Cart;
using ShopPatternsLab8.Models;

namespace ShopPatternsLab8.Orders;

public class OrderContext
{
    public required ShoppingCart Cart { get; init; }
    public required IReadOnlyDictionary<int, Product> Catalog { get; init; }
    public required decimal Balance { get; init; }
    public decimal Total => Cart.Items.Sum(item => Catalog[item.ProductId].Price * item.Quantity);
}
