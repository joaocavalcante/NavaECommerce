using System;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int Estoque { get; private set; }

        // Construtor
        public Produto(string nome, string descricao, decimal preco, int estoque)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Estoque = estoque;
        }

        // Métodos de domínio
        public void AtualizarDetalhes(string nome, string descricao, decimal preco)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
        }

        public void AdicionarEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainException("Quantidade deve ser maior que zero.");

            Estoque += quantidade;
        }

        public void RemoverEstoque(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainException("Quantidade deve ser maior que zero.");
            if (quantidade > Estoque)
                throw new DomainException("Estoque insuficiente.");

            Estoque -= quantidade;
        }
    }
}
