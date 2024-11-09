using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.Concrete;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly AppDbContext _context;
    
    public ProductCategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductCategory> CreateProductCategoryAsync(ProductCategory productCategory)
    {
        _context.ProductCategories.Add(productCategory);
        await _context.SaveChangesAsync();
        return productCategory;
    }

    public async Task<List<ProductCategory>> GetProductCategoriesAsync()
    {
        var productCategories = await _context.ProductCategories
            .AsNoTracking()
            .ToListAsync();
        return productCategories;
    }

    public async Task<ProductCategory> UpdateProductCategoryAsync(ProductCategory updatedProductCategory)
    {
        _context.ProductCategories.Update(updatedProductCategory);
        await _context.SaveChangesAsync();
        return updatedProductCategory;
    }

    public async Task<ProductCategory> DeleteProductCategoryAsync(int productCategoryId)
    {
        var productCategory = _context.ProductCategories
            .Include(p => p.Products)
            .Single(pc => pc.Id == productCategoryId);
        _context.ProductCategories.Remove(productCategory);
        
        await _context.SaveChangesAsync();
        
        return productCategory;
    }

    public async Task<ProductCategory?> GetOriginalAsync(int productCategoryId)
    {
        var category = await _context.ProductCategories.SingleOrDefaultAsync(p => p.Id == productCategoryId);
        return category;
    }
}