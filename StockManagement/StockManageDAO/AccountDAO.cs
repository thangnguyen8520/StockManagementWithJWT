using StockManageBusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManageDAO
{
    public class AccountDAO
    {
        private readonly Stocks2024DBContext dBContext = null;
        private static AccountDAO instance = null;
        public AccountDAO()
        {
            dBContext = new Stocks2024DBContext();
        }

        public static AccountDAO Instance {
            get
            {
                if(instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public Account GetAccount (string email, string password)
        {
            try
            {
                return dBContext.Accounts.FirstOrDefault(a => a.Email.Equals(email)
                                                        && a.Password.Equals(password)
                                                        && a.Status.Equals("active"));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public Account GetAccountByEmail(string email)
        {
            try
            {
                return dBContext.Accounts.FirstOrDefault(a => a.Email.Equals(email)
                                                        && a.Status.Equals("active"));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public void UpdateAccount(Account account)
        {
            try
            {
                var existingAccount = dBContext.Accounts.FirstOrDefault(a => a.Email.Equals(account.Email));
                if (existingAccount != null)
                {
                    existingAccount.RefreshToken = account.RefreshToken;
                    dBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating account", ex);
            }
        }

        public class AccountDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
