using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class PagamentoConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.PedidoId).IsRequired();
            builder.Property(m => m.Valor).HasPrecision(10, 2); 
            builder.Property(m => m.Tipo).IsRequired();
            builder.Property(m => m.Status).IsRequired();
            builder.Property(m => m.DataPagamento).IsRequired();
        }
    }
}
