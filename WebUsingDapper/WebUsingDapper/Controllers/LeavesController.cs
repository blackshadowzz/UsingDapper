using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WebUsingDapper.Connections;
using WebUsingDapper.Models;

namespace WebUsingDapper.Controllers
{
    public class LeavesController : Controller
    {
        private readonly DapperDbContext _dapper;

        public LeavesController(DapperDbContext dapper)
        {
            _dapper = dapper;
        }
        public async Task<IActionResult> Index()
        {
            
            var gets =await _dapper.connection.QueryAsync<Leave>("spr_Leaves_Select",param:new
            {
                @Id=0
            } 
           ,commandType: CommandType.StoredProcedure);
            return View(gets);
        }
        public async Task<IActionResult> Details(int id)
        {

            var gets = await _dapper.connection.QueryFirstOrDefaultAsync<Leave>("spr_Leaves_Select", param: new
            {
                @Id = id
            }
           , commandType: CommandType.StoredProcedure);
            return View(gets);
        }
        public IActionResult Create()
        {
            string emp = "Select * from Employees";
            var emps = _dapper.connection.Query(emp, null);
            ViewBag.Employees = new SelectList(emps, "EmployeeId", "FirstName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Leave leave)
        {
            if (ModelState.IsValid)
            {
                var row = _dapper.connection.Execute("spr_Leaves_Insert", commandType: CommandType.StoredProcedure,
                    param: new
                    {
                        leave.EmployeeId,
                        leave.LeaveDate,
                        leave.Amount,
                        leave.ReasonType,
                        leave.Description
                    });
                if (row > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        public async Task<IActionResult> Edit(int id)
        {
            string emp = "Select * from Employees";
            var emps = _dapper.connection.Query(emp, null);
            ViewBag.Employees = new SelectList(emps, "EmployeeId", "FirstName");
            var get = await _dapper.connection.QueryFirstOrDefaultAsync<Leave>("spr_Leaves_Select", param: new
            {
                @Id = id
            }
          , commandType: CommandType.StoredProcedure);
            return View(get);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Leave leave)
        {
            if(ModelState.IsValid)
            {
                var get = await _dapper.connection.ExecuteAsync("spr_Leaves_Update", param: new
                {
                    @Id = id,
                    @EmployeeId= leave.EmployeeId,
                    @LeaveDate= leave.LeaveDate,
                    @Amount= leave.Amount,
                    @ReasonType= leave.ReasonType,
                    @Description= leave.Description
                }
                ,commandType: CommandType.StoredProcedure);
                if(get>0)
                    return RedirectToAction(nameof(Index));
            }
            
            return View(leave);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var getOne = await _dapper.connection.QueryFirstOrDefaultAsync<Leave>("spr_Leaves_Select", param: new
            {
                @Id = id
            }
            , commandType: CommandType.StoredProcedure);

                var get = await _dapper.connection.ExecuteAsync("spr_Leaves_Delete", param: new
                {
                    @Id = getOne.Id,

                }
               , commandType: CommandType.StoredProcedure);
             return RedirectToAction(nameof(Index));
        }
    }
}
