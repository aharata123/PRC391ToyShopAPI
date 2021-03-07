using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.ViewModel
{
    public class ToyPurchaseModel
    {
        public int ToyId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
