using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.Nome).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Descricao).HasMaxLength(100).IsRequired();
            builder.Property(m => m.Preco).HasPrecision(10, 2);
            builder.Property(m => m.Estoque).IsRequired();
        }
    }
}
