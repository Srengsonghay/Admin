using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class CategoryCustomerController : Controller
    {
        private readonly AdminDbContext _adminDbcontext;
        public CategoryCustomerController(AdminDbContext adminDb)
        {
            _adminDbcontext = adminDb;
        }
        [HttpGet]
        public IActionResult CreateCategoryCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryCustomer(Category_customer category_Customer)
        {
            await _adminDbcontext.Category_Customers.AddAsync(category_Customer);
            await _adminDbcontext.SaveChangesAsync();
            return RedirectToAction("ListCategory_customer");
        }
        public async Task<IActionResult> Listcategory_customer()
        {
            var Category_customer = await _adminDbcontext.Category_Customers.ToListAsync();
            return View(Category_customer);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory_customer(Guid id)
        {
            var Category_customer = await _adminDbcontext.Category_Customers.FirstOrDefaultAsync(i => i.id == id);
            return View(Category_customer);
            return RedirectToAction("UpdateCategory_customer");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory_customer(Category_customer model)
        {
            _adminDbcontext.Update(model);
            await _adminDbcontext.SaveChangesAsync();
            return RedirectToAction("ListCategory_customer");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category_customer model)
        {
            var Category_customer = await _adminDbcontext.Category_Customers.FindAsync(model.id);
            _adminDbcontext.Category_Customers.Remove(Category_customer);
            await _adminDbcontext.SaveChangesAsync();
            return RedirectToAction("Listcategory_customer");
        }
    }
}
