using Microsoft.Extensions.Configuration;
using StockManageBusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageService
{
    public interface IAccountService
    {
        public Account GetAccount(string email, string password);
        public bool isUser(string email, string password);
        public Account GetAccountByEmail(string email);
        public void UpdateAccount(Account account);

    }
}
