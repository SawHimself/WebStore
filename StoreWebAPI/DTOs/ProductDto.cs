using System.ComponentModel.DataAnnotations;

namespace StoreWebAPI.DTOs;

public class ProductDto
{
    [Required (ErrorMessage = "Name not specified")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "The line length must be from 3 to 50 characters")]
    public string Name { get; set; } = null!;
    [Required (ErrorMessage = "Description not specified")]
    public string Description { get; set; } = null!;
    [Required (ErrorMessage = "Price not specified")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}