using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using DnsClient;
using System.Linq;
using System.Collections.Generic;

namespace Models
{

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>  
    {  
        public IConfiguration Configuration { get; }
        public DataContextFactory (IConfiguration configuration )
        {
            this.Configuration = configuration;
        }
        public DataContext CreateDbContext(string[] args)
        {
            if ((args==null)||(args.Length==0) ||(args[0]=="RW"))
            {
                return new DataContext(this.Configuration);  
            }
            else
            {
                var client = new DnsClient.LookupClient();
                client.UseCache = true;
                var result = client.Query("readonly.dbwebinar.local", QueryType.A).AllRecords.First();
                var adr = ((DnsClient.Protocol.ARecord)result).Address.ToString();
                return new ReadOnlyDataContext(this.Configuration, adr);
            }
        }
    }  
    public class DataContext : DbContext
    {
        public IConfiguration Configuration { get; }
        /*
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }*/
        public DataContext (IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.GetConnectionString("SalesDatabase"), mySqlOptions => mySqlOptions
                    // replace with your Server Version and Type
                    .ServerVersion(new Version(5, 7, 0), ServerType.MySql)
                    .CommandTimeout(10000));
        }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasIndex(b => b.TheDate).IsUnique(false);
        } 
    }   
}