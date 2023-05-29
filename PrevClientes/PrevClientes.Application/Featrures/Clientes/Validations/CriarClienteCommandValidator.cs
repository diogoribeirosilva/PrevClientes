using FluentValidation;
using PrevClientes.Application.Featrures.Clientes.Validations;
using PrevClientes.Application.Features.Clientes.Commands;

namespace PrevClientes.Application.Featrures.Clientes.Validator
{
    public class CriarClienteCommandValidator : AbstractValidator<CriarClienteCommand>
    {
        public CriarClienteCommandValidator()
        {
            RuleFor(command => command.Cliente.Nome).NotEmpty().WithMessage("O nome do cliente é obrigatório.");
            RuleFor(command => command.Cliente.Cpf).NotEmpty().WithMessage("O CPF do cliente é obrigatório.").Must(ValidarCpf).WithMessage("O CPF do cliente é inválido.");
            RuleFor(command => command.Cliente.Email).NotEmpty().WithMessage("O e-mail do cliente é obrigatório.").EmailAddress().WithMessage("O e-mail do cliente é inválido.");
            RuleFor(command => command.Cliente.Idade).GreaterThanOrEqualTo(18).WithMessage("O cliente deve ter pelo menos 18 anos de idade.");
            RuleFor(command => command.Cliente.DataNascimento).NotEmpty().WithMessage("A data de nascimento do cliente é obrigatória.");
        }
        private bool ValidarCpf(string cpf)
        {
            return CpfValidator.IsValid(cpf);
        }

    }
}
