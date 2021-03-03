using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Enums;
using PRC391ToyShopAPI.Services;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToyController : ControllerBase
    {
        private readonly IToyService _toyService;

        public ToyController(IToyService toyService)
        {
            _toyService = toyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToyModel>>> GetAllToys()
        {
            var toys = await _toyService.GetAllToys();

            if(toys == null)
            {
                return StatusCode(404);
            } else 
                return StatusCode(200, toys);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateNewToy([FromForm] CreateToyViewModel model)
        {
            int id = await _toyService.CreateNewToy(model);

            if (id == SystemStatusCode.ERROR || id == SystemStatusCode.FAIL)
            {
                return StatusCode(500);
            }  else
            {
                var message = "toyId: " + id;
                return StatusCode(201, message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateToy([FromForm] CreateToyViewModel model, int id)
        {
            bool isUpdated = await _toyService.UpdateToy(model, id);

            if (!isUpdated)
            {
                return StatusCode(500);
            }
            else
            {
                return StatusCode(200);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteToy(int id)
        {
            bool isDeleted = await _toyService.DeleteToy(id);
            
            if(isDeleted)
            {
                return StatusCode(204);
            } else
            {
                return StatusCode(400);
            }
        }
        
    }
}
