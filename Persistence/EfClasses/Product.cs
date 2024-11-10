using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.EfClasses;

public class Product
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    [ForeignKey(nameof(ProductCategory))]
    public int CategoryId { get; set; }
    
    public ProductCategory? Category { get; set; } = null!;
}