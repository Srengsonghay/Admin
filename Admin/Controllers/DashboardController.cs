using Admin.Data;
using Admin.Models;
using Microsoft.AspNetCore.Mvc;


namespace Admin.Controllers
{
  
    
     public class DashboardController : Controller
     {
        
       
            public IActionResult ViewDashboard()
            {

                return View();
            }
            
            
            
    }
    
}
