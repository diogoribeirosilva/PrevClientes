using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PrevClientes.DependencyInjection;
using PrevClientes.Infrastruture.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Registro do serviço MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Configuração da injeção de dependência
DependencyInjectionConfig.ConfigureDependencies(builder.Services);

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrevClientes", Version = "v1" });
});

// Configuração do contexto do banco de dados
builder.Services.AddDbContext<SqlContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.MultipleCollectionIncludeWarning))
    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));

});
builder.Services.AddMemoryCache();
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