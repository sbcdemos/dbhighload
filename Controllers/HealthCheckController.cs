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

                var client = new DnsClient.LookupClient();
                client.UseCache=false;
                var dnsresult = client.Query("readonly.dbwebinar.local", QueryType.A).AllRecords.First();
                var adr = ((DnsClient.Protocol.ARecord)dnsresult).Address.ToString();
                result+="IPAddress of 'readonly.dbwebinar.local' is:\n\r "+adr+"\n\r";

                dnsresult = client.Query("master.dbwebinar.local", QueryType.A).AllRecords.First();
                adr = ((DnsClient.Protocol.ARecord)dnsresult).Address.ToString();
                result+="IPAddress of 'master.dbwebinar.local' is:\n\r "+adr+"\n\r";

                return Ok(result);
            }
            catch{
                return Ok(result);
            }
        }
    }
}