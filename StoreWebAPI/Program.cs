using Microsoft.EntityFrameworkCore;
using NLog;
using Persistence;
using Persistence.Repository;
using Persistence.Repository.Concrete;
using Services.Logging;
using StoreWebAPI.Extensions;
using UseCases;
using UseCases.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureLoggerService();

// DI
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

// DB
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Persistence")));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Logging
LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();