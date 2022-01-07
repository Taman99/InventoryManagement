using ProductService.Context;
using ProductService.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var service = builder.Services;
var config = builder.Configuration;

    service.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
service.AddDbContext<InventoryManagementContext>(op => op.UseSqlServer(config.GetConnectionString("conStr")));
service.AddScoped<IProductRepository, ProductRepository>();

service.AddEndpointsApiExplorer();
    service.AddSwaggerGen();

// End services

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
