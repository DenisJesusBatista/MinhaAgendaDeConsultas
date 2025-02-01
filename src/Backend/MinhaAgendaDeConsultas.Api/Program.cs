using Microsoft.OpenApi.Models;
using MinhaAgendaDeConsultas.Api.Filtros;
using MinhaAgendaDeConsultas.Application;
using MinhaAgendaDeConsultas.Infraestrutura;
using MinhaAgendaDeConsultas.Application.Services;
using MinhaAgendaDeConsultas.Infraestrutura.Logging;
using MinhaAgendaDeConsultas.Infraestrutura.Migrations;
using System.Reflection;
using MinhaAgendaDeConsultas.Application.Services.Criptografia;
using MinhaAgendaDeConsultas.Application.Services.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddControllers();

builder.Services.AddSingleton<PasswordEncripter>();


// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    string version = "1.0";
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha agenda de consultas", Version = version });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                    \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.
                    \r\n\r\nExample: 'Bearer your_token_goes_here'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new string[] {}
        }
    });
});


// Register Repositories and Application
builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

// Register global exception filter
builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDasExceptions)))
    .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);

// Configuring AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperConfiguracao).Assembly); // Registra o perfil do AutoMapper

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information
}));

var app = builder.Build();

// Enable Swagger UI in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

DatabaseSetup.AtualizarBaseDeDados(builder.Configuration, app);

app.Run();

public partial class Program { }
