using FluentValidation;
using PrevClientes.Application.Featrures.Clientes.Commands;
using PrevClientes.Application.Featrures.Clientes.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrevClientes.Application.Featrures.Clientes.Validator
{
    public class AtualizarClienteCommandValidator : AbstractValidator<AtualizarClienteCommand>
    {
        public AtualizarClienteCommandValidator()
        {
            RuleFor(command => command.ClienteDTO.Nome).NotEmpty().WithMessage("O nome do cliente é obrigatório.");
            RuleFor(command => command.ClienteDTO.Cpf).NotEmpty().WithMessage("O CPF do cliente é obrigatório.").Must(ValidarCpf).WithMessage("O CPF do cliente é inválido.");
            RuleFor(command => command.ClienteDTO.Email).NotEmpty().WithMessage("O e-mail do cliente é obrigatório.").EmailAddress().WithMessage("O e-mail do cliente é inválido.");
            RuleFor(command => command.ClienteDTO.Idade).GreaterThanOrEqualTo(18).WithMessage("O cliente deve ter pelo menos 18 anos de idade.");
            RuleFor(command => command.ClienteDTO.DataNascimento).NotEmpty().WithMessage("A data de nascimento do cliente é obrigatória.");
        }

        private bool ValidarCpf(string cpf)
        {
            return CpfValidator.IsValid(cpf);
        }

    }
}
