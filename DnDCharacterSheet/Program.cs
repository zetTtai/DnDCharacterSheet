using Converters;
using DTOs;
using Factories;
using Interfaces;
using Models;
using Services;
using Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IUtilsService, UtilsService>();
builder.Services.AddSingleton<IConverter<Capability, CapabilityDTO>, CapabilityConverter>();
builder.Services.AddSingleton<IConverter<Sheet, SheetDTO>, SheetConverter>();

builder.Services.AddSingleton<ISettingAttributeStrategyFactory, SettingAttributeStrategyFactory>();
builder.Services.AddSingleton<IAttributeSettingStrategy, RollingDiceStrategy>();
builder.Services.AddSingleton<ISheetService, SheetService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
