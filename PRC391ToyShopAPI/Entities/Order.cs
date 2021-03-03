using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PRC391ToyShopAPI.Entities
{
    public partial class Order
    {
        public Order()
        {
            ToyInOrders = new HashSet<ToyInOrder>();
        }

        public int OrderId { get; set; }
        public string Username { get; set; }
        public DateTime DateOrder { get; set; }

        public virtual Account UsernameNavigation { get; set; }
        public virtual ICollection<ToyInOrder> ToyInOrders { get; set; }
    }
}
