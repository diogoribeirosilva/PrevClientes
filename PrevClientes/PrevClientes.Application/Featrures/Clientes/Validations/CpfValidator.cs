using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrevClientes.Application.Featrures.Clientes.Validations
{
    public static class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            // Verifica se o CPF possui 11 dígitos
            if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
            {
                return false;
            }

            // Verifica se o CPF possui apenas dígitos
            if (!Regex.IsMatch(cpf, @"^\d+$"))
            {
                return false;
            }

            // Verifica se todos os dígitos são iguais (CPF inválido)
            if (IsAllDigitsEqual(cpf))
            {
                return false;
            }

            // Calcula o primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            }
            int primeiroDigitoVerificador = 11 - (soma % 11);
            if (primeiroDigitoVerificador >= 10)
            {
                primeiroDigitoVerificador = 0;
            }

            // Verifica o primeiro dígito verificador
            if (int.Parse(cpf[9].ToString()) != primeiroDigitoVerificador)
            {
                return false;
            }

            // Calcula o segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            }
            int segundoDigitoVerificador = 11 - (soma % 11);
            if (segundoDigitoVerificador >= 10)
            {
                segundoDigitoVerificador = 0;
            }

            // Verifica o segundo dígito verificador
            if (int.Parse(cpf[10].ToString()) != segundoDigitoVerificador)
            {
                return false;
            }

            return true;
        }

        private static bool IsAllDigitsEqual(string input)
        {
            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] != input[0])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
