using ShopPatternsLab8.Models;

namespace ShopPatternsLab8.Cart;


public class CartMemento
{
    public IReadOnlyList<CartItem> Items { get; }
    public CartMemento(IEnumerable<CartItem> items) => Items = items.ToArray();
}
