namespace Persistence.EfClasses;

public class ProductCategory
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public List<Product>? Products { get; set; } = [];
}