using Admin.Data;
using Admin.Models;
using Admin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcContrib.ActionResults;
using System.Linq;

namespace Admin.Controllers
{
    public class EventController : Controller
    {
        private readonly AdminDbContext _adminDbContext;

        public EventController(AdminDbContext adminDbContext)
        {
            _adminDbContext = adminDbContext;
        }

        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(Events events, List<IFormFile> event_image)
        {
            foreach (var item in event_image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        events.event_image = stream.ToArray();
                    }
                }
            }
            await _adminDbContext.Events.AddAsync(events);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("CreateEvent");
        }
        public async Task<IActionResult> ListEvent()
        {
            var events = _adminDbContext.Events.OrderByDescending(x => x.id).ToList();
            //var events = await _adminDbContext.Events.ToListAsync();
            return View(events);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEvent(Guid id)
        {
            var events = await _adminDbContext.Events.FirstOrDefaultAsync(i => i.id == id);
            return View(events);
            return RedirectToAction("ListEvent");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(Events model)
        {
            var events = await _adminDbContext.Events.FirstOrDefaultAsync(x => x.id == model.id);
            if (model.logo != null)
            {
                if (model.logo.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await model.logo.CopyToAsync(stream);
                        model.event_image = stream.ToArray();
                    }
                }

            }
            else
            {
                model.event_image = events?.event_image;
            }
            events.event_heading = model.event_heading;
            events.event_image = model.event_image;
            events.event_detail = model.event_detail;
            events.event_date = model.event_date;
            _adminDbContext.Events.Update(events);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListEvent");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Events model)
        {
            var events = await _adminDbContext.Events.FindAsync(model.id);
            _adminDbContext.Events.Remove(events);
            await _adminDbContext.SaveChangesAsync();
            return RedirectToAction("ListEvent");
        }
        [HttpGet]
        public async Task<IActionResult> ViewEvent()
        {
            //var events =   _adminDbContext.Events.OrderByDescending(x => x.id).ToList();
            EventViewModel events = new EventViewModel()
            {
                Post = await _adminDbContext.Events.OrderByDescending(i => i.id).ToListAsync(),
                LastPost = await _adminDbContext.Events.OrderByDescending(x => x.event_date).ToListAsync()
            };
            return View(events);

        }

        [HttpGet]
        [Route("ViewEventDetail/{id}/{title}")]
        public ActionResult ViewEventDetail(Guid id, string title)
        {
            EventDetailViewModel events = new EventDetailViewModel();

            events.EventDetail = _adminDbContext.Events.FirstOrDefault(x => x.id == id);

            //string realTitle = SlugGenerator.SlugGenerator.GenerateSlug(events.EventDetail.event_heading);
            //string urlTitle = (title ?? "").Trim().ToLower();

            //if (realTitle != urlTitle)
            //{
            //    string url = "/EventDetail/" + events.EventDetail.id + "/" + realTitle;
            //    return RedirectToActionPermanent(url);
            //}

            events.Last5Events = _adminDbContext.Events.Where(x => x.id != id).OrderByDescending(x => x.event_date).ToList();

            return View(events);
            
            return RedirectToAction("ViewEventDetail");

        }


        //[HttpGet]
        //public ActionResult ViewEventDetail(Guid id, string title)
        //{
        //    EventDetailViewModel events = new EventDetailViewModel();

        //    events.EventDetail =  _adminDbContext.Events.FirstOrDefault(x => x.id == id);
        //    string realTitle = SlugGenerator.SlugGenerator.GenerateSlug(events.EventDetail.event_heading);
        //    string urlTitle = (title ?? "").Trim().ToLower();

        //    if (realTitle != urlTitle)
        //    {
        //        string url = "/EventDetail/" + events.EventDetail.id + "/" + realTitle;
        //        return RedirectToActionPermanent(url);
        //    }

        //    events.Last5Events =  _adminDbContext.Events.Where(x => x.id != id).OrderByDescending(x => x.event_date).ToList();

        //    return View(events);

        //}

        //[HttpGet]
        //public async Task<IActionResult> ViewEventDetail(Guid id)
        //{
        //    EventDetailViewModel events = new EventDetailViewModel()
        //    {
        //        EventDetail = await _adminDbContext.Events.FirstOrDefaultAsync(x => x.id == id),
        //        //if (events.Last5Events.FirstOrDefault events.EventDetail.id)
        //        Last5Events = await _adminDbContext.Events.Where(x => x.id != id).OrderByDescending(x => x.event_date).ToListAsync()
        //    };
        //    return View(events);
        //    return RedirectToAction("ViewEventsDetail");
        //}

    }

    
}

