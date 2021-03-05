using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Enums;
using PRC391ToyShopAPI.Services;
using PRC391ToyShopAPI.ViewModel;

namespace PRC391ToyShopAPI.Controllers
{
/*    [Authorize]*/
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly PRC391_ToyShopContext _context;

        private readonly IAccountService _accountService;

        public AccountController(PRC391_ToyShopContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<List<AccountModel>>> GetAccounts()
        {
            var accounts = await _accountService.GetAccounts();

            if (accounts == null)
            {
                return StatusCode(404);
            }
            else
                return StatusCode(200, accounts);
        }



        // PUT: api/Accounts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateAccount([FromBody] AccountModel model, string username)
        {
            bool isUpdated = await _accountService.UpdateAccount(model, username);

            if (!isUpdated)
            {
                return StatusCode(500);
            }
            else
            {
                return StatusCode(200);
            }
        }

        // POST: api/Account
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> CreateNewAccount([FromBody] AccountModel model)
        {


            int status = await _accountService.CreateNewAccount(model);

            if (status == SystemStatusCode.ERROR)
            {
                return StatusCode(500);
            } else if (status == SystemStatusCode.FAIL)
            {
                return StatusCode(409);
            }
            else
            {
                return StatusCode(201);
            }
        }




        // DELETE: api/Account/5
        [HttpDelete("{username}")]
        public async Task<ActionResult> DeleteAccount(string username)
        {
            bool isDeleted = await _accountService.DeleteAccount(username);

            if (isDeleted)
            {
                return StatusCode(204);
            }
            else
            {
                return StatusCode(400);
            }
        }

        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.Username == id);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login([FromBody] LoginModel model)
        {
            var account = await _accountService.Login(model);
            if(account != null)
            {
                return StatusCode(200, account);
            } else
            {
                return StatusCode(404);
            }

          
        }
    }
}
