using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly AppDbContext _context;

        public PagamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pagamento pagamento)
        {
            await _context.Pagamentos.AddAsync(pagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<Pagamento> ObterPorIdAsync(Guid id)
        {
            return await _context.Pagamentos.FindAsync(id);
        }

        public async Task AtualizarAsync(Pagamento pagamento)
        {
            _context.Pagamentos.Update(pagamento);
            await _context.SaveChangesAsync();
        }
    }
}
