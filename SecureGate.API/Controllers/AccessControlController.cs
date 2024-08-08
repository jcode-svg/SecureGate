using Microsoft.AspNetCore.Mvc;

namespace SecureGate.API.Controllers
{
    public class AccessControlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
