using Domain.Enums;

namespace Domain.Entities
{
    public class Pagamento
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal Valor { get; private set; }
        public TipoPagamento Tipo { get; private set; }
        public StatusPagamento Status { get; private set; }
        public DateTime DataPagamento { get; private set; }

        // Construtor
        public Pagamento(Guid pedidoId, decimal valor, TipoPagamento tipo)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            Valor = valor;
            Tipo = tipo;
            Status = StatusPagamento.Pendente;
            DataPagamento = DateTime.UtcNow;
        }

        // Métodos de domínio
        public void ConfirmarPagamento()
        {
            Status = StatusPagamento.Confirmado;
        }

        public void CancelarPagamento()
        {
            Status = StatusPagamento.Cancelado;
        }
    }
}
