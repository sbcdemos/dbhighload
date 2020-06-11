using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models{
    public class Sale {
        public int ID {get; set;}

        public DateTime? TheDate {get; set;}
        public int ProductID {get; set;}
        public Product Product {get; set;}
        public int ClientID {get; set;}
        public Client Client {get; set;}
        public int Quantity{get; set;}
        public decimal Price {get; set;}
        public decimal TotalAmount {get; set; }
        public decimal LastClientBalance {get; set;}
    }
}