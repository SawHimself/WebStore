using Persistence.EfClasses;

namespace Persistence.Repository;

public interface IProductCategoryRepository
{
    public Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory);
    public Task<List<ProductCategory>> GetProductCategoriesAsync();
    public Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory updatedProductCategory);
    public Task<ProductCategory> DeleteProductCategoryAsync(int productCategoryId);
    public Task<ProductCategory?> GetOriginalAsync(int productCategoryId);
}