using Admin.Data;
using Admin.Models;
using Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AdminDbContext _adminDBContext;

        public HomeController(ILogger<HomeController> logger, AdminDbContext adminDbContext)
        {
            _logger = logger;
            _adminDBContext = adminDbContext;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel home = new HomeViewModel();
            home.blog_event = await _adminDBContext.Events.OrderByDescending(x=>x.event_date).ToListAsync();
            home.solutions = await _adminDBContext.Solutions.Include(x => x.solutions_type).ToListAsync();
            home.about_us = await _adminDBContext.AboutUs.ToListAsync();
            home.partner = await _adminDBContext.Partners.Where(x=>x.status == true).ToListAsync();
            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}