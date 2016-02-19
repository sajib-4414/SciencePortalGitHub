using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuthentication2.Models
{
    public class AccountModel
    {
        private List<Account> listAccounts = new List<Account>();
        private AccountContext db = new AccountContext();
        public AccountModel()
        {
            listAccounts.Add(new Account { Username = "acc1", Password = "123", Roles = "superadmin", FullName = "Acer1", Email = "admin1@email.com" });
            listAccounts.Add(new Account { Username = "acc2", Password = "123", Roles = "superadmin", FullName = "Acer2", Email = "admin2@email.com" });
           //egula non admin gula database theke dekhbo
            //listAccounts.Add(new Account { Username = "acc3", Password = "123", Roles = "employee" });

        }
        public Account find(string username)
        {

            //ei bool expression ta banano ,kaj korbe kina janina
            bool b = listAccounts.Any(acc => acc.Username.Equals(username));//checking in static list first
            
            if (b == true)//found in static list
            {
                return listAccounts.Where(acc => acc.Username.Equals(username)).FirstOrDefault();
            }
            //returning from database
            return db.Accounts.Where(acc => acc.Username.Equals(username)).FirstOrDefault();  
        }
        public Account login(string username, string password)
        {

            //ei bool expression ta banano ,kaj korbe kina janina
            bool b = listAccounts.Any(acc => acc.Username.Equals(username) && acc.Password.Equals(password));//checking in static list first

            if (b == true)//matched in static list
            {
                return listAccounts.Where(acc => acc.Username.Equals(username) && acc.Password.Equals(password)).FirstOrDefault();
            }
            //returning from database
            return db.Accounts.Where(acc => acc.Username.Equals(username) && acc.Password.Equals(password)).FirstOrDefault();  
        }
    }
}