using System;

namespace PrevClientes.Domain.Models
{
    public class Cliente
    {
        public int Id { get;  set; }
        public string Nome { get;  set; }
        public string Cpf { get;  set; }
        public string Email { get;  set; }
        public int Idade { get;  set; }
        public DateTime DataNascimento { get;  set; }

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