using ApiSmartHome.Configuration;
using ApiSmartHome.Contracts.Validation;
using ApiSmartHome.Data;
using ApiSmartHome.Data.Repository;
using ApiSmartHome.MappingProfiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Загружаем конфигурацию из файлов
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json")
    .AddJsonFile("HomeOptions.json");

builder.Logging.ClearProviders();           // Убираем все дефолтные логгеры
builder.Logging.AddConsole();                // Логируем в консоль (IDE, терминал)
builder.Logging.AddDebug();                  // Логируем в Output (Visual Studio)
builder.Logging.AddEventSourceLogger();      // Логируем в системные события Windows (ETW)

// Добавление настроек из файла конфигурации
builder.Services.AddOptions<HomeOptions>()
    .Bind(builder.Configuration)
    .ValidateDataAnnotations() // Можно добавить валидацию с аттрибутами данных, если нужно
.ValidateOnStart(); // Опционально: проверка на ошибки в данных при старте приложения

// Регистрация сервисов репозиториев
builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// Настраиваем контекст базы данных
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

// Подключаем валидацию
builder.Services.AddFluentValidationAutoValidation();
//Это просканирует текущую сборку и зарегистрирует все валидаторы, которые наследуются от AbstractValidator<T>
builder.Services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();

// Подключаем автомаппинг. Получаем все сборки, которые могут содержать Profile
var assembly = typeof(MappingProfile).Assembly;
builder.Services.AddAutoMapper(assembly);

// Add services to the container.

builder.Services.AddControllers();

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
