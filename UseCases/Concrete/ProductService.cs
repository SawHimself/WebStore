using Entities;
using Persistence.Repository;
using Persistence.Repository.Concrete;

namespace UseCases.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> CreateProductAsync(Product newProduct)
    {
        return await _repository.CreateProductAsync(newProduct);
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _repository.GetProductsAsync();
    }

    public async Task<List<Product>> GetProductsPaginatedAsync(int pageNumber, int pageSize, string? name, int? category, decimal? minPrice, decimal? maxPrice)
    {
        if (pageNumber < 1 || pageSize < 1) throw new ArgumentException("Page number and page size must be greater than 0");
        if(minPrice is < 0) throw new ArgumentException("Minimum price cannot be less than 0");
        if(maxPrice is < 0) throw new ArgumentException("Maximum price cannot be less than 0");
        
        return await _repository.GetProductsPaginatedAsync(pageNumber, pageSize, name, category, minPrice, maxPrice);
    }

    public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct)
    {
        var oldProduct =  await _repository.GetOriginalAsync(productId);
        
        if (oldProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} not found.");
        }
        
        UpdateProductFields(oldProduct, updatedProduct);
        
        return await _repository.UpdateProductAsync(oldProduct);
    }

    public async Task<Product?> DeleteProductAsync(int productId)
    { 
        var removedProduct = await _repository.GetOriginalAsync(productId);
        
        if (removedProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} not found.");
        }
        
        return await _repository.DeleteProductAsync(productId);
    }
    
    private static void UpdateProductFields(Product existingProduct, Product newProduct)
    {
        existingProduct.Name = newProduct.Name;
        existingProduct.Description = newProduct.Description;
        existingProduct.Price = newProduct.Price;
        existingProduct.CategoryId = newProduct.CategoryId;
    }
}