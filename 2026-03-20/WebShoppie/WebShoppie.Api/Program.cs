using Microsoft.EntityFrameworkCore;
using WebShoppie.DataModel;
using WebShoppie.Domain.Services;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Persistence;
using WebShoppie.Persistence.Dapper;
using WebShoppie.Persistence.EFCore;
using WebShoppie.Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<WebShoppieDbContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "Postgres")));

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, EFCoreCustomerRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, DapperProductRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, DapperOrderRepository>();

var app = builder.Build();
app.MapControllers();

app.UseExceptionHandler(app.Environment.IsDevelopment() ? "/error-development" : "/error");

app.Run();