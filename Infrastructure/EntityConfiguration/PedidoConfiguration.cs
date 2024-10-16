using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.UsuarioId).IsRequired();
            builder.Property(m => m.ValorTotal).HasPrecision(10, 2);
            builder.Property(m => m.Status).IsRequired();
            builder.Property(m => m.DataCriacao).IsRequired();
        }
    }
}
