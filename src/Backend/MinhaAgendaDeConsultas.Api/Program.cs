using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinhaAgendaDeConsultas.Api.Filtros;
using MinhaAgendaDeConsultas.Api.Token;
using MinhaAgendaDeConsultas.Application;
using MinhaAgendaDeConsultas.Application.Services;
using MinhaAgendaDeConsultas.Domain.Seguranca.Token;
using MinhaAgendaDeConsultas.Infraestrutura;
using MinhaAgendaDeConsultas.Infraestrutura.Logging;
using MinhaAgendaDeConsultas.Infraestrutura.Migrations;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddControllers();

//builder.Services.AddScoped<IPasswordEncripter, Sha512Encripter>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);



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

// Register services  
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();
//builder.Services.AddScoped<AutenticandoUsuarioFiltro>();
//builder.Services.AddScoped<UsuarioLogado>();


builder.Services.AddHttpContextAccessor();

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


var Secret = builder.Configuration.GetSection("Jwt").GetValue<string>("ChaveAssinatura");

var key = Encoding.ASCII.GetBytes(Secret);


// Adiciona a autenticação JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,            //Valida se o Issuer é válido 
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,                  //Indica que se deve validar a origem do token
        ValidateAudience = true,              //indica se vai validar uma url específica
        ValidAudience="*",
        ValidIssuer= "MinhaAgendaDeConsultas.Api"

    };

    // Eventos para personalizar erros de autenticação/autorização
    x.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = async context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"Falha na autenticação. Token inválido ou expirado.\"}");
        },
        OnChallenge = async context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"Acesso negado. Token ausente ou inválido.\"}");
        },
        OnForbidden = async context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\": \"Sem permissão para acessar este recurso.\"}");
        }
    };
});

//Adiciona as Polices de segurança 
builder.Services.AddAuthorization();


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("TipoUsuarioPolicy", policy =>
//    {
//        policy.RequireClaim("TipoUsuario", "Medico", "Paciente");
        
//    });   
//});



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
