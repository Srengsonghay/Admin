using Admin.Data;
using Admin.Services;
using Admin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
		private readonly ILogger<AdminController> _logger;
		private readonly IDataAccessService _dataAccessService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<ApplicationRole> _roleManager;
		private readonly AdminDbContext _adminDbContext;

		public AdminController(
				UserManager<ApplicationUser> userManager,
				RoleManager<ApplicationRole> roleManager,
				IDataAccessService dataAccessService,
				AdminDbContext adminDbContext,
				ILogger<AdminController> logger)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			_roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
			_dataAccessService = dataAccessService ?? throw new ArgumentNullException(nameof(dataAccessService));
			_adminDbContext = adminDbContext;
		}

		[Authorize("Authorization")]
		public async Task<IActionResult> Roles()
		{
			var roleViewModel = new List<RoleViewModel>();
			var roles = await _roleManager.Roles.ToListAsync();
			foreach (var item in roles)
			{
				roleViewModel.Add(new RoleViewModel()
				{
					Id = item.Id,
					Name = item.Name,
				});
			}

			return View(roleViewModel);
		}

		[Authorize("Roles")]
		public IActionResult CreateRole()
		{
			return View(new RoleViewModel());
		}

		[HttpPost]
		[Authorize("Roles")]
		public async Task<IActionResult> CreateRole(RoleViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _roleManager.CreateAsync(new ApplicationRole() { Name = viewModel.Name });
				if (result.Succeeded)
				{
					return RedirectToAction(nameof(Roles));
				}
				else
				{
					ModelState.AddModelError("Name", string.Join(",", result.Errors));
				}
			}

			return View(viewModel);
		}

		[Authorize("Authorization")]
		public async Task<IActionResult> Users()
		{
			var userViewModel = new List<UserViewModel>();
			var users = await _userManager.Users.ToListAsync();
			foreach (var item in users)
			{
				userViewModel.Add(new UserViewModel()
				{
					Id = item.Id,
					Email = item.Email,
					UserName = item.UserName,
				});
			}

			return View(userViewModel);
		}

		[Authorize("Users")]
		public async Task<IActionResult> EditUser(string id)
		{
			var viewModel = new UserViewModel();
			if (!string.IsNullOrWhiteSpace(id))
			{
				var user = await _userManager.FindByIdAsync(id);
				var userRoles = await _userManager.GetRolesAsync(user);

				viewModel.Email = user?.Email;
				viewModel.UserName = user?.UserName;

				var allRoles = await _roleManager.Roles.ToListAsync();
				viewModel.Roles = allRoles.Select(x => new RoleViewModel()
				{
					Id = x.Id,
					Name = x.Name,
					Selected = userRoles.Contains(x.Name)
				}).ToArray();

			}

			return View(viewModel);
		}

		[HttpPost, Authorize("Users")]
		public async Task<IActionResult> EditUser(UserViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(viewModel.Id);
				var userRoles = await _userManager.GetRolesAsync(user);

				await _userManager.RemoveFromRolesAsync(user, userRoles);
				await _userManager.AddToRolesAsync(user, viewModel.Roles?.Where(x => x.Selected).Select(x => x.Name));

				return RedirectToAction(nameof(Users));
			}

			return View(viewModel);
		}

		[Authorize("Authorization")]
		public async Task<IActionResult> EditRolePermission(string id)
		{
			var permissions = new List<NavigationMenuViewModel>();
			if (!string.IsNullOrWhiteSpace(id))
			{
				permissions = await _dataAccessService.GetPermissionsByRoleIdAsync(id);
			}

			return View(permissions);
		}

		[HttpPost, Authorize("Authorization")]
		public async Task<IActionResult> EditRolePermission(string id, List<NavigationMenuViewModel> viewModel)
		{
			if (ModelState.IsValid)
			{
				var permissionIds = viewModel.Where(x => x.Permitted).Select(x => x.Id);

				await _dataAccessService.SetPermissionsByRoleIdAsync(id, permissionIds);
				return RedirectToAction(nameof(Roles));
			}

			return View(viewModel);
		}

		public async Task<IActionResult> ActionPermission()
        {
			
			
			var permissionViewModel = new List<NavigationMenuViewModel>();
			var permission = await _adminDbContext.NavigationMenu.ToListAsync();
			foreach (var item in permission)
			{
				permissionViewModel.Add(new NavigationMenuViewModel()
				{
					Id = item.Id,
					Name = item.Name,
					ParentMenuId = item.ParentMenuId,
					Area = item.Area,
					ControllerName = item.ControllerName,
					ActionName = item.ActionName,
					IsExternal = item.IsExternal,
					ExternalUrl = item.ExternalUrl,
					Permitted = item.Permitted,
					DisplayOrder = item.DisplayOrder,
					Visible = item.Visible,

				});
				
			}
			
			return View(permissionViewModel);
		}
        [HttpGet, Route("ActionPermission/{order}")]
		public async Task<IActionResult> ActionPermission(int order)
		{
			var permissionViewModel = new List<NavigationMenuViewModel>();
			var permission = await _adminDbContext.NavigationMenu.Where(x=>x.DisplayOrder == order).ToListAsync();
			foreach (var item in permission)
			{
				permissionViewModel.Add(new NavigationMenuViewModel()
				{
					Id = item.Id,
					Name = item.Name,
					ParentMenuId = item.ParentMenuId,
					Area = item.Area,
					ControllerName = item.ControllerName,
					ActionName = item.ActionName,
					IsExternal = item.IsExternal,
					ExternalUrl = item.ExternalUrl,
					Permitted = item.Permitted,
					DisplayOrder = item.DisplayOrder,
					Visible = item.Visible,

				});

			}
			return View(permissionViewModel);
		}
		public IActionResult CreateActionPermission()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateActionPermission(NavigationMenu model)
        {
            if (!ModelState.IsValid)
            {
				var action = await _adminDbContext.NavigationMenu.AddAsync(model);
				await _adminDbContext.SaveChangesAsync();
				
			}
            else
            {
				return NotFound();
            }
			return RedirectToAction("ActionPermission");
		}
		public async Task<IActionResult> EditActionPermission(Guid id)
        {
			var permission = await _adminDbContext.NavigationMenu.FirstOrDefaultAsync(x => x.Id == id);
			var action = new NavigationMenuViewModel()
            {
				Id = permission.Id,
				Name = permission.Name,
				ParentMenuId = permission.ParentMenuId,
				Area = permission.Area,
				ControllerName = permission.ControllerName,
				ActionName = permission.ActionName,
				IsExternal = permission.IsExternal,
				ExternalUrl = permission.ExternalUrl,
				Permitted = permission.Permitted,
				DisplayOrder = permission.DisplayOrder,
				Visible = permission.Visible,
			};
			return View(action);

		}
		[HttpPost]
		public async Task<IActionResult> EditActionPermission(NavigationMenu model)
		{
			if (ModelState.IsValid)
			{
				_adminDbContext.NavigationMenu.Update(model);
				await _adminDbContext.SaveChangesAsync();
				
			}
			else
			{
				return NotFound();
			}
			return RedirectToAction("ActionPermission");
		}
		[HttpPost]
		public async Task<IActionResult> DeleteActionPermission(Guid id)
		{
			
				var action = await _adminDbContext.NavigationMenu.FindAsync(id);
				_adminDbContext.NavigationMenu.Remove(action);
				await _adminDbContext.SaveChangesAsync();
			return RedirectToAction("ActionPermission");
		}


	}
}
