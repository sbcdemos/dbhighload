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
    public class ClientController: Controller
    {
        //private readonly DataContext _context;
        private IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        [HttpGet]
        public IActionResult Get()
        {
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("SalesDatabase"))){
                var cnt = conn.Query<int>("select COUNT(*) from Clients").First();
                return Ok($"Success {cnt}");
            }
        }
/*
        public IActionResult Index()
        {
            return Ok("Success-index");
        }
        */
        [HttpPost]
        public IActionResult Post(ClientDTO clientData)
        {
            long newID;
            using (var conn = new MySqlConnection(_configuration.GetConnectionString("SalesDatabase"))){
                var client=new Client();
                client.Name = clientData.Name;
                client.Description = clientData.Description;
                newID = conn.Insert<Client>(client);
            }
          
            return Ok("Created:"+clientData.Name);
        }
    }
}