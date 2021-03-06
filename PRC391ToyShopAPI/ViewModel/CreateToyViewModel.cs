﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.ViewModel
{
    public class CreateToyViewModel
    {
        public string ToyName { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
    }
}
