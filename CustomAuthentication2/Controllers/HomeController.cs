using CustomAuthentication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace CustomAuthentication2.Controllers
{
    public class HomeController : Controller
    {
        private AccountContext db = new AccountContext();
       
        [HttpGet]
        public ActionResult Index(string category, string searchKey, int? page)
        {
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            if(category!=null)
            {
                var posts = db.Posts.Where(a => a.Category.Equals(category)).OrderBy(a => a.PostingDate);
                ViewBag.category = category;
                return View(posts.ToPagedList(pageNumber, pageSize));
            }
            
            if(searchKey!=null)
            {

                var posts = db.Posts.Where(a => a.Title.ToLower().Contains(searchKey.ToLower())).OrderBy(a => a.PostingDate);
                ViewBag.searchKey = searchKey;
                return View(posts.ToPagedList(pageNumber, pageSize));
                
                
                //var posts3 = db.Posts.Where(a => a.Title.Equals(searchKey));
                //ViewBag.searchKeyforViewBag = searchKey;
                //return View(posts3.ToList());
            }

            var posts2 = db.Posts.OrderBy(a => a.PostingDate);
            
            //paging code was here
            return View(posts2.ToPagedList(pageNumber, pageSize));

            //return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}