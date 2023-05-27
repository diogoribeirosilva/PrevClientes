using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Application.Features.Clientes.Commands;
using PrevClientes.Application.Features.Clientes.Handlers;
using PrevClientes.Application.Features.Clientes.Queries;
using PrevClientes.Application.Queries.Clientes;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using PrevClientes.Infrastructure.Repositories;

namespace PrevClientes.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencies(IServiceCollection services)
        {
            // Repositório
            services.AddScoped<IClienteRepository, ClienteRepository>();

            // Handlers
            services.AddScoped<IRequestHandler<CriarClienteCommand, int>, CriarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarClienteCommand, bool>, AtualizarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverClienteCommand, bool>, RemoverClienteCommandHandler>();
            services.AddScoped<IRequestHandler<ObterClientePorIdQuery, ClienteDTO>, ObterClientePorIdQueryHandler>();
            services.AddScoped<IRequestHandler<ObterTodosClientesQuery, List<ClienteDTO>>, ObterTodosClientesQueryHandler>();
        }
    }
}
