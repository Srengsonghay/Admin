using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class EventController : Controller
    {
        private readonly AdminDbContext _adminDbContext;

        public EventController(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(Events events)
        {
            await _adminDbContext.Events.AddAsync(events);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateEvent");
        }
        public async Task<IActionResult> ListEvent()
        {
            var events = await _adminDbContext.Events.ToListAsync();
            return View(events);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEvent(Guid id)
        {
            var events = await _adminDbContext.Events.FirstOrDefaultAsync(i => i.id == id);
            return View(events);
            return RedirectToAction("ListEvent");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(Events model)
        {
            _adminDbContext.Events.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListEvent");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Events model)
        {
            var events = await _adminDbContext.Events.FindAsync(model.id);
            _adminDbContext.Events.Remove(events);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListEvent");
        }
    }
}
