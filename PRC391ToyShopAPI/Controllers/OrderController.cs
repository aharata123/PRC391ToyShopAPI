using Microsoft.AspNetCore.Mvc;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Enums;
using PRC391ToyShopAPI.Repositories;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly PRC391_ToyShopContext _context;

        private readonly IOrderRepository _orderRepo;

        public OrderController(PRC391_ToyShopContext context, IOrderRepository orderRepo)
        {
            _context = context;
            _orderRepo = orderRepo;
        }
        [HttpPost("{username}")]
        public async Task<ActionResult> Login([FromBody] List<ToyPurchaseModel> cart, string username)
        {
            int status = await _orderRepo.Purchase(username, cart);
            if (status == SystemStatusCode.SUCCESS)
            {
                return StatusCode(200);
            }
            else
            {
                return StatusCode(409);
            }


        }
    }
}
