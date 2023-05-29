using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Features.Clientes.Commands;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using PrevClientes.Domain.Models;

namespace PrevClientes.Test.Features.Clientes
{
    [TestFixture]
    public class CriarClienteCommandHandlerTests
    {
        private Mock<IClienteRepository> _clienteRepositoryMock;
        private CriarClienteCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _handler = new CriarClienteCommandHandler(_clienteRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_WhenCommandIsValid_ShouldCreateClienteAndReturnClienteId()
        {
            // Arrange
            var clienteDTO = new ClienteDTO
            {
                Nome = "Nome do Cliente",
                Cpf = "87044236073",
                Email = "cliente@hotmail.com",
                Idade = 30,
                DataNascimento = new DateTime(1993, 1, 1)
            };

            var command = new CriarClienteCommand(clienteDTO);

            var validationResult = new ValidationResult();
            var validatorMock = new Mock<IValidator<CriarClienteCommand>>();
            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<CriarClienteCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            _handler = new CriarClienteCommandHandler(_clienteRepositoryMock.Object);

            var clienteId = 1;
            _clienteRepositoryMock
                .Setup(r => r.CriarCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(clienteId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().Be(clienteId);
            _clienteRepositoryMock.Verify(r => r.CriarCliente(It.IsAny<Cliente>()), Times.Once);
        }

        [Test]
        public async Task Handle_WhenCommandIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var clienteDTO = new ClienteDTO();
            var command = new CriarClienteCommand(clienteDTO);

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Nome", "O nome é obrigatório."));
            var validatorMock = new Mock<IValidator<CriarClienteCommand>>();
            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<CriarClienteCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            _handler = new CriarClienteCommandHandler(_clienteRepositoryMock.Object);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("Aconteceram uma ou mais falhas de validação.");
            _clienteRepositoryMock.Verify(r => r.CriarCliente(It.IsAny<Cliente>()), Times.Never);
        }

    }
}
