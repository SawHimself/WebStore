using Entities;

namespace UseCases;

public interface IProductCategoryService
{
    public Task<ProductCategory?> CreateProductCategoryAsync(ProductCategory newProductCategory);
    public Task<List<ProductCategory>?> GetProductCategoriesAsync();
    public Task<ProductCategory?> UpdateProductCategoryAsync(int categoryId, ProductCategory category);
    public Task<ProductCategory?> DeleteProductCategoryAsync(int categoryId);
}