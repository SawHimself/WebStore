using System.ComponentModel.DataAnnotations;

namespace Entities;

public class Product
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Description is required.")]
    [MinLength(5, ErrorMessage = "Description must be at least 5 characters long.")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "Price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
    [Required(ErrorMessage = "Category Id is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Category Id must be greater than zero.")]
    public int CategoryId { get; set; }
}