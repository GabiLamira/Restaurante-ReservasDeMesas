using Microsoft.EntityFrameworkCore;
using RestauranteReserva.Data;
using RestauranteReserva.Models;

namespace RestauranteReserva.Repositories.Implementations
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly RestauranteContext _context;

        public ReservaRepository(RestauranteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
            => await _context.Reservas.ToListAsync();

        public async Task<IEnumerable<Reserva>> GetReservasComMesaAsync()
            => await _context.Reservas.Include(r => r.Mesa).ToListAsync();

        public async Task<Reserva?> GetByIdAsync(int id)
            => await _context.Reservas.Include(r => r.Mesa).FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }
    }
}