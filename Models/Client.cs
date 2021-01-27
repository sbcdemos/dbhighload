using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib;

namespace Models {
    [Table("Clients")]
    public class Client {
        [Key]
        public int ID {get; set;}
        [StringLength(255)]
        public string Name {get; set;}
        public string Description {get; set;}
    }
}