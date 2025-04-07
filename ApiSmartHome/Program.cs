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

// ��������� ������������ �� ������
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.Development.json")
    .AddJsonFile("HomeOptions.json");

builder.Logging.ClearProviders();           // ������� ��� ��������� �������
builder.Logging.AddConsole();                // �������� � ������� (IDE, ��������)
builder.Logging.AddDebug();                  // �������� � Output (Visual Studio)
builder.Logging.AddEventSourceLogger();      // �������� � ��������� ������� Windows (ETW)

// ���������� �������� �� ����� ������������
builder.Services.AddOptions<HomeOptions>()
    .Bind(builder.Configuration)
    .ValidateDataAnnotations() // ����� �������� ��������� � ����������� ������, ���� �����
.ValidateOnStart(); // �����������: �������� �� ������ � ������ ��� ������ ����������

// ����������� �������� ������������
builder.Services.AddSingleton<IDeviceRepository, DeviceRepository>();
builder.Services.AddSingleton<IRoomRepository, RoomRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

// ����������� �������� ���� ������
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

// ���������� ���������
builder.Services.AddFluentValidationAutoValidation();
//��� ������������ ������� ������ � �������������� ��� ����������, ������� ����������� �� AbstractValidator<T>
builder.Services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>();

// ���������� �����������. �������� ��� ������, ������� ����� ��������� Profile
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
