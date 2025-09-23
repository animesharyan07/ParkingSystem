using Models;
using Microsoft.Extensions.Options;
using Services;
using DBSettings;
using Repository;
using MongoDB.Driver;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ParkingDatabaseSetting>(builder.Configuration.GetSection(nameof(ParkingDatabaseSetting)));

builder.Services.AddSingleton<IParkingDatabase>(sp =>
    sp.GetRequiredService<IOptions<ParkingDatabaseSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("ParkingDatabaseSetting:ConnectionString")));

// Register Repository
builder.Services.AddScoped<IParkingRepository, ParkingRepository>();

// Register Services
builder.Services.AddScoped<IParkingServices, ParkingService>();

builder.Services.AddControllers();

builder.Services.AddLogging();
//builder.Logging.ClearProviders();            // Remove default providers
//builder.Logging.AddConsole();                // Add console logging
//builder.Logging.SetMinimumLevel(LogLevel.Information); // Minimum log level


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
