using RestauranteReserva.Models;

namespace RestauranteReserva.Repositories
{
    public interface IMesaRepository : IRepository<Mesa>
    {
        Task<IEnumerable<Mesa>> GetMesasDisponiveisAsync();
        Task SetDisponibilidadeAsync(int mesaId, bool disponivel);
    }
}