using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BLL.Models
{
    public class M_Orders
    {

        [Required]
        public int IdSuppliers { get; set; }

        public string Status { get; set; }= "waiting";

        public Order Convert()
        {
            Order o = new Order()
            {
               
                IdSuppliers = this.IdSuppliers
            };
            return o;
        }
    }
}
