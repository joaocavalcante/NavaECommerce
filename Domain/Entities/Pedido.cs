using Domain.Enums;

namespace Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public List<ItemPedido> Itens { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusPedido Status { get; private set; }
        public DateTime DataCriacao { get; private set; }

        // Construtor
        public Pedido(Guid usuarioId)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            Itens = new List<ItemPedido>();
            Status = StatusPedido.Criado;
            DataCriacao = DateTime.UtcNow;
        }

        // Métodos de domínio
        public void AdicionarItem(Guid produtoId, int quantidade, decimal precoUnitario)
        {
            var item = Itens.Find(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                item.AdicionarQuantidade(quantidade);
            }
            else
            {
                Itens.Add(new ItemPedido(produtoId, quantidade, precoUnitario));
            }
            CalcularValorTotal();
        }

        public void RemoverItem(Guid produtoId)
        {
            var item = Itens.Find(i => i.ProdutoId == produtoId);
            if (item != null)
            {
                Itens.Remove(item);
                CalcularValorTotal();
            }
        }

        private void CalcularValorTotal()
        {
            ValorTotal = 0;
            foreach (var item in Itens)
            {
                ValorTotal += item.Quantidade * item.PrecoUnitario;
            }
        }

        public void AtualizarStatus(StatusPedido novoStatus)
        {
            Status = novoStatus;
        }
    }
}
