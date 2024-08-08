using Microsoft.AspNetCore.Mvc;

namespace SecureGate.API.Controllers
{
    public class OfficeManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
