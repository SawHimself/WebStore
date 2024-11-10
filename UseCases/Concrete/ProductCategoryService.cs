using Entities;
using Persistence.Repository;
using Services.Logging;
using Services.Mappers;

namespace UseCases.Concrete;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _repository;
    private readonly ILoggerManager _logger;

    public ProductCategoryService(IProductCategoryRepository repository, ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<ProductCategory?> CreateProductCategoryAsync(ProductCategory newProductCategory)
    {
        var addedCategory = ProductCategoryMapper.ToEntities(
            await _repository.CreateProductCategoryAsync(ProductCategoryMapper.ToEfProduct(newProductCategory)));
        _logger.LogInfo($"Created ProductCategory: {addedCategory.Id}, {addedCategory.Name}");
        return addedCategory;
    }

    public async Task<List<ProductCategory>?> GetProductCategoriesAsync()
    {
        var efCategory = await _repository.GetProductCategoriesAsync();
        return efCategory.Select(ProductCategoryMapper.ToEntities).ToList();
    }
    public async Task<ProductCategory?> UpdateProductCategoryAsync(int categoryId, ProductCategory category)
    {
        var oldCategory = await _repository.GetOriginalAsync(categoryId);
        
        if (oldCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }
        
        UpdateCategoryFields(ProductCategoryMapper.ToEntities(oldCategory), category);

        var updatedCategory =
            ProductCategoryMapper.ToEntities(await _repository.UpdateProductCategoryAsync(oldCategory));
        
        _logger.LogInfo($"Updated ProductCategory: {updatedCategory.Id}, {updatedCategory.Name}");
        
        return updatedCategory;
    }

    public async Task<ProductCategory?> DeleteProductCategoryAsync(int categoryId)
    {
        var removedCategory = _repository.GetOriginalAsync(categoryId);
        if (removedCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
        }

        var deletedCategory =
            ProductCategoryMapper.ToEntities(await _repository.DeleteProductCategoryAsync(categoryId));
        
        _logger.LogInfo($"Deleted ProductCategory: {deletedCategory.Id}, {deletedCategory.Name}");
        
        return deletedCategory;
    }
    
    private static void UpdateCategoryFields(ProductCategory existingCategory, ProductCategory newCategory)
    {
        if(!string.IsNullOrWhiteSpace(existingCategory.Name))
            existingCategory.Name = newCategory.Name;
        if(!string.IsNullOrWhiteSpace(existingCategory.Description))
            existingCategory.Description = newCategory.Description;
    }
}