using EventPlusWebAPI.BdContextEvent;
using EventPlusWebAPI.Interfaces;
using EventPlusWebAPI.Models;
using EventPlusWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Configuração do EFcore - Banco de dados 
builder.Services.AddDbContext<EventContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default Connection")));
//Injeção de dependência 
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>(); //UMA INSTANCIA NOVA É CRIADA POR REQ HTTP E ISSO GARANTE QUE CADA REQ TENHA SEU PRÓPIO CONTEXTO ISOLADO
builder.Services.AddScoped<ITipoEvento, TipoEventoRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository>();
builder.Services.AddScoped<IInstituicao, InstituicaoRepository>();
builder.Services.AddScoped<IPresenca, PresencaRepository>();
builder.Services.AddScoped<IComentario, ComentarioRepository>();
builder.Services.AddScoped<IEvento, EventoRepository>();
builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run();
