using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUsingDapper.Connections;
using WebUsingDapper.Models;

namespace WebUsingDapper.Controllers
{
    public class LoginsController : Controller
    {
        private readonly DapperDbContext _dbContext;

        public LoginsController(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public  async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _dbContext.connection.QuerySingle<LoginModel>("Select * From UserRegisters Where UserName=@username", new
                {
                    @username = model.UserName
                });

                if (user != null) 
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName), 
                        new Claim(ClaimTypes.Email,user.Email),

                    };
                    var claimIden=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrinciple=new ClaimsPrincipal(claimIden);
                    await HttpContext.SignInAsync(claimPrinciple, new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    });
                    return Redirect("Registers/Index");
                }
            }
           
            return View(model);
        }
    }
}
