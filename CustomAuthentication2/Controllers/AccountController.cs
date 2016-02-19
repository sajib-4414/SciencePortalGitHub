using CustomAuthentication2.Models;
using CustomAuthentication2.Security;
using CustomAuthentication2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthentication2.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();
        

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountViewModel avm)
        {

            AccountModel am = new AccountModel();
            if ( am.login(avm.Account.Username, avm.Account.Password) == null)
            {
                ViewBag.Error = "Can't Log in,please check Username and Password";
                return View();
            }
            SessionPersister.Username = avm.Account.Username;
            
            return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        public ActionResult EditAccount(string username)
        {
            if (username == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Account account = db.Accounts.Find(username);
            if (account == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            //ViewBag.Username = new SelectList(db.Accounts, "Username", "Email", post.Username);

            EditAccountViewModel eaccount = new EditAccountViewModel { FullName = account.FullName, Email = account.Email, Username = account.Username, Password = account.Password, Roles = account.Roles };//
            ViewBag.role = account.Roles;
            return View(eaccount);
        }

        [HttpPost]
        public ActionResult EditAccount(EditAccountViewModel eaccount)
        {
            
            
            if (ModelState.IsValid)
            {

                Account account2 = new Account { FullName = eaccount.FullName, Email = eaccount.Email, Username = eaccount.Username, Password = eaccount.Password, Roles = eaccount.Roles };//

                //update also the fullname in Posts
                db.Posts
                .Where(x => x.Username.Equals(eaccount.Username))
                .ToList()
                .ForEach(x => x.FullName = eaccount.FullName);
                
                db.Entry(account2).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Success = "Your Account info was updated successfully";
                return View(eaccount);
            }
            // ViewBag.Username = new SelectList(db.Accounts, "Username", "Email", post.Username);
            ViewBag.Error = "Sorry Couldn't edit your account ,please try again";
            return View(eaccount);
        }


        
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {

            AccountModel am = new AccountModel();
            Account recieved = new Account { FullName = rvm.FullName, Email = rvm.Email, Username = rvm.Username, Password = rvm.Password, Roles = rvm.Roles };//creating an account from viewmodel recieved data thats why its called recieved

            //for testing
            //ViewBag.Error = recieved.Email;

            if (am.find(recieved.Username)!=null)
            {
                ViewBag.Error = "Username already exists,please choose other one";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Accounts.Add(recieved);
                db.SaveChanges();
                 
            }
            SessionPersister.Username = recieved.Username;
            //SessionPersister.Fullname = recieved.FullName;
            //return View("Success");
            //ViewBag.Success = "User Successfully created";
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            SessionPersister.Username = null;
            //SessionPersister.Fullname = null;
            return RedirectToAction("Index","Home");
        }
    }
}