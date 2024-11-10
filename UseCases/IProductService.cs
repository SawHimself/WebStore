using Entities;
using Persistence.Repository;

namespace UseCases;

public interface IProductService
{
    public Task<Product?> CreateProductAsync(Product newProduct);
    public Task<List<Product>> GetProductsAsync();
    public Task<List<Product>> GetProductsPaginatedAsync(
        int pageNumber, 
        int pageSize,
        string? name,
        int? category, 
        decimal? minPrice, 
        decimal? maxPrice);
    public Task<Product?> UpdateProductAsync(int productId, Product product, IProductCategoryRepository productCategoryRepository);
    public Task<Product?> DeleteProductAsync(int productId);
}