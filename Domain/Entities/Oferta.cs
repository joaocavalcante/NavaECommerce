using System;

namespace Domain.Entities
{
    public class Oferta
    {
        public Guid Id { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal Preco { get; private set; }
        public DateTime HoraDisponivel { get; private set; }

        // Construtor
        public Oferta(string produtoNome, int quantidade, decimal preco, DateTime horaDisponivel)
        {
            Id = Guid.NewGuid();
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            Preco = preco;
            HoraDisponivel = horaDisponivel;
        }

        // Métodos de domínio
        public void AtualizarQuantidade(int novaQuantidade)
        {
            Quantidade = novaQuantidade;
        }

        public void AtualizarPreco(decimal novoPreco)
        {
            Preco = novoPreco;
        }
    }
}
