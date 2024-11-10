using Persistence.EfClasses;

namespace Persistence.Repository;

public interface IProductRepository
{
    public Task<Product> CreateProductAsync(Product product);
    public Task<List<Product>> GetProductsAsync();
    public Task<List<Product>> GetProductsPaginatedAsync(
        int pageNumber, 
        int pageSize,
        string? name,
        int? categoryId, 
        decimal? minPrice, 
        decimal? maxPrice);
    public Task<Product> UpdateProductAsync(Product product);
    public Task<Product> DeleteProductAsync(int productId);
    public Task<Product?> GetOriginalAsync(int productId);
}