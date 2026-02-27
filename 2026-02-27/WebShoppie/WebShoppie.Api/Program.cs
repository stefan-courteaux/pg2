using WebShoppie.Domain.Services;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Persistence;
using WebShoppie.Persistence.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();
app.MapControllers();

app.UseExceptionHandler(app.Environment.IsDevelopment() ? "/error-development" : "/error");

app.Run();