﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DAL.Context;
using CML.ConsoleClient.Core;

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
    })
    .Build();

builder.Start();

var app = builder.Services.GetService<Application>();
