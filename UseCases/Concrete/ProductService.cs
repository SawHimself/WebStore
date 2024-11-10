using Entities;
using Persistence.Repository;
using Services.Logging;
using Services.Mappers;

namespace UseCases.Concrete;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly ILoggerManager _logger;

    public ProductService(IProductRepository repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Product?> CreateProductAsync(Product newProduct)
    {
        var addedProduct = ProductMapper.ToEntity(await _repository.CreateProductAsync(ProductMapper.ToEfProduct(newProduct)));
        _logger.LogInfo($"Created Product: {addedProduct.Id}, {addedProduct.Name}");
        return addedProduct;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        var efProducts = await _repository.GetProductsAsync();
        return efProducts.Select(ProductMapper.ToEntity).ToList();
    }

    public async Task<List<Product>> GetProductsPaginatedAsync(int pageNumber, int pageSize, string? name, int? category, decimal? minPrice, decimal? maxPrice)
    {
        if (pageNumber < 1 || pageSize < 1) throw new ArgumentException("Page number and page size must be greater than 0");
        if(minPrice is < 0) throw new ArgumentException("Minimum price cannot be less than 0");
        if(maxPrice is < 0) throw new ArgumentException("Maximum price cannot be less than 0");
        
        var productsPaginated = await _repository.GetProductsPaginatedAsync(pageNumber, pageSize, name, category, minPrice, maxPrice);
        return productsPaginated.Select(ProductMapper.ToEntity).ToList();
    }

    public async Task<Product?> UpdateProductAsync(int productId, Product updatedProduct, IProductCategoryRepository categoryRepository)
    {
        var oldProduct =  await _repository.GetOriginalAsync(productId);
        
        if (oldProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} not found.");
        }
        
        var oldCategory = await categoryRepository.GetOriginalAsync(updatedProduct.CategoryId);
        if (oldCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {updatedProduct.CategoryId} not found.");
        }
        
        
        UpdateProductFields(ProductMapper.ToEntity(oldProduct), updatedProduct);
        
        var result = ProductMapper.ToEntity(await _repository.UpdateProductAsync(oldProduct));
        _logger.LogInfo($"Updated Product: {updatedProduct.Id}, {updatedProduct.Name}");
        
        return updatedProduct;
    }

    public async Task<Product?> DeleteProductAsync(int productId)
    { 
        var originalProduct = await _repository.GetOriginalAsync(productId);
        
        if (originalProduct == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} not found.");
        }

        var deletedProduct = ProductMapper.ToEntity(await _repository.DeleteProductAsync(productId));
        
        _logger.LogInfo($"Deleted Product: {originalProduct.Id}, {originalProduct.Name}");
        return deletedProduct;
    }
    
    private static void UpdateProductFields(Product existingProduct, Product newProduct)
    {
        if(!string.IsNullOrEmpty(newProduct.Name))
            existingProduct.Name = newProduct.Name;
        if(!string.IsNullOrEmpty(newProduct.Description))
            existingProduct.Description = newProduct.Description;
        existingProduct.Price = newProduct.Price;
        existingProduct.CategoryId = newProduct.CategoryId;
    }
}