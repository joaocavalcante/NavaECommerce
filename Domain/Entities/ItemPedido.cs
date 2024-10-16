using Domain.Exceptions;

namespace Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        // Construtor
        public ItemPedido(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        // Métodos de domínio
        public void AdicionarQuantidade(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void RemoverQuantidade(int quantidade)
        {
            if (Quantidade < quantidade)
                throw new DomainException("Quantidade a remover é maior que a existente.");

            Quantidade -= quantidade;
        }
    }
}
