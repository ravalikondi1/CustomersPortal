using CustomersAPI.DataAccess;
using CustomersAPI.Services;
using CustomersAPI.Validators;
using Microsoft.EntityFrameworkCore;
//using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IValidator, Validator>();

builder.Services.AddDbContext<CustomerDbContext>(option =>
option.UseInMemoryDatabase("CustomerDB"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
CustomerDbContext context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
context.Database.EnsureCreated();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
