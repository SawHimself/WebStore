using Entities;
using Persistence.Repository;

namespace UseCases.Concrete;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _repository;

    public ProductCategoryService(IProductCategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ProductCategory?> CreateProductCategoryAsync(ProductCategory newProductCategory)
    {
        return await _repository.CreateProductCategoryAsync(newProductCategory);
    }

    public async Task<List<ProductCategory>?> GetProductCategoriesAsync()
    {
        return await _repository.GetProductCategoriesAsync();
    }
    public async Task<ProductCategory?> UpdateProductCategoryAsync(int categoryId, ProductCategory category)
    {
        var oldCategory = await _repository.GetOriginalAsync(categoryId);
        
        if (oldCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }
        
        UpdateCategoryFields(oldCategory, category);
        
        return await _repository.UpdateProductCategoryAsync(oldCategory);
    }

    public async Task<ProductCategory?> DeleteProductCategoryAsync(int categoryId)
    {
        var removedCategory = _repository.GetOriginalAsync(categoryId);
        if (removedCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }
        
        return await _repository.DeleteProductCategoryAsync(categoryId);
    }
    
    private static void UpdateCategoryFields(ProductCategory existingCategory, ProductCategory newCategory)
    {
        existingCategory.Name = newCategory.Name;
        existingCategory.Description = newCategory.Description;
    }
}