using StockManageBusinessObjects.Models;
using StockManageDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageRepository
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccount(string email, string password) => AccountDAO.Instance.GetAccount(email, password);

        public Account GetAccountByEmail(string email) => AccountDAO.Instance.GetAccountByEmail(email);
        public void UpdateAccount(Account account) => AccountDAO.Instance.UpdateAccount(account);

    }
}
