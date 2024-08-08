using Microsoft.AspNetCore.Mvc;

namespace SecureGate.API.Controllers
{
    public class AccessVerificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
