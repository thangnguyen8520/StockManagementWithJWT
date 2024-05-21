using StockManageBusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageRepository
{
    public interface IAccountRepository
    {
      public Account GetAccount(string email, string password);
      public Account GetAccountByEmail(string email);
        public void UpdateAccount(Account account);

    }
}
