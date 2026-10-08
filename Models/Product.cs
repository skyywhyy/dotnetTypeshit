namespace ShopPatternsLab8.Models;

public class Product
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int Stock { get; init; }

    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }
}
