namespace ShopPatternsLab8.Models;

public class CartItem
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }

    public CartItem(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}
