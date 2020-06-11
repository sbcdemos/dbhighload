using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models 
{
    public class ProductDTO{
        public int ID {get; set;}
        
        public string Name {get; set;}
        public string Description {get; set;}
        public decimal Price {get; set;}
    }
}