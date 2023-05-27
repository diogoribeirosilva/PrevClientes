using System;

namespace PrevClientes.Domain.Models
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public int Idade { get; private set; }
        public DateTime DataNascimento { get; private set; }

        // Construtor
        public Cliente(string nome, string cpf, string email, int idade, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Idade = idade;
            DataNascimento = dataNascimento;
        }

        // Método para atualizar os dados do cliente
        public void AtualizarDados(string nome, string cpf, string email, int idade, DateTime dataNascimento)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Idade = idade;
            DataNascimento = dataNascimento;
        }
    }
}