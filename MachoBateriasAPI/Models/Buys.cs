using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace MachoBateriasAPI.Models
{
    public class Buys
    {
        public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        public int amount { get; set; }
        public double total { get; set; }
        public int supplierId { get; set; }
        public int employeeId { get; set; }
        public int productId { get; set; }


    }
}
