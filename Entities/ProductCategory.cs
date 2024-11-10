using System.ComponentModel.DataAnnotations;

namespace Entities;

public class ProductCategory
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Description is required.")]
    [MinLength(5, ErrorMessage = "Description must be at least 5 characters long.")]
    public string? Description { get; set; }
}