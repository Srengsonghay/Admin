using Microsoft.AspNetCore.Identity;

namespace Admin.Data
{
    public class ApplicationRole :IdentityRole
    {
        public bool? IsLocked { get; set; }

    }
}
