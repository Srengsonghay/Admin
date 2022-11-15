using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class CategoryPartnerController : Controller
    {
        private readonly AdminDbContext _adminDbContext;
        public CategoryPartnerController(AdminDbContext adminDb)
        {
            _adminDbContext =  adminDb;
        }
        [HttpGet]
        public IActionResult CreateCategoryPartner()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategoryPartner(Category_partner addCategory)
        {
            //var Category_partner = new Category_partner
            //{
            //    id = Guid.NewGuid(),
            //    category_name = addCategory.category_name,
            //    status = addCategory.status,
            //};
            await _adminDbContext.Category_partners.AddAsync(addCategory);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("Listcategory_partner");
        }
      
        public async Task<IActionResult> Listcategory_partner()
        {
            var Category_partner = await _adminDbContext.Category_partners.ToListAsync();
            return View(Category_partner);
        }
        [HttpGet]
        public async Task<IActionResult> Updatecategory_partner(Guid id)
        {
            var Category_partner = await _adminDbContext.Category_partners.FirstOrDefaultAsync(i => i.id == id);
            if (Category_partner != null)
            {
                //var updateCategoryPartnerViewModel = new UpdateCategoryPartnerViewModel()
                //{
                //    id = Category_partner.id,
                //    category_name = Category_partner.category_name,
                //    status = Category_partner.status,
                //};
                return View(Category_partner);
            }
            return RedirectToAction("Updatecategory_partner");
        }
        [HttpPost]
        public async Task<IActionResult> Updatecategory_partner(Category_partner model)
        {
            //var Category_partner = await _adminDbContext.Category_partners.FindAsync(model.id);
            //if (Category_partner != null)
            //{
            //    Category_partner.category_name = model.category_name;
            //    Category_partner.status = model.status;      
            //}
            _adminDbContext.Update(model);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("Listcategory_partner");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Category_partner model)
        {
            var Category_partner = await _adminDbContext.Category_partners.FindAsync(model.id);
            if (Category_partner != null)
            {
                _adminDbContext.Category_partners.Remove(Category_partner);
                await _adminDbContext.SaveChangesAsync();
                
            }
            return RedirectToAction("Listcategory_partner");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCategoryPartner(Guid? id)
        {
            var Category_partner = await _adminDbContext.Category_partners.FindAsync(id);
            if (Category_partner != null)
            {
                _adminDbContext.Category_partners.Remove(Category_partner);
                await _adminDbContext.SaveChangesAsync();
                
            }
            return RedirectToAction("Listcategory_partner");
        }
    }
}
