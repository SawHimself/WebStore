using Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository;
using StoreWebAPI.DTOs;
using UseCases;

namespace StoreWebAPI.Controllers;

[Route("product")]
[ApiController]
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
    [ProducesResponseType(typeof(Product),StatusCodes.Status201Created)]
    public async Task<ActionResult<Product>> AddNewProduct([FromBody] ProductDto productDto, [FromServices] IProductCategoryRepository categoryRepository)
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
            }, categoryRepository);

        if (newProduct == null)
        {
            return StatusCode(500, "Unknown error when creating product");
        }
        
        return CreatedAtAction(nameof(AddNewProduct), new { id = newProduct.Id }, newProduct);
    }
    
    //Read
    [HttpGet("get")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products);
    }
    
    //Update
    [HttpPut("update/{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] ProductDto productDto, [FromServices] IProductCategoryRepository categoryRepository)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedProduct = await _productService.UpdateProductAsync(
            new Product()
            {
                Id = id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
            }
            , categoryRepository);
    
        if (updatedProduct == null)
        {
            return StatusCode(500, $"Unknown error occurred while updating the product with ID {id}.");
        }
        return Ok(updatedProduct);
    }
    
    //Delete
    [HttpDelete("delete/{id:int}")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var deletedProduct = await _productService.DeleteProductAsync(id);
        if (deletedProduct == null)
        {
            return StatusCode(500, $"Unknown error when deleting product with identifier {id}."); 
        }
        return Ok(deletedProduct);
    }
}