using Config;
using Converters;
using DTOs;
using Factories;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Middlewares;
using Models;
using Repositories;
using Services;
using Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(DatabaseSettings.Environment));

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) => 
{
    var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    var connectionString = $"Server={dbSettings.ServerName};" +
                       $"Database={dbSettings.DatabaseName};" +
                       $"Trusted_Connection={dbSettings.UseTrustedConnection};" +
                       $"Encrypt={dbSettings.Encrypt};";
    options.UseSqlServer(connectionString);

});

// Repositories
builder.Services.AddScoped<ICoinRepository, CoinRepository>();

// Add services to the container.
builder.Services.AddSingleton<IUtilsService, UtilsService>();
builder.Services.AddSingleton<IConverter<Capability, CapabilityDTO>, CapabilityConverter>();
builder.Services.AddSingleton<IConverter<Sheet, SheetDTO>, SheetConverter>();
builder.Services.AddSingleton<IConverter<Coin, CoinDTO>, CoinConverter>();

// Strategies
builder.Services.AddSingleton<ISettingAttributeStrategyFactory, SettingAttributeStrategyFactory>();
builder.Services.AddSingleton<IAttributeSettingStrategy, RollingDiceStrategy>();

// Services - Model
builder.Services.AddSingleton<ISheetService, SheetService>();
builder.Services.AddScoped<ICoinService, CoinService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
