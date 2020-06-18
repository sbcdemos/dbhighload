using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mysqlproject.Models;
using Models;

namespace mysqlproject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContextFactory _contextFactory;


        public HomeController(ILogger<HomeController> logger, DataContextFactory contextFactory)
        {
            _logger = logger;
            this._contextFactory=contextFactory;
        }

        public IActionResult Index()
        {
            using (var _context=_contextFactory.CreateDbContext(null)){
                var sale=new Sale{
                    ClientID=1,
                    ProductID=1,
                    Price=20,
                    Quantity=2,
                    TotalAmount=40
                };
                _context.Sales.Add(sale);
                _context.SaveChanges();
                var salesCount=_context.Sales.Count();
                ViewBag.salescount = salesCount;
                return View();
            }
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
