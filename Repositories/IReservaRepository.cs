using RestauranteReserva.Models;

namespace RestauranteReserva.Repositories
{
    public interface IReservaRepository : IRepository<Reserva>
    {
        Task<IEnumerable<Reserva>> GetReservasComMesaAsync();
    }
}