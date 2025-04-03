using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Models;

namespace BLL.Models
{
    public class M_Suppliers
    {

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Company { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^05\d{8}$")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string RepresentativeName { get; set; }

          public Supplier Convert()
        {
            Supplier s =new Supplier()
            {
                Company = this.Company,
                PhoneNumber = this.PhoneNumber,
                RepresentativeName = this.RepresentativeName
            };
            return s;

        }



    }
}
