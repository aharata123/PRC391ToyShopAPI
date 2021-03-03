using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PRC391ToyShopAPI.Entities
{
    public partial class Category
    {
        public Category()
        {
            Toys = new HashSet<Toy>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Toy> Toys { get; set; }
    }
}
