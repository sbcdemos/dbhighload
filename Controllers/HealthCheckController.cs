using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using mysqlproject.Models;
using Models;
using DnsClient;

namespace mysqlproject.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class HealthCheckController: Controller
    {
        private IConfiguration _configuration;
        public HealthCheckController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            string result="Config parameters:\n\r";
            try{
                
                result +=_configuration.GetConnectionString("SalesDatabase")+"\n\r";

                

                return Ok(result);
            }
            catch{
                return Ok(result);
            }
        }
    }
}