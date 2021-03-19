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

    public interface IAccountRepository
    {
        Task<List<Account>> GetAccounts();
        Task<AccountModel> GetAccountByUsername(string username);
        Task<int> CreateNewAccount(Account account);
        Task<bool> UpdateAccount(AccountModel model, string username);
        Task<bool> DeleteAccount(string username);
        Task<Account> Login(LoginModel model);

    }
    public class AccountRepository : IAccountRepository
    {
        private readonly PRC391_ToyShopContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(PRC391_ToyShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<int> CreateNewAccount(Account model)
        {
            int status = SystemStatusCode.FAIL;

            Account account = await _context.Accounts.FindAsync(model.Username);
            if(account != null)
            {
                return status;
            }

            try
            {
                _context.Accounts.Add(model);
                var response = await _context.SaveChangesAsync();
                if (response == SystemStatusCode.SUCCESS)
                {
                    status = SystemStatusCode.SUCCESS;
                }
            }
            catch (DbUpdateException e)
            {
                status = SystemStatusCode.ERROR;
            }

            return status;
        }

        public async Task<bool> DeleteAccount(string username)
        {
            bool isDeleted = false;

            Account account = await _context.Accounts.FindAsync(username);

            if (account != null)
            {
                account.Disabled = true;
                var response = await _context.SaveChangesAsync();
                if (response == SystemStatusCode.SUCCESS)
                {
                    isDeleted = true;
                }

            }
            return isDeleted;
        }

        public async Task<AccountModel> GetAccountByUsername(string username)
        {
            AccountModel model = null;
            var result = await _context.Accounts.FindAsync(username);
            if(result != null)
            {
                model = _mapper.Map<AccountModel>(result);
            }
            return model;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var result = await _context.Accounts.Where(account => account.Disabled == false)
        .ToListAsync();

            return result;
        }

        public async Task<Account> Login(LoginModel model)
        {
            var account = _context.Accounts.Where(account => account.Username.Equals(model.Username) && account.Password.Equals(model.Password) && account.Disabled == false)
                .Include(t => t.Role).FirstOrDefault();
            return account;
        }

        public async Task<bool> UpdateAccount(AccountModel model, string username)
        {
            bool isUpdated = false;

            Account account = await _context.Accounts.FindAsync(username);


            if (account != null)
            {
                _context.Entry(account).State = EntityState.Detached;
                account = _mapper.Map<Account>(model);

                _context.Entry(account).State = EntityState.Modified;


                _context.Accounts.Update(account);

                var response = await _context.SaveChangesAsync();
                if (response == SystemStatusCode.SUCCESS)
                {
                    isUpdated = true;
                }

            }
            return isUpdated;
        }
    }
}
