using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class SolutionsTypeController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public SolutionsTypeController(AdminDbContext adminDb)
        {
            _adminDbContext = adminDb;
        }
        [HttpGet]
        public IActionResult CreateSolutionType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSolutionType(SolutionsType SolutionType)
        {
            await _adminDbContext.AddAsync(SolutionType);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolutionType");
        }
        public async Task<IActionResult> ListSolutionType()
        {
            var SolutionType = await _adminDbContext.SolutionsType.ToListAsync();
            return View(SolutionType);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateSolutionType(Guid id)
        {
            var SolutionType = await _adminDbContext.SolutionsType.FirstOrDefaultAsync(i => i.id == id);
            return View(SolutionType);
            return RedirectToAction("UpdateSolutionType");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSolutionType(SolutionsType model)
        {
            _adminDbContext.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolutionType");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SolutionsType model)
        {
            var SolutionType = await _adminDbContext.SolutionsType.FindAsync(model.id);
            _adminDbContext.SolutionsType.Remove(SolutionType);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolutionType");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteList(Guid id, SolutionsType model)
        {
            var SolutionType = await _adminDbContext.SolutionsType.FirstOrDefaultAsync(i => i.id == id);
            return View(SolutionType);

            var Type = await _adminDbContext.SolutionsType.FindAsync(model.id);
            _adminDbContext.SolutionsType.Remove(Type);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolutionType");

        }
    }
}
