using ShopPatternsLab8.Models;

namespace ShopPatternsLab8.Cart;

// Originator
public class ShoppingCart
{
    private readonly List<CartItem> _items = new();
    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    public CartMemento Save() => new(_items);
    public void Restore(CartMemento snapshot)
    {
        _items.Clear();
        _items.AddRange(snapshot.Items);
    }

    public void Add(Product product)
    {
        int index = _items.FindIndex(x => x.ProductId == product.Id);
        int current = index < 0 ? 0 : _items[index].Quantity;
        if (current >= product.Stock)
            throw new InvalidOperationException("Нельзя добавить больше товаров, чем есть на складе.");
        if (index < 0) _items.Add(new CartItem(product.Id, 1));
        else _items[index] = new CartItem(product.Id, current + 1);
    }

    public void RemoveOne(int productId)
    {
        int index = _items.FindIndex(x => x.ProductId == productId);
        if (index < 0) throw new InvalidOperationException("Товара нет в корзине.");
        CartItem item = _items[index];
        if (item.Quantity == 1) _items.RemoveAt(index);
        else _items[index] = new CartItem(item.ProductId, item.Quantity - 1);
    }

    public void Clear() => _items.Clear();
}
