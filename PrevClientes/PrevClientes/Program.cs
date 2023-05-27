using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrevClientes", Version = "v1" });
});

// Adicionar controllers à aplicação
builder.Services.AddControllers();

var app = builder.Build();

// Habilitar o Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrevClientes v1");
    c.RoutePrefix = string.Empty;
});

// Definir os endpoints da API
app.MapControllers();

app.Run();