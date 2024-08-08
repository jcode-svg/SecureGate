using Microsoft.AspNetCore.Mvc;

namespace SecureGate.API.Controllers
{
    public class EmployeeManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
