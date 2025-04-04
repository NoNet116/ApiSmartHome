using ApiSmartHome.Configuration;
using ApiSmartHome.Data.Repository;
using ApiSmartHome.MappingProfiles;

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
    .ValidateDataAnnotations()
.ValidateOnStart();

builder.Services.AddScoped<IUserRepository, UserRepository>();

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
