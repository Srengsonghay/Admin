using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class AboutUSController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public AboutUSController(AdminDbContext adminDb)
        {

            _adminDbContext = adminDb;
        }

        public IActionResult CreateAboutUS()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAboutUS(AboutUs addAboutus)
        {
            await _adminDbContext.AboutUs.AddAsync(addAboutus);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateAboutUS");
        }

        public async Task<IActionResult> ListAboutUS()
        {
            var listAboutUS = await _adminDbContext.AboutUs.ToListAsync();
            return View(listAboutUS);
        }
       
        
        [HttpGet]
        public async Task<IActionResult> UpdateAboutUS(Guid id)
        {
            var aboutus = await _adminDbContext.AboutUs.FirstOrDefaultAsync(x => x.id == id);
            return View(aboutus);
            return RedirectToAction("UpdateAboutUS");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAboutUS(AboutUs model)
        {
            _adminDbContext.AboutUs.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListAboutUS");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(AboutUs model)
        {
            var aboutus = await _adminDbContext.AboutUs.FindAsync(model.id);
            if (aboutus != null)
            {
                _adminDbContext.AboutUs.Remove(aboutus);
                await _adminDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListAboutUS");
        }
        public async Task<IActionResult> ViewAboutus()
        {
            var about = await _adminDbContext.AboutUs.ToListAsync();
            return View(about);
        }
    }
}
