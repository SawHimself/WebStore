namespace Services.Mappers;

public static class ProductCategoryMapper
{
    public static Persistence.EfClasses.ProductCategory ToEfProduct(Entities.ProductCategory productCategory) =>
        new Persistence.EfClasses.ProductCategory
        {
            Id = productCategory.Id,
            Description = productCategory.Description,
            Name = productCategory.Name
        };

    public static Entities.ProductCategory ToEntities(Persistence.EfClasses.ProductCategory productCategory) =>
        new Entities.ProductCategory
        {
            Id = productCategory.Id,
            Description = productCategory.Description,
            Name = productCategory.Name
        };
}