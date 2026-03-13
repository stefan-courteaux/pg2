using WebShoppie.Domain.Services;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Persistence;
using WebShoppie.Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
app.MapControllers();

app.UseExceptionHandler(app.Environment.IsDevelopment() ? "/error-development" : "/error");

app.Run();