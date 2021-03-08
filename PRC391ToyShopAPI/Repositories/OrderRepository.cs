using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Enums;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<int> Purchase(string username, List<ToyPurchaseModel> cart);
        Task<List<PurchaseHistoryModel>> GetPurchaseHistory(string username);

    }
    public class OrderRepository : IOrderRepository
    {
        private readonly PRC391_ToyShopContext _context;
        private readonly IMapper _mapper;

        public OrderRepository(PRC391_ToyShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<PurchaseHistoryModel>> GetPurchaseHistory(string username)
        {
            List<PurchaseHistoryModel> history = new List<PurchaseHistoryModel>();
            List<Order> listOrders = await _context.Orders.Where(order => order.Username.Equals(username)).OrderByDescending(order => order.DateOrder).ToListAsync();

            for (int i = 0; i < listOrders.Count; i++)
            {
                int idOrder = listOrders[i].OrderId;
                List<ToyInOrder> toys = await _context.ToyInOrders.Where(toys => toys.OrderId == idOrder)
                    .Include(t => t.Toy)
                    .ToListAsync();

                List<ToyModel> list = new List<ToyModel>();
                for (int k = 0; k < toys.Count; k++)
                {
                    Toy toy = toys[k].Toy;
                    toy.Quantity = toys[k].Quantity;
                    toy.Price = toys[k].Price;
                    ToyModel toyModel = _mapper.Map<ToyModel>(toy);
                    list.Add(toyModel);
                }

                PurchaseHistoryModel model = new PurchaseHistoryModel(idOrder,listOrders[i].Username, listOrders[i].DateOrder, list);
                history.Add(model);
            }

            return history;

        }

        public async Task<int> Purchase(string username, List<ToyPurchaseModel> cart)
        {
            int status = SystemStatusCode.FAIL;
            int orderId;
           using var transaction = _context.Database.BeginTransaction();
            try
            {
                Order order = new Order();
                order.Username = username;
                order.DateOrder = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

                _context.Orders.Add(order);

                var response = await _context.SaveChangesAsync();
                if (response == SystemStatusCode.SUCCESS)
                {
                    orderId = order.OrderId;
                } else
                {
                    return status;
                }

                for(int i = 0; i < cart.Count; i++)
                {

                    ToyInOrder toy = _mapper.Map<ToyInOrder>(cart[i]);
                    toy.OrderId = orderId;
                    Toy warehouse = await _context.Toys.FindAsync(toy.ToyId);

                    // check if warehouse quantity is enough
                    if(warehouse.Quantity < toy.Quantity)
                    {
                        return status;
                    } else
                    {
                        // minus quantity in warehouse

                        _context.Entry(warehouse).State = EntityState.Detached;
                        warehouse.Quantity = warehouse.Quantity - toy.Quantity;

                        _context.Entry(warehouse).State = EntityState.Modified;
                    }

                    _context.ToyInOrders.Add(toy);
                        
                }
                _context.SaveChanges();
                transaction.Commit();
                status = SystemStatusCode.SUCCESS;

            } catch (Exception)
            {
                status = SystemStatusCode.ERROR;
                transaction.Rollback();
            }
            return status;
        }
    }
}
