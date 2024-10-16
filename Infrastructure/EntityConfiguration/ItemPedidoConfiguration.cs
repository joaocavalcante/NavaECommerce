using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(m => m.ProdutoId).IsRequired();
            builder.Property(m => m.Quantidade).IsRequired();
            builder.Property(m => m.PrecoUnitario).HasPrecision(10, 2); 
        }
    }
}
