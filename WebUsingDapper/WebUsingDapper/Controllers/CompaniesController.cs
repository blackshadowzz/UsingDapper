using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebUsingDapper.Connections;
using WebUsingDapper.Models;

namespace WebUsingDapper.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly DapperDbContext _dapper;

        public CompaniesController(DapperDbContext dapper)
        {
            _dapper = dapper;
        }
        public async Task<IActionResult> Index()
        {
            var gets =await _dapper.connection.QueryAsync<Company>("spr_company_selects", commandType: CommandType.StoredProcedure,
                                            param: new
                                            {
                                                @Id=0
                                            });
            return View(gets);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {

            if (ModelState.IsValid)
            {
               var row=  await _dapper.connection.ExecuteAsync("spr_company_insert", commandType: CommandType.StoredProcedure,
                    param:new
                    {
                        company.CompanyName,
                        company.CompanyDescription,
                        company.CompanyPhone,
                        company.CompanyLocation
                    });
                if (row > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(company);   
        }
        public IActionResult Edit(int id)
        {
            var edit= _dapper.connection.QueryFirstOrDefault<Company>("spr_company_selects", commandType: CommandType.StoredProcedure,
                     param: new
                     {
                        @Id=id
                     });
            if(edit==null)return NotFound();

            return View(edit);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Company company)
        {

            if (ModelState.IsValid)
            {
                var row = await _dapper.connection.ExecuteAsync("spr_company_update", commandType: CommandType.StoredProcedure,
                     param: new
                     {
                         company.Id,
                         company.CompanyName,
                         company.CompanyDescription,
                         company.CompanyPhone,
                         company.CompanyLocation
                     });
                if (row > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(company);
            }

            return View(company);
        }
    }
}
