namespace ShopPatternsLab8.Cart;

// Caretaker
public class CartHistory
{
    private readonly Stack<CartMemento> _snapshots = new();
    public void Push(CartMemento snapshot) => _snapshots.Push(snapshot);
    public bool TryPop(out CartMemento? snapshot) => _snapshots.TryPop(out snapshot);
    public int Count => _snapshots.Count;
    public void Clear() => _snapshots.Clear();
}
