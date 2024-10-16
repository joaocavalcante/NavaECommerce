using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class OfertaRepository : IOfertaRepository
    {
        private readonly AppDbContext _context;

        public OfertaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Oferta oferta)
        {
            await _context.Ofertas.AddAsync(oferta);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Oferta>> ListarOfertasDisponiveisAsync(DateTime currentTime)
        {
            return await _context.Ofertas
                .Where(o => o.HoraDisponivel <= currentTime && o.Quantidade > 0)
                .ToListAsync();
        }
    }
}
