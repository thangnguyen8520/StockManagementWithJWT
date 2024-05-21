using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockManageBusinessObjects.Models;
using StockManageRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockManageService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;
        public AccountService(IAccountRepository iAccountRepository)
        {
            this.iAccountRepository = iAccountRepository;
        }

        public Account GetAccount(string email, string password)
        {
            return iAccountRepository.GetAccount(email, password);
        }

        public Account GetAccountByEmail(string email)
        {
            return iAccountRepository.GetAccountByEmail(email);
        }

        public bool isUser(string email, string password)
        {
            return iAccountRepository.GetAccount(email, password) != null;
        }

        public void UpdateAccount(Account account)
        {
            iAccountRepository.UpdateAccount(account);
        }
    }
}
