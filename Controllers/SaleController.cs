using System;
using System.Threading;
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
    [ApiController]
    [Route("{controller}")]
    public class SaleController: Controller
    {
        private readonly DataContextFactory _contextFactory;

        public SaleController(DataContextFactory contextFactory)
        {
            this._contextFactory=contextFactory;
        }

        [HttpPost]
        public IActionResult CreateSale(SaleDTO saleData)
        {
            var wr_ctx = _contextFactory.CreateDbContext(null);
            var ro_ctx = _contextFactory.CreateDbContext(new string[] {"RO"});
            using (ro_ctx){
                using (wr_ctx)
                {

                    var lastminute = System.DateTime.Now.AddSeconds(-60);
                    //getting Product Info
                    var product = ro_ctx.Products.Find(saleData.ProductID);
                    var client = ro_ctx.Products.Find(saleData.ClientID);
                    var alreadySoldByClient = ro_ctx.Sales.Where(x=>x.ClientID == saleData.ClientID).Sum(x=>x.TotalAmount);
                    var alreadySoldByProduct = ro_ctx.Sales.Where(x=>x.ProductID == saleData.ProductID).Sum(x=>x.TotalAmount);
                    var alreadySoldByClientAndProduct = ro_ctx.Sales.Where(x=>x.ClientID == saleData.ClientID && x.ProductID==saleData.ProductID).Sum(x=>x.TotalAmount);
                    var alreadySoldByLastMinute=ro_ctx.Sales.Where(x=>x.TheDate>lastminute).Sum(x=>x.TotalAmount);
                    var r=new Random();
                    var rvalue = r.Next(1000);

                    var sale=new Sale();
                    sale.TheDate = DateTime.Now;
                    sale.ProductID = saleData.ProductID;
                    sale.ClientID = saleData.ClientID;
                    sale.Price = product.Price;
                    sale.TotalAmount = product.Price*saleData.Quantity;
                    sale.Quantity = saleData.Quantity;
                    wr_ctx.Sales.Add(sale);
                    wr_ctx.SaveChanges();
                    var saleResult = new SaleResultDTO{
                        SaleID = sale.ID,
                        Price = sale.Price,
                        ProductID = sale.ProductID,
                        ProductName = product.Name,
                        Quantity = sale.Quantity,
                        TotalAmount = sale.TotalAmount,
                        ClientTotalAmount = alreadySoldByClient,
                        ProductTotalAmount = alreadySoldByProduct,
                        ProductByClientTotalAmount = alreadySoldByClientAndProduct,
                        LastMinuteTotalAmount = alreadySoldByLastMinute
                    };
                    return Ok(saleResult);
                }
            }
        }
    }
}