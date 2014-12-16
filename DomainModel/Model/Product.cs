using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }
        
        public string ProductType { get; set; }

        public double Price { get; set; }

        public int BrandId { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("Department")]
        public virtual Department Department { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        public virtual List<Discount> Discounts { get; set; }
    }
}
