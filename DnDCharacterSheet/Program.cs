using Converters;
using DTOs;
using Factories;
using Interfaces;
using Middlewares;
using Models;
using Services;
using Strategies;
using System.Text.Json.Serialization;
using Ability = Models.Ability;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IUtilsService, UtilsService>();
builder.Services.AddSingleton<IConverter<Capability, CapabilityDTO>, CapabilityConverter>();
builder.Services.AddSingleton<IConverter<Ability, AbilityDTO>, AbilityConverter>();
builder.Services.AddSingleton<IConverter<Sheet, SheetDTO>, SheetConverter>();
builder.Services.AddSingleton<ISettingAbilitiesStrategyFactory, SettingAbilityStrategyFactory>();
builder.Services.AddSingleton<IAbilitySettingStrategy, RollingDiceStrategy>();
builder.Services.AddSingleton<ISheetService, SheetService>();


builder.Services.AddControllers()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseMiddleware<GlobalErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
