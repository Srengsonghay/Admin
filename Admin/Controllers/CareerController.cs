using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class CareerController : Controller
    {
        private readonly AdminDbContext _adminDbContext;

        public CareerController(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        public IActionResult CreateCareer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCareer(Careers career)
        {
            await _adminDbContext.Careers.AddAsync(career);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateCareer");
        }
       
        public async Task<IActionResult> ListCareer()
        {
            var career = await _adminDbContext.Careers.ToListAsync();
            return View(career);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCareer(Guid id)
        {
            var career = await _adminDbContext.Careers.FirstOrDefaultAsync(i => i.id == id);
            return View(career);
            return RedirectToAction("ListCareer");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCareer(Careers model)
        {
            _adminDbContext.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListCareer");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Careers model)
        {
            var career = await _adminDbContext.Careers.FindAsync(model.id);
            _adminDbContext.Careers.Remove(career);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListCareer");
        }
    }
}
