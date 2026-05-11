using Microsoft.EntityFrameworkCore;
using RestauranteReserva.Data;
using RestauranteReserva.Models;

namespace RestauranteReserva.Repositories.Implementations
{
    public class MesaRepository : IMesaRepository
    {
        private readonly RestauranteContext _context;

        public MesaRepository(RestauranteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mesa>> GetAllAsync()
            => await _context.Mesas.ToListAsync();

        public async Task<Mesa?> GetByIdAsync(int id)
            => await _context.Mesas.FindAsync(id);

        public async Task<IEnumerable<Mesa>> GetMesasDisponiveisAsync()
            => await _context.Mesas.Where(m => m.Disponivel).ToListAsync();

        public async Task SetDisponibilidadeAsync(int mesaId, bool disponivel)
        {
            var mesa = await _context.Mesas.FindAsync(mesaId);
            if (mesa != null)
            {
                mesa.Disponivel = disponivel;
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Mesa mesa)
        {
            _context.Mesas.Add(mesa);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mesa mesa)
        {
            _context.Mesas.Update(mesa);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa != null)
            {
                _context.Mesas.Remove(mesa);
                await _context.SaveChangesAsync();
            }
        }
    }
}