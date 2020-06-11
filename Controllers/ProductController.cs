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
    [ApiController]
    [Route("{controller}")]
    public class ProductController: Controller
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public IActionResult Post(ProductDTO productData)
        {
            var product=new Product();
            product.Name = productData.Name;
            product.Description = productData.Description;
            product.Price = productData.Price;
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}