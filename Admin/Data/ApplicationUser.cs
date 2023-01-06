﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Admin.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public partial class ApplicationUser : IdentityUser
{
    public string? EmpID { get; set; }
    public string? FullName { get; set; }
}

