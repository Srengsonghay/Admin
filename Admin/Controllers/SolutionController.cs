using Admin.Data;
using Admin.Models;
using Admin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    [Authorize]
    public class SolutionController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public SolutionController(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        [HttpGet, Authorize("Authorization")]
        public async Task<IActionResult> CreateSolution()
        {
            var list  = await _adminDbContext.SolutionsType.ToListAsync();
            var typelist = new List<SelectListItem>();
            foreach(var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.short_cut;
                typelist.Add(itm);
            }
            ViewBag.type = typelist;
            return View();
        }
        [HttpPost, Authorize("Authorization")]
        public async Task<IActionResult> CreateSolution(Solutions solutions)
        {
            var list = await _adminDbContext.SolutionsType.ToListAsync();
            var typelist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.short_cut;
                typelist.Add(itm);
            }
            ViewBag.type = typelist;
            if(solutions.description.Length >= 260)
            {
                ViewBag.message = "No 260 length";
                return RedirectToAction("CreateSolution");
            }
            await _adminDbContext.Solutions.AddAsync(solutions);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateSolution");
        }

        public async Task<IActionResult> ListSolution()
        {
            var listSolution = await _adminDbContext.Solutions.Include(x => x.solutions_type).ToListAsync();
            return View(listSolution);
        }
        [HttpGet, Authorize("Authorization")]
        public async Task<IActionResult> UpdateSolution(Guid id)
        {
            var list = await _adminDbContext.SolutionsType.ToListAsync();
            var typelist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.short_cut;
                typelist.Add(itm);
            }
            ViewBag.type = typelist;
            var Solution = await _adminDbContext.Solutions.FirstOrDefaultAsync(i => i.id ==id);
            return View(Solution);
            return RedirectToAction("UpdateSolution");
        }
        [HttpPost, Authorize("Authorization")]
        public async Task<IActionResult> UpdateSolution(Solutions model)
        {
            _adminDbContext.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListSolution");
        }
        [HttpPost, Authorize("Authorization")]
        public async Task<IActionResult> Delete(Solutions model)
        {
            var Solution = await _adminDbContext.Solutions.FindAsync(model.id);
            if (Solution != null) { 
            _adminDbContext.Solutions.Remove(Solution);
            await _adminDbContext.SaveChangesAsync();
            }
            return RedirectToAction("ListSolution");
        }
        public async Task<IActionResult> ViewSolution(Nullable<Guid> type)
        {
            SolutionViewModel solution = new SolutionViewModel();
            if (type != null)
            {
                solution.solutions = await _adminDbContext.Solutions.Where(x => x.solution_type_id == type).ToListAsync();
            }
            else
            {
                solution.solutions = await _adminDbContext.Solutions.ToListAsync();
            }

            solution.SolutionType = await _adminDbContext.SolutionsType.ToListAsync();
            return View(solution);
        }
        public async Task<IActionResult> ViewSolutionDetail(Guid? id)
        {
            SolutionDetailViewModel detail = new SolutionDetailViewModel();
            detail.solutionDetail = await _adminDbContext.SolutionDetails.Include(x=>x.solutions).Include(x => x.solutions.solutions_type).Where(x=>x.solution_id == id).FirstOrDefaultAsync(x => x.solution_id == id);
            detail.relatedSolution = await _adminDbContext.Solutions.OrderByDescending(x=>x.id).Where(x=>x.id != id).ToListAsync();
            return View(detail);
        }
    }
}
