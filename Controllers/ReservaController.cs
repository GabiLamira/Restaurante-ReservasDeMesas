using Microsoft.AspNetCore.Mvc;
using RestauranteReserva.Models;
using RestauranteReserva.Repositories;

namespace RestauranteReserva.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaRepository _reservaRepo;
        private readonly IMesaRepository _mesaRepo;

        public ReservaController(IReservaRepository reservaRepo, IMesaRepository mesaRepo)
        {
            _reservaRepo = reservaRepo;
            _mesaRepo = mesaRepo;
        }

        public async Task<IActionResult> Index()
            => View(await _reservaRepo.GetReservasComMesaAsync());

        public async Task<IActionResult> Create()
        {
            ViewBag.Mesas = await _mesaRepo.GetMesasDisponiveisAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                await _reservaRepo.AddAsync(reserva);
                await _mesaRepo.SetDisponibilidadeAsync(reserva.MesaId, false);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesas = await _mesaRepo.GetMesasDisponiveisAsync();
            return View(reserva);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _reservaRepo.GetByIdAsync(id);
            if (reserva == null) return NotFound();
            ViewBag.Mesas = await _mesaRepo.GetAllAsync();
            return View(reserva);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _reservaRepo.UpdateAsync(reserva);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Mesas = await _mesaRepo.GetAllAsync();
            return View(reserva);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reserva = await _reservaRepo.GetByIdAsync(id);
            if (reserva == null) return NotFound();
            return View(reserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _reservaRepo.GetByIdAsync(id);
            if (reserva != null)
            {
                await _mesaRepo.SetDisponibilidadeAsync(reserva.MesaId, true);
                await _reservaRepo.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}