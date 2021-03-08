using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.ViewModel
{
    public class PurchaseHistoryModel
    {

        public int OrderId { get; set; }
        public string Username { get; set; }
        public DateTime DateOrder { get; set; }
        public List<ToyModel> ListToyModel { get; set; }

        public PurchaseHistoryModel(int OrderId, string Username, DateTime DateOrder, List<ToyModel> list)
        {
            this.OrderId = OrderId;
            this.Username = Username;
            this.DateOrder = DateOrder;
            this.ListToyModel = list;
        }
    }
}
