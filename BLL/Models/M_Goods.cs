using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using DAL.Models;

namespace BLL.Models
{
    public class M_Goods
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string ProductName { get; set; }

        [Required]
        [Range(0.5, int.MaxValue)]
        public float Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int minQuantity { get; set; }
        public Good Convert()
        {
            Good good = new Good()
            {
                ProductName = this.ProductName,
               
                Price = this.Price,

                MinQuantity = this.minQuantity
            };
            return good;
        }
    }
}
