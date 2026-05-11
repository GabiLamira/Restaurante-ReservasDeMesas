using Microsoft.EntityFrameworkCore;
using RestauranteReserva.Models;

namespace RestauranteReserva.Data
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options)
            : base(options) { }

        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}