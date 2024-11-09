using Microsoft.AspNetCore.Mvc;
using Entities;
using StoreWebAPI.DTOs;
using UseCases;

namespace StoreWebAPI.Controllers;

[Route("product_category")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
//[ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)] 
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryService _categoryService;

    public ProductCategoryController(IProductCategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    //Create
    [HttpPost("add")]
    public async Task<ActionResult<ProductCategory>> AddNewCategory([FromBody] ProductCategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var newCategory = await _categoryService.CreateProductCategoryAsync(
            new ProductCategory
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
            });

        if (newCategory == null)
        {
            return StatusCode(500, "Unknown error when creating Category");
        }
        
        return CreatedAtAction(nameof(AddNewCategory), new { id = newCategory.Id }, newCategory);
    }
    
    //Read
    [HttpGet("get")]
    public async Task<ActionResult<List<ProductCategory>>> GetCategories()
    {
        var categories = await _categoryService.GetProductCategoriesAsync();
        return Ok(categories);
    }
    
    //Update
    [HttpPut("update/{id:int}")]
    public async Task<ActionResult<ProductCategory>> UpdateCategory(int id, [FromBody] ProductCategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedCategory = await _categoryService.UpdateProductCategoryAsync(id, new ProductCategory()
        {
            Name = categoryDto.Name,
            Description = categoryDto.Description,
        });
    
        if (updatedCategory == null)
        {
            return StatusCode(500, $"Unknown error occurred while updating the category with ID {id}.");
        }
        return Ok(updatedCategory);
    }
    
    //Delete
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<ProductCategory>> DeleteCategory(int id)
    {
        var deletedCategory = await _categoryService.DeleteProductCategoryAsync(id);
        if (deletedCategory == null)
        {
            return StatusCode(500, $"Unknown error when deleting Category with identifier {id}."); 
        }
        return deletedCategory;
    }
}