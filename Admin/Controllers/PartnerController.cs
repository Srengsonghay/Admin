using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Runtime.Serialization.Formatters.Binary;

namespace Admin.Controllers
{
    public class PartnerController : Controller
    {


        private readonly AdminDbContext _adminDbContext;

        public PartnerController(AdminDbContext adminDb)
        {
            _adminDbContext = adminDb;
        }
        [HttpGet]
        public async Task<IActionResult> CreatePartner()
        {
            var list = await _adminDbContext.Category_partners.Where(x=>x.status == true).ToListAsync();
            var catlist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.category_name;
                catlist.Add(itm);
            }
            ViewBag.ListCategory = catlist;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePartner(Partners addPartner, List<IFormFile> partner_logo)
        {
            foreach (var item in partner_logo)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        addPartner.partner_logo = stream.ToArray();
                    }
                }
            }

            await _adminDbContext.Partners.AddAsync(addPartner);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreatePartner");
        }

        public async Task<IActionResult> ListPartner()
        {
            var listPartner = await _adminDbContext.Partners.Include(x => x.category_partner).ToListAsync();
            return View(listPartner);
        }

        //Get ID form EditPartner View
        [HttpGet]
        public async Task<IActionResult> UpdatePartner(Guid id)
        {

            var list = await _adminDbContext.Category_partners.Where(x => x.status == true).ToListAsync();
            var catlist = new List<SelectListItem>();
            foreach (var item in list)
            {
                var itm = new SelectListItem();
                itm.Value = item.id.ToString();
                itm.Text = item.category_name;
                catlist.Add(itm);
            }
            ViewBag.ListCategory = catlist;

            var Partner = await _adminDbContext.Partners.FirstOrDefaultAsync(x => x.id == id);
            return View(Partner);
            //return RedirectToAction("UpdatePartner");
        }

        //Post Update on UpdatePartner
        [HttpPost]
        public async Task<IActionResult> UpdatePartner(Partners model)
        {
            var partner = await _adminDbContext.Partners?.FirstOrDefaultAsync(x => x.id == model.id);
            if (model.logo != null)
            {
                    if (model.logo.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await model.logo.CopyToAsync(stream);
                            model.partner_logo = stream.ToArray();
                        }
                    }
            }
            else
            {
                model.partner_logo = partner?.partner_logo;
                //var img = await _adminDbContext.Partners.FindAsync(partner_logo);
                //model.partner_logo = ObjectToByteArray(img);
            }
            partner.partner_logo = model.partner_logo;
            partner.partner_name = model.partner_name;
            partner.category_id = model.category_id;
            partner.status = model.status;
            partner.partner_detail = model.partner_detail;

            _adminDbContext.Update(partner);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListPartner");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Partners model)
        {
            var Partner = await _adminDbContext.Partners.FindAsync(model.id);
            if (Partner != null)
            {
                _adminDbContext.Partners.Remove(Partner);
                await _adminDbContext.SaveChangesAsync();
                return RedirectToAction("ListPartner");
            }
            return RedirectToAction("ListPartner");
        }
        public async Task<IActionResult> ViewPartner()
        {
            var partner = await _adminDbContext.Partners.Where(x=>x.status == true).ToListAsync();
            return View(partner);
        }
    }
}
