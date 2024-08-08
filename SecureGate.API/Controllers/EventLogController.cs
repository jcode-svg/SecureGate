using Microsoft.AspNetCore.Mvc;

namespace SecureGate.API.Controllers
{
    public class EventLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
