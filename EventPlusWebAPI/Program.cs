using Microsoft.AspNetCore.Authentication.JwtBearer;
using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using EventPlusWebAPI.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//Configuração do EFcore - Banco de dados 

builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//corta o ciclo Usuario-
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//Injeção de dependência 
// Injeção de dependência 
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<ITipoEvento, TipoEventoRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();
builder.Services.AddScoped<IComentario, ComentarioRepository>();
builder.Services.AddScoped<IEvento, EventoRepository>();
builder.Services.AddScoped<IPresenca, PresencaRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();


// Autentificação JWT
// Configurar  como a API vai validar os tokens recebidos nas requisições
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters

    {

        // Valida quem  emitiu token
        ValidateIssuer = true,
        ValidIssuer = "EventPlusWebAPI",

        // Valida para quem o token foi emitido
        ValidateAudience = true,
        ValidAudience = "EventPlusWebAPI",

        // Valida se o token ainda dentro do prazo de validade
        ValidateLifetime = true,

        // Define a tolerancia de clock entre o servidores
        ClockSkew = TimeSpan.FromMinutes(5),

        // chave secreta utilizada para validar a assinatura do token
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes("Jwt:Key")

            )


    };
});

builder.Services.AddAuthentication();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection(); // Redireciona para HTTPS
app.UseAuthentication();   // Verifica a autenticação
app.UseAuthorization();    // Verifica as permissões
app.MapControllers();      // Mapeia os controllers
app.Run();                 // Inicia a aplicação

