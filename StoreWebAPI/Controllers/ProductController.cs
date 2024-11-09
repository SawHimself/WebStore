using Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository;
using StoreWebAPI.DTOs;
using UseCases;

namespace StoreWebAPI.Controllers;

[Route("product")]
[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
//[ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)] 
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    //Create
    [HttpPost("add")]
    public async Task<ActionResult<Product>> AddNewProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var newProduct = await _productService.CreateProductAsync(
            new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
            });

        if (newProduct == null)
        {
            return StatusCode(500, "Unknown error when creating product");
        }
        
        return CreatedAtAction(nameof(AddNewProduct), new { id = newProduct.Id }, newProduct);
    }
    
    //Read
    [HttpGet("get")]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }
    
    //Update
    [HttpPut("update/{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedProduct = await _productService.UpdateProductAsync(id, new Product()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
        });
    
        if (updatedProduct == null)
        {
            return StatusCode(500, $"Unknown error occurred while updating the product with ID {id}.");
        }
        return Ok(updatedProduct);
    }
    
    //Delete
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var deletedProduct = await _productService.DeleteProductAsync(id);
        if (deletedProduct == null)
        {
            return StatusCode(500, $"Unknown error when deleting product with identifier {id}."); 
        }
        return deletedProduct;
    }
}