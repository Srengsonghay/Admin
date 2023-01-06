using Admin.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public JobApplicationController(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> CreateApplication(Guid id)
        {
            var list = await _adminDbContext.Careers.ToListAsync();
            var careerList = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.career_name;
                itm.Text = item.department;
                careerList.Add(itm);
            }
            ViewBag.carList = careerList;

            var application = await _adminDbContext.Careers.FirstOrDefaultAsync(x => x.id == id);

            return View(application);
            return RedirectToAction("CreateApplication");
        }
    }
}
