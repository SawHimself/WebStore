using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository.Concrete;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Product> CreateProductAsync(Product product)
    { 
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        List<Product> products = await _context.Products
            .AsNoTracking()
            .ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetProductsPaginatedAsync(int pageNumber, int pageSize, string? name , int? categoryId, decimal? minPrice, decimal? maxPrice)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => !categoryId.HasValue || p.CategoryId == categoryId.Value)
            .Where(p => !minPrice.HasValue || p.Price >= minPrice.Value)
            .Where(p => !maxPrice.HasValue || p.Price <= maxPrice.Value)
            .Where(p => string.IsNullOrWhiteSpace(name) || p.Name.Contains(name))
            .OrderBy(p => p.Price)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var existingProduct = _context.Products.Single(p =>p.Id == product.Id);
        
        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.CategoryId = product.CategoryId;
        
        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<Product> DeleteProductAsync(int productId)
    {
        var product = _context.Products.Single(p => p.Id == productId);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> GetOriginalAsync(int productId)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId);
        return product;
    }
}