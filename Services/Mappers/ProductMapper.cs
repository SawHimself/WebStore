namespace Services.Mappers;

public static class ProductMapper
{
    public static Persistence.EfClasses.Product ToEfProduct(Entities.Product product) =>
        new Persistence.EfClasses.Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId
        };

    public static Entities.Product ToEntity(Persistence.EfClasses.Product product) =>
        new Entities.Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId
        };
}