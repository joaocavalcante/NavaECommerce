using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task<Pedido> ObterPorIdAsync(Guid id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pedido>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            try
            {
                _context.Pedidos.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
