using System;
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
    public class ProductController: Controller
    {
        private IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("SalesDatabase"))){
                var cnt = conn.Query<int>("select COUNT(*) from Products").First();
                return Ok($"Record count: {cnt}");
            }

        }

        [HttpPost]
        public IActionResult Post(ProductDTO productData)
        {
            long newID;
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("SalesDatabase"))){
                var product=new Product();
                product.Name = productData.Name;
                product.Description = productData.Description;
                product.Price = productData.Price;
                newID = conn.Insert<Product>(product);
            }
            /*
            using (var _context = _contextFactory.CreateDbContext(null)){
                var product=new Product();
                product.Name = productData.Name;
                product.Description = productData.Description;
                product.Price = productData.Price;
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok();
            }
            */
            return Ok(newID);
        }
    }
}