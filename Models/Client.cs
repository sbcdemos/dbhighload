using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models {
    public class Client {
        public int ID {get; set;}
        [StringLength(255)]
        public string Name {get; set;}
        public string Description {get; set;}
    }
}