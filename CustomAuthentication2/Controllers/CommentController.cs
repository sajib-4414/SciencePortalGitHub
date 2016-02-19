using CustomAuthentication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthentication2.Controllers
{
    public class CommentController : Controller
    {

        private AccountContext db = new AccountContext();
        // GET: Comment
        [HttpPost]
        public ActionResult PostComment(FormCollection formCollection)
        {
            //generating random comment id
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            //getting data for making up a comment model object
            string commentId = finalString;
            string postId = formCollection["postid"];
            string userId = formCollection["username"];
            Account account = db.Accounts.Find(userId);
            string FullnamE = account.FullName;
            string commentdescription = formCollection["description"];
            DateTime commentTime = DateTime.Now;

            //making a model object
            Comment comment = new Comment { ID = commentId, PostId = postId, Fullname = FullnamE, CommentDescription = commentdescription,CommentTime=commentTime };
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Details","Posts", routeValues: new { id = postId ,userlevel="public"});
            //return View();
        }
    }
}