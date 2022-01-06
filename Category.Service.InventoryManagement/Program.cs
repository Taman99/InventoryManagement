using Category.Service.Context;
using Category.Service.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

var service = builder.Services;

service.AddControllers();
service.AddDbContext<CategoryContext>(option => option.UseSqlServer(config.GetConnectionString("conStr")));
service.AddScoped<ICategoryRepository,CategoryRepository>();
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

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
