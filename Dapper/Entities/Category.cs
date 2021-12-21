﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public string? Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public override string ToString()
        {
            return Name!;
        }
    }
}
