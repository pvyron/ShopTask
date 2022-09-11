using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Shop.Api.DataAccess;
using Shop.Api.DataAccess.Interfaces;
using Shop.Api.Services;

var builder = WebApplication.CreateBuilder(args);

string shopDbConnectionString = builder.Configuration.GetConnectionString("ShopDbConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(shopDbConnectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => o.IncludeXmlComments("Documentation\\Shop.Api.xml"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
