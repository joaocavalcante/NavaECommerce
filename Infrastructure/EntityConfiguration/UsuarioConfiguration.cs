using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class  UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.Nome).HasMaxLength(50).IsRequired();
            builder.Property(m => m.Email).HasMaxLength(100).IsRequired();
            builder.Property(m => m.SenhaHash).HasMaxLength(20).IsRequired();
            builder.Property(m => m.Role).IsRequired();
        }
    }
}
