using Microsoft.AspNetCore.Mvc;
using RestauranteReserva.Models;
using RestauranteReserva.Repositories;

namespace RestauranteReserva.Controllers
{
    public class MesaController : Controller
    {
        private readonly IMesaRepository _mesaRepo;

        public MesaController(IMesaRepository mesaRepo)
        {
            _mesaRepo = mesaRepo;
        }

        public async Task<IActionResult> Index()
            => View(await _mesaRepo.GetAllAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                await _mesaRepo.AddAsync(mesa);
                return RedirectToAction(nameof(Index));
            }
            return View(mesa);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var mesa = await _mesaRepo.GetByIdAsync(id);
            if (mesa == null) return NotFound();
            return View(mesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Mesa mesa)
        {
            if (id != mesa.Id) return NotFound();
            if (ModelState.IsValid)
            {
                await _mesaRepo.UpdateAsync(mesa);
                return RedirectToAction(nameof(Index));
            }
            return View(mesa);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var mesa = await _mesaRepo.GetByIdAsync(id);
            if (mesa == null) return NotFound();
            return View(mesa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mesaRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}