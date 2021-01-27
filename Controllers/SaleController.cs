using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using mysqlproject.Models;
using Models;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Dapper.Contrib.Extensions;

namespace mysqlproject.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class SaleController: Controller
    {
        private IConfiguration _configuration;

        public SaleController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost]
        public IActionResult CreateSale(SaleDTO saleData)
        {
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("SalesDatabase")))
            {
                var lastminute = System.DateTime.Now.AddSeconds(-60);
                var product = conn.Get<Product>(saleData.ProductID);
                var client = conn.Get<Client>(saleData.ClientID);

                var alreadySoldByClient = conn.Query<decimal>("select sum(TotalAmount) from Sales where ClientID=@ClientID", new {ClientID=saleData.ClientID}).First();

                var sale=new Sale();
                sale.TheDate = DateTime.Now;
                sale.ProductID = saleData.ProductID;
                sale.ClientID = saleData.ClientID;
                sale.Price = product.Price;
                sale.TotalAmount = product.Price*saleData.Quantity;
                sale.Quantity = saleData.Quantity;
                var saleID = conn.Insert(sale);
                var saleResult = new SaleResultDTO{
                    SaleID = sale.ID,
                    Price = sale.Price,
                    ProductID = sale.ProductID,
                    ProductName = product.Name,
                    Quantity = sale.Quantity,
                    TotalAmount = sale.TotalAmount,
                    ClientTotalAmount = alreadySoldByClient,
                    ProductTotalAmount =0, // alreadySoldByProduct,
                    ProductByClientTotalAmount = 0,//alreadySoldByClientAndProduct,
                    LastMinuteTotalAmount = 0//alreadySoldByLastMinute
                };
                return Ok(saleResult);
            }
            /*
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
                    var alreadySoldByLastMinute = 0;//=ro_ctx.Sales.Where(x=>x.TheDate>lastminute).Sum(x=>x.TotalAmount);
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
            */
        }
    }
}