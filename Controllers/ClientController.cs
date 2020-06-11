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
    public class ClientController: Controller
    {
        private readonly DataContext _context;

        public ClientController(DataContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Success");
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
            var client=new Client();
            client.Name = clientData.Name;
            client.Description = clientData.Description;
            _context.Clients.Add(client);
            _context.SaveChanges();
            return Ok("Created:"+clientData.Name);
        }
    }
}