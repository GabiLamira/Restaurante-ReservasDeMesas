using Microsoft.EntityFrameworkCore;
using RestauranteReserva.Data;
using RestauranteReserva.Models;
using RestauranteReserva.Repositories;
using RestauranteReserva.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// troquei InMemory por SQL Server — com independência de banco
builder.Services.AddDbContext<RestauranteContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Registra interfaces → implementações (injeção de dependência)
builder.Services.AddScoped<IMesaRepository, MesaRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed das mesas
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RestauranteContext>();
    context.Database.Migrate(); // cria o banco automaticamente
    if (!context.Mesas.Any())
    {
        context.Mesas.AddRange(
            new Mesa { Numero = 1, Capacidade = 2, Disponivel = true },
            new Mesa { Numero = 2, Capacidade = 4, Disponivel = true },
            new Mesa { Numero = 3, Capacidade = 4, Disponivel = true },
            new Mesa { Numero = 4, Capacidade = 6, Disponivel = true },
            new Mesa { Numero = 5, Capacidade = 8, Disponivel = true }
        );
        context.SaveChanges();
    }
}

app.MapControllerRoute("default", "{controller=Reserva}/{action=Index}/{id?}");

app.Run();