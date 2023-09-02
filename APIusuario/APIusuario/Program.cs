using Auth0.AspNetCore.Authentication;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL.Tools;

var builder = WebApplication.CreateBuilder(args);

SqlHelper.conString = builder.Configuration.GetConnectionString("dbHealthChain");

builder.Configuration.AddJsonFile("appsettings.json",
        optional: true,
        reloadOnChange: true);

builder.Services.AddControllersWithViews();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
