using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using mysqlproject.Models;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;

namespace mysqlproject.Controllers
{
    public class HomeController : Controller
    {
     
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IActionResult Index()
        {
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("SalesDatabase"))){
                var cnt = conn.Query<int>("select COUNT(*) from sales").First();
                ViewBag.salescount = cnt;
                //return Ok($"Record count: {cnt}");
                return View();

            }
            /*
            using (var _context=_contextFactory.CreateDbContext(null)){
                var sale=new Sale{
                    ClientID=1,
                    ProductID=1,
                    Price=20,
                    Quantity=2,
                    TotalAmount=40
                };
                //_context.Sales.Add(sale);
                _context.SaveChanges();
                var salesCount=_context.Sales.Count();
                ViewBag.salescount = salesCount;
                return View();
            }
            */
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
