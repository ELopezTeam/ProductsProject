using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Services;
using ProductApi.Application.Validators;
using ProductApi.Domain.Interfaces;
using ProductApi.Infrastructure.Cache;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Middleware;
using ProductApi.Infrastructure.Repositories;
using ProductApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>();
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();


var sqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(sqlConnection, sqlserveroptions => sqlserveroptions.CommandTimeout(60)));

// Registro de servicios e infraestructura
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDiscountService, DiscountService>();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Configuración del middleware
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();