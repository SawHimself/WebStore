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
    public async Task<ProductCategory?> UpdateProductCategoryAsync(ProductCategory category)
    {
        var oldCategory = await _repository.GetOriginalAsync(category.Id);
        if (oldCategory == null)
        {
            throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
        }
        
        oldCategory.Name = category.Name;
        oldCategory.Description = category.Description;
        
        var updatedCategory =
            ProductCategoryMapper.ToEntities(
                await _repository.UpdateProductCategoryAsync(oldCategory));
        
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
}