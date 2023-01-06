using Admin.Data;
using Admin.Models;
using Admin.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> ViewCareer()
        {
            var career = await _adminDbContext.Careers.Where(x=>x.is_active == true).ToListAsync();
            return View(career);
        }
        public async Task<IActionResult> ViewCareerDetail(Guid id)
        {
            var career = await _adminDbContext.Careers.FirstOrDefaultAsync(x => x.id == id);
            return View(career);
            return RedirectToPage("ViewCareerDetail");
        }
        [HttpGet]
        public async Task<IActionResult> CreateApplication(Guid id, JobApplication job)
        {
            job.career_id = id; 
            var list = await _adminDbContext.Careers.ToListAsync();
            var list1 = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.career_name;
                list1.Add(itm);
            }
            ViewBag.position = list1;
            var list2 = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.department;
                list2.Add(itm);
            }
            ViewBag.department = list2;
            return View(job);
            //return RedirectToAction("CreateApplication");
            return RedirectToAction("ViewCareerDetail");
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplication(JobApplication jobApplication)
        {
            
            if(jobApplication.formFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await jobApplication.formFile.CopyToAsync(stream);
                    jobApplication.file = stream.ToArray();
                }
            }
            jobApplication.send_date = DateTime.Now;
            await _adminDbContext.AddAsync(jobApplication);
            await _adminDbContext.SaveChangesAsync();
            TempData["success"] = "Thank you for your submition";
            return RedirectToAction("ViewCareer");
        }
        [HttpGet]
        public async Task<IActionResult> ListApplication()
        {
            
            var list = await _adminDbContext.Careers.ToListAsync();
            var list1 = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.career_name;
                list1.Add(itm);
            }
            ViewBag.position = list1;
            ListJobViewModel view = new ListJobViewModel();
            {   
              view.Job = await _adminDbContext.jobApplications.OrderByDescending(i => i.id).Include(x => x.Career).ToListAsync();
            }
            return View(view);
            //var application = await _adminDbContext.jobApplications.OrderByDescending(i=>i.id).Include(x=>x.Career).ToListAsync();
            //return View(application);
        }
        [HttpPost]
        public async Task<IActionResult> ListApplication(Nullable<Guid> select_career)
        {
            var list = await _adminDbContext.Careers.Where(x=>x.is_active == true).ToListAsync();
            var list1 = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.career_name;
                list1.Add(itm);
            }
            ViewBag.position = list1;
            ListJobViewModel view = new ListJobViewModel();
            if (select_career != null)
            {
                view.Job = await _adminDbContext.jobApplications.OrderByDescending(i => i.id).Where(i => i.career_id == select_career).Include(x => x.Career).ToListAsync();
            }
            else
            {
                view.Job = await _adminDbContext.jobApplications.OrderByDescending(i => i.id).Include(x => x.Career).ToListAsync();
            }
            return View(view);
        }
        [HttpGet]
        public async Task<IActionResult> ViewApplicationDetail(Guid id)
        {
            
            var job = await _adminDbContext.jobApplications.Include(x => x.Career).FirstOrDefaultAsync(i => i.id == id);
           
            return View(job);
        }


    }
}
