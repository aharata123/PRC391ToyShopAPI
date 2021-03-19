using AutoMapper;
using PRC391ToyShopAPI.Entities;
using PRC391ToyShopAPI.Repositories;
using PRC391ToyShopAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRC391ToyShopAPI.Services
{
    public interface IAccountService
    {
        Task<Account> Login(LoginModel model);

        Task<List<AccountModel>> GetAccounts();

        Task<AccountModel> GetAccountByUsername(string username);

        Task<int> CreateNewAccount(AccountModel model);
        Task<bool> UpdateAccount(AccountModel model, string username);

        Task<bool> DeleteAccount(string username);
    }
    public class AccountServices : IAccountService
    {
        private readonly IAccountRepository _account;
        private readonly IMapper _mapper;

        public AccountServices(IAccountRepository account, IMapper mapper)
        {
            _account = account;
            _mapper = mapper;
        }

        public async Task<int> CreateNewAccount(AccountModel model)
        {
            Account account = _mapper.Map<Account>(model);

            account.Disabled = false;
            account.RoleId = 2;

            int status = await _account.CreateNewAccount(account);

            return status;
        }

        public async Task<bool> DeleteAccount(string username)
        {
            bool status = await _account.DeleteAccount(username);

            return status;
        }

        public async Task<AccountModel> GetAccountByUsername(string username)
        {
            var result = await _account.GetAccountByUsername(username);

            return result;
        }

        public async Task<List<AccountModel>> GetAccounts()
        {
            var result = await _account.GetAccounts();

            List<AccountModel> list = new List<AccountModel>();

            for (int i = 0; i < result.Count(); i++)
            {
                var model = _mapper.Map<AccountModel>(result[i]);

                list.Add(model);
            }


            return list;
        }

        public Task<Account> Login(LoginModel model)
        {
            return _account.Login(model);
        }

        public async Task<bool> UpdateAccount(AccountModel model, string username)
        {
            bool isUpdated = await _account.UpdateAccount(model, username);

            return isUpdated;
        }
    }
}
