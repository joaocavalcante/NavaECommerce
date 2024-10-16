using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class OfertaConfiguration : IEntityTypeConfiguration<Oferta>
    {
        public void Configure(EntityTypeBuilder<Oferta> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.ProdutoNome).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Quantidade).IsRequired();
            builder.Property(m => m.Preco).HasPrecision(10, 2); 
            builder.Property(m => m.HoraDisponivel).IsRequired();
        }
    }
}
