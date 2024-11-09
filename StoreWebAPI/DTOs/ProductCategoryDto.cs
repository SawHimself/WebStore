using System.ComponentModel.DataAnnotations;
using Entities;

namespace StoreWebAPI.DTOs;

public class ProductCategoryDto
{
    [Required (ErrorMessage = "Name not specified")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "The line length must be from 3 to 50 characters")]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}