using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Features.Clientes.Handlers;
using PrevClientes.Application.Features.Clientes.Queries;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using PrevClientes.Domain.Models;

namespace PrevClientes.Test.Features.Clientes
{
    [TestFixture]
    public class ObterTodosClientesQueryHandlerTests
    {
        private Mock<IClienteRepository> _clienteRepositoryMock;
        private ObterTodosClientesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _handler = new ObterTodosClientesQueryHandler(_clienteRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnListOfClientesDTO()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                 new Cliente("Cliente 1", "24728882012", "cliente1@hotmail.com", 30, new DateTime(1993, 1, 1)),
                 new Cliente("Cliente 2", "87044236073", "cliente2@hotmail.com", 35, new DateTime(1988, 5, 10))
            };


            _clienteRepositoryMock
                .Setup(r => r.ObterTodosClientes())
                .ReturnsAsync(clientes);

            // Act
            var query = new ObterTodosClientesQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            result[0].Id.Should().Be(1);
            result[0].Nome.Should().Be("Cliente 1");
            result[0].Cpf.Should().Be("24728882012");
            result[0].Email.Should().Be("cliente1@hotmail.com");
            result[0].Idade.Should().Be(30);
            result[0].DataNascimento.Should().Be(new DateTime(1993, 1, 1));

            result[1].Id.Should().Be(2);
            result[1].Nome.Should().Be("Cliente 2");
            result[1].Cpf.Should().Be("87044236073");
            result[1].Email.Should().Be("cliente2@hotmail.com");
            result[1].Idade.Should().Be(35);
            result[1].DataNascimento.Should().Be(new DateTime(1988, 5, 10));
        }
    }
}
