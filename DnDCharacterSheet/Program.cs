using Converters;
using DnDCharacterSheet;
using DTOs;
using Factories;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Middlewares;
using Models;
using Repositories;
using Services;
using Strategies;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Configuration["Environment"];
var serverName = builder.Configuration["DatabaseSettings:" + environment + ":ServerName"];
var databaseName = builder.Configuration["DatabaseSettings:" + environment + ":DatabaseName"];
var useTrustedConnection = builder.Configuration["DatabaseSettings:" + environment + ":UseTrustedConnection"];


var connectionString = $"Server={serverName};Database={databaseName};" +
                       $"Trusted_Connection={useTrustedConnection};";

if (environment == "Development")
{
    connectionString += "Encrypt=False;";
}

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ICoinRepository, CoinRepository>();

// Add services to the container.
builder.Services.AddSingleton<IUtilsService, UtilsService>();
builder.Services.AddSingleton<IConverter<Capability, CapabilityDTO>, CapabilityConverter>();
builder.Services.AddSingleton<IConverter<Sheet, SheetDTO>, SheetConverter>();

builder.Services.AddSingleton<ISettingAttributeStrategyFactory, SettingAttributeStrategyFactory>();
builder.Services.AddSingleton<IAttributeSettingStrategy, RollingDiceStrategy>();
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
