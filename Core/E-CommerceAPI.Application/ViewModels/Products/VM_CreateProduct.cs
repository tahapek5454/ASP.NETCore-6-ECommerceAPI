﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.ViewModels.Products
{
    public class VM_CreateProduct
    {
        public string Name { get; set;}
        public int Stock { get; set;}
        public decimal Price { get; set;}
    }
}
