using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.UserManagement
{
    public class UserPermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
