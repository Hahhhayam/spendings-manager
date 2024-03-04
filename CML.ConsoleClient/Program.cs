using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DAL.Context;
using CML.ConsoleClient.Core;
using Mapster;
using MapsterMapper;
using BLL.Services;

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
        services.AddDbContext<SMDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddSingleton<Application>();

        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddScoped<DebtService>();
        services.AddScoped<TransactionsService>();
        services.AddScoped<CurrencyService>();
        services.AddScoped<TagService>();
        services.AddScoped<MoneyFormatService>();
        services.AddScoped<PersonService>();
        
    })
    .Build();

builder.Start();

var app = builder.Services.GetService<Application>();
