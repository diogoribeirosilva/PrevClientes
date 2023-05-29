using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using PrevClientes.Application.DTO;
using PrevClientes.Application.DTO.DTO;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Application.Features.Clientes.Commands;
using PrevClientes.Domain.Core.Interfaces.Repositories;
using PrevClientes.Domain.Models;

namespace PrevClientes.Test.Features.Clientes
{
    [TestFixture]
    public class AtualizarClienteCommandHandlerTests
    {
        private Mock<IClienteRepository> _clienteRepositoryMock;
        private Mock<IValidator<AtualizarClienteCommand>> _validatorMock;
        private AtualizarClienteCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _validatorMock = new Mock<IValidator<AtualizarClienteCommand>>();
            _handler = new AtualizarClienteCommandHandler(
                _clienteRepositoryMock.Object,
                _validatorMock.Object
            );
        }

        [Test]
        public async Task Handle_WhenCommandIsValid_ShouldUpdateClienteAndReturnTrue()
        {
            // Arrange
            var command = new AtualizarClienteCommand
            {
                ClienteId = 1,
                ClienteDTO = new ClienteDTO
                {
                    Nome = "Novo Nome",
                    Cpf = "87044236073",
                    Email = "novoemail@teste.com",
                    Idade = 30,
                    DataNascimento = new DateTime(1993, 1, 1)
                }
            };

            var validationResult = new ValidationResult();
            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<AtualizarClienteCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            var existingCliente = new Cliente(
               "Nome Existente",
               "87044236073",
               "email@hotmail.com",
               25,
               new DateTime(1998, 1, 1)
           );

            _clienteRepositoryMock
                .Setup(r => r.ObterClientePorId(command.ClienteId))
                .ReturnsAsync(existingCliente);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            existingCliente.Nome.Should().Be(command.ClienteDTO.Nome);
            existingCliente.Cpf.Should().Be(command.ClienteDTO.Cpf);
            existingCliente.Email.Should().Be(command.ClienteDTO.Email);
            existingCliente.Idade.Should().Be(command.ClienteDTO.Idade);
            existingCliente.DataNascimento.Should().Be(command.ClienteDTO.DataNascimento);

            _clienteRepositoryMock.Verify(r => r.AtualizarCliente(existingCliente), Times.Once);
        }

        [Test]
        public async Task Handle_WhenCommandIsInvalid_ShouldThrowValidationException()
        {
            // Arrange
            var command = new AtualizarClienteCommand();

            var validationResult = new ValidationResult();
            validationResult.Errors.Add(new ValidationFailure("Nome", "O nome é obrigatório."));
            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<AtualizarClienteCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>().WithMessage("Aconteceram uma ou mais falhas de validação.");
            _clienteRepositoryMock.Verify(r => r.AtualizarCliente(It.IsAny<Cliente>()), Times.Never);
        }

        [Test]
        public async Task Handle_WhenClienteNotFound_ShouldReturnFalse()
        {
            // Arrange
            var command = new AtualizarClienteCommand
            {
                ClienteId = 1,
                ClienteDTO = new ClienteDTO()
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<AtualizarClienteCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _clienteRepositoryMock
                .Setup(r => r.ObterClientePorId(command.ClienteId))
                .ReturnsAsync((Cliente)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeFalse();
            _clienteRepositoryMock.Verify(r => r.AtualizarCliente(It.IsAny<Cliente>()), Times.Never);
        }
    }
}
