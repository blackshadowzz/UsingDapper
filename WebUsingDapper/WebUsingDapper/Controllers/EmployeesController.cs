using Microsoft.AspNetCore.Mvc;

namespace WebUsingDapper.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
