using System.ComponentModel.DataAnnotations;
using BLL.Services;
using CML.ConsoleClient.Core;
using DAL.Context;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var currentDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..");

#if DEBUG
currentDirectory = Path.Combine(currentDirectory, @"..\..\..");
#endif

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(currentDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var builder = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddDbContext<SpendingsManagerDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddSingleton<Application>();

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddScoped<DebtService>();
        services.AddScoped<TransactionService>();
        services.AddScoped<ContactService>();
        services.AddScoped<StatisticsService>();
    })
    .Build();

builder.Start();

var app = builder.Services.GetService<Application>();
app.Run();
