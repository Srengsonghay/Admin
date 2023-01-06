using Admin.Data;
using Microsoft.AspNetCore.Builder;

namespace Admin
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Event}/{action=ViewEvent}/{id?}/{slug?}");
            //});
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "Event",
                   pattern: "{controller=Event}/{action=ViewEventDetail}/{id?}");

            });
        }
    }
}
