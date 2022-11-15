using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class SolutionController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public SolutionController(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> CreateSolution()
        {
            var list  = await _adminDbContext.SolutionsType.ToListAsync();
            var typelist = new List<SelectListItem>();
            foreach(var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.type_name;
                typelist.Add(itm);
            }
            ViewBag.type = typelist;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSolution(Solutions solutions)
        {
            await _adminDbContext.Solutions.AddAsync(solutions);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateSolution");
        }

        public async Task<IActionResult> ListSolution()
        {
            var listSolution = await _adminDbContext.Solutions.Include(x => x.solutions_type).ToListAsync();
            return View(listSolution);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateSolution(Guid id)
        {
            var list = await _adminDbContext.SolutionsType.ToListAsync();
            var typelist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.type_name;
                typelist.Add(itm);
            }
            ViewBag.type = typelist;
            var Solution = await _adminDbContext.Solutions.FirstOrDefaultAsync(i => i.id ==id);
            return View(Solution);
            return RedirectToAction("UpdateSolution");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSolution(Solutions model)
        {
            _adminDbContext.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolution");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Solutions model)
        {
            var Solution = await _adminDbContext.Solutions.FindAsync(model.id);
            if (Solution != null) { 
            _adminDbContext.Solutions.Remove(Solution);
            await _adminDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListSolution");
        }


    }
}
