using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;

namespace Models
{
   
    public class ReadOnlyDataContext: DataContext
    {
        private string IPAddress {get; }
        public ReadOnlyDataContext(IConfiguration configuration) :base(configuration){

        }
        public ReadOnlyDataContext(IConfiguration configuration, string ipaddress) :base(configuration){
            this.IPAddress = ipaddress;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connstring = Configuration.GetConnectionString("SalesDatabaseRO");
            connstring = connstring.Replace("readonly.dbwebinar.local", IPAddress);
            optionsBuilder.UseMySql(connstring, mySqlOptions => mySqlOptions
                    // replace with your Server Version and Type
                    .ServerVersion(new Version(5, 7, 0), ServerType.MySql)
                    .CommandTimeout(10000));
        }
    }
}