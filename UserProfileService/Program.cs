
using Microsoft.EntityFrameworkCore;
using UserProfileService.Context;
using UserProfileService.Repository;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
var service = builder.Services;

service.AddControllers();
service.AddDbContext<InventoryManagementContext>(op => op.UseSqlServer(config.GetConnectionString("conStr")));
service.AddScoped<IUserProfileRepository, UserProfileRepository>();
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

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
