using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class SolutionDetailController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public SolutionDetailController(AdminDbContext AdminDb)
        {
            _adminDbContext = AdminDb;
        }
        [HttpGet]
        public async Task<IActionResult> CreateSolutionDetail()
        {
            var list = await _adminDbContext.Solutions.ToListAsync();
            var type = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.solution_name;
                //itm.Value = item.solution_type_id.ToString();
                type.Add(itm);
            }
            ViewBag.type = type;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSolutionDetail(SolutionDetail detail)
        {
            await _adminDbContext.SolutionDetails.AddAsync(detail);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateSolutionDetail");
        }
        
        public async Task<IActionResult> ListSolutionDetail()
        {
            var listSolutionDetail = await _adminDbContext.SolutionDetails.Include(x => x.solutions).ToListAsync();
            return View(listSolutionDetail);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSolutionDetail(Guid id)
        {
            var list = await _adminDbContext.Solutions.ToListAsync();
            var type = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.solution_name;
                //itm.Value = item.solution_type_id.ToString();
                type.Add(itm);
            }
            ViewBag.type = type;
            var detail = await _adminDbContext.SolutionDetails.FirstOrDefaultAsync(x => x.id == id);
            return View(detail);
            return RedirectToAction("UpdateSolutionDetail");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSolutionDetail(SolutionDetail model)
        {
            _adminDbContext.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolutionDetail");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(SolutionDetail model)
        {
            var detail = await _adminDbContext.SolutionDetails.FindAsync(model.id);
            if (detail != null)
            {
                _adminDbContext.SolutionDetails.Remove(detail);
                await _adminDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListSolutionDetail");
        }
    }
}



