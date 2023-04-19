using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using WebUsingDapper.Connections;
using WebUsingDapper.Models;

namespace WebUsingDapper.Controllers
{
    public class RegistersController : Controller
    { 
        private readonly DapperDbContext _dapper;

        public RegistersController(DapperDbContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetAll());
          
        }
        public async Task<List<RegisterModel>> GetAll()
        {
            string sql = "SELECT * FROM UserRegisters";

            var gets = await _dapper.connection.QueryAsync<RegisterModel>(sql, null);
            return gets.ToList();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            RegisterModel model = new();
            var sql = "SELECT * FROM UserRegisters WHERE UserID=@UserID";
            model = await _dapper.connection.QuerySingleAsync<RegisterModel>(sql, new { UserID = id });
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel register)
        {
            string sql = @"INSERT INTO UserRegisters(UserID,FirstName,LastName,Email,UserName,Password) 
                            Values(@UserID,@FirstName,@LastName,@Email,@UserName,@Password)";

            register.UserID=Guid.NewGuid();
            if (ModelState.IsValid)
            {
                var row = await _dapper.connection.ExecuteAsync(sql, register);
                if (row > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(register);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            string sql = "SELECT * FROM UserRegisters WHERE UserID=@UserID";
            var get = await _dapper.connection.QueryFirstAsync<RegisterModel>(sql, new RegisterModel { UserID=id});
            return View(get);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid UserID,RegisterModel register)
        {
            string sql = @"UPDATE UserRegisters SET UserID=@UserID,FirstName=@FirstName,LastName=@LastName,Email=@Email,UserName=UserName,Password=@Password,IsActive=@IsActive where UserID=@UserID";

            register.UserID = UserID;
            if(ModelState.IsValid)
            {
                var get = await _dapper.connection.ExecuteAsync(sql, register);
                if (get > 0)
                {
                    return RedirectToAction("Index");
                }
            }
           
            return View(register);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            string sql = "DELETE FROM UserRegisters WHERE UserID=@UserID";
            if(ModelState.IsValid)
            {
                await _dapper.connection.ExecuteAsync(sql, new { UserID = id });
            }
            return RedirectToAction("Index");

        }

        public JsonResult CheckUsername(string Username)
        {
            var user=_dapper.connection.Query<RegisterModel>("Select * From UserRegisters Where UserName=@username",new
            {
                @username=Username
            }).FirstOrDefault();

            if(user!=null)
            {
                return Json(data: false);
            }
            return Json(data: true);

        }
    }
}
