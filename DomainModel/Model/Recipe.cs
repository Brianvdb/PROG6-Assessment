﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public virtual ICollection<Product> Products { get; set; }
    }
}
