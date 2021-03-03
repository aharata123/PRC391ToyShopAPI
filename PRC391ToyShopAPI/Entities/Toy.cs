using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PRC391ToyShopAPI.Entities
{
    public partial class Toy
    {
        public Toy()
        {
            ToyInOrders = new HashSet<ToyInOrder>();
        }

        public int ToyId { get; set; }
        public string ToyName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ToyInOrder> ToyInOrders { get; set; }
    }
}
