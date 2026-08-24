using Microsoft.AspNetCore.Authentication.JwtBearer;
using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Repositories;
using EventPlusWebAPI.Services;
using EventPlusWebAPI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração do EF Core - Banco de dados
builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Configuração dos Controllers
// Evita problemas com ciclos de referência entre entidades
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddScoped<EmailService>();
// Injeção de dependência
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<ITipoEvento, TipoEventoRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();
builder.Services.AddScoped<IComentario, ComentarioRepository>();
builder.Services.AddScoped<IEvento, EventoRepository>();
builder.Services.AddScoped<IPresenca, PresencaRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
// Configuração do Cloudinary
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("Cloudinary")
);

// Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida quem emitiu o token
        ValidateIssuer = true,
        ValidIssuer = "EventPlusWebAPI",

        // Valida para quem o token foi emitido
        ValidateAudience = true,
        ValidAudience = "EventPlusWebAPI",

        // Valida se o token ainda está dentro da validade
        ValidateLifetime = true,

        // Tolerância de horário entre servidores
        ClockSkew = TimeSpan.FromMinutes(5),

        // Chave usada para validar a assinatura
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:Key"]!
            )
        )
    };
});

// Autorização
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();

// JWT
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();