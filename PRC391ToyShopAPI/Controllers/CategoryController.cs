using Microsoft.AspNetCore.Mvc;
using PRC391ToyShopAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly PRC391_ToyShopContext _context;

        public CategoryController(PRC391_ToyShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategory()
        {
            var categories = _context.Categories.ToList();

            if (categories == null)
            {
                return StatusCode(404);
            }
            else
                return StatusCode(200, categories);
        }

    }
}
