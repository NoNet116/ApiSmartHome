using ApiSmartHome.Configuration;
using ApiSmartHome.Data.Repository;
using ApiSmartHome.MappingProfiles;

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
    .ValidateDataAnnotations()
.ValidateOnStart();

builder.Services.AddScoped<IUserRepository, UserRepository>();

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
