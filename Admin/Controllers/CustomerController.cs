using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AdminDbContext _adminDbcontext;

        public CustomerController(AdminDbContext adminDbcontext)
        {
            _adminDbcontext = adminDbcontext;
        }
        [HttpGet]
        public async Task<IActionResult> CreateCustomer()
        {
            var list = await _adminDbcontext.Category_Customers.ToListAsync();
            var catelist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var get = new SelectListItem();
                get.Value = item.id.ToString();
                get.Text = item.category_name;
                catelist.Add(get);
            }
            ViewBag.ListCategory = catelist;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customers customer, List<IFormFile> customer_logo)
        {
            foreach (var item in customer_logo)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        customer.customer_logo = stream.ToArray();
                    }
                }
            }
            await _adminDbcontext.Customers.AddAsync(customer);
            await _adminDbcontext.SaveChangesAsync();
            return RedirectToAction("CreateCustomer");
        }
        public async Task<IActionResult> ListCustomer()
        {
            var listCustomer = await _adminDbcontext.Customers.Include(i => i.category_customer).ToListAsync();
            return View(listCustomer);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(Guid id)
        {
            var list = await _adminDbcontext.Category_Customers.ToListAsync();
            var catlist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var get = new SelectListItem();
                get.Value=item.id.ToString();
                get.Text = item.category_name;
                catlist.Add(get);
            }
            ViewBag.ListCategory = catlist;
            var customer = await _adminDbcontext.Customers.FirstOrDefaultAsync(x=> x.id==id);
            return View(customer);
            //return RedirectToAction("UpdateCustomer");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customers model)
        {
            var customer = await _adminDbcontext.Customers.FirstOrDefaultAsync(x => x.id == model.id);
            if (model.logo != null)
            {
                if (model.logo.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.logo.CopyToAsync(stream);
                        model.customer_logo = stream.ToArray();
                    }
                }
            }
            else
            {
                model.customer_logo = customer?.customer_logo;
            }
            customer.customer_logo = model.customer_logo;
            customer.customer_name = model.customer_name;
            customer.customer_detail = model.customer_detail;
            customer.category_id = model.category_id;
            customer.status = model.status;

            _adminDbcontext.Update(customer);
            await _adminDbcontext.SaveChangesAsync();
            return RedirectToAction("ListCustomer");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(Customers model)
        {
            var customer = await _adminDbcontext.Customers.FindAsync(model.id);
            _adminDbcontext.Customers.Remove(customer);
            await _adminDbcontext.SaveChangesAsync();
            return RedirectToAction("ListCustomer");
        }
        //public async Task<IActionResult> DeleteID (Guid id)
        //{
        //    var customer = await _adminDbcontext.Customers.FirstOrDefaultAsync(x => x.id == id);
        //    _adminDbcontext.Remove(customer);
        //    await _adminDbcontext.SaveChangesAsync();
        //    return RedirectToAction("ListCustomer");
        //}
    }
}
