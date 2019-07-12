﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSite.Entities
{
     public class Product : BaseEntity
    {
        [Range(1,100000)]
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public int Stock { get; set; }

        public int CategoryID { get; set; }  
        public virtual Category Category { get; set; }
    }
}
