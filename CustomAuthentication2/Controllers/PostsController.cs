using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomAuthentication2.Models;
using CustomAuthentication2.Security;

namespace CustomAuthentication2.Controllers
{
    public class PostsController : Controller
    {
        private AccountContext db = new AccountContext();

        
        public ActionResult Index(string id,string userlevel)
        {
            if(id==null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            
            ViewBag.UserLevel = userlevel;
            try
            {
                var posts = db.Posts.Where(p => p.Account.Username.Equals(id));
                if (posts == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                Account account = db.Accounts.Find(id);

                ViewBag.Fullname = account.FullName;
                

                return View(posts.ToList());
            }
            catch
            {
                return View();
            }
        }

        

        public ActionResult Details(string id,string userlevel)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            ViewBag.CommentsList = db.Comments.Where(comm => comm.PostId.Equals(id));
            ViewBag.UserLevel = userlevel;
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            //ViewBag.Username = new SelectList(db.Accounts, "Username", "Email");
            if (SessionPersister.Username == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, HttpPostedFileBase file)
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);//this is random key

            post.ID = finalString;
            
            
            //code for image
            string filepathtosave;
            try
            {
                if (file != null)
                {
                    int byteCount = file.ContentLength;
                    if (byteCount > 400000)//400KB
                    {
                        ViewBag.Error = "Can't Upload images more than 100KB";
                        return View(post);
                    }
                }

                /*Geting the file name*/
                string filename = System.IO.Path.GetFileName(file.FileName);
                //removing whitespaces
                filename=filename.ToCharArray()
                 .Where(c => !Char.IsWhiteSpace(c))
                 .Select(c => c.ToString())
                 .Aggregate((a, b) => a + b);

                /*Saving the file in server folder*/
                file.SaveAs(Server.MapPath("~/Images/Post/" + filename));
                filepathtosave = "Images/Post/" + filename;
                /*Storing image path to show preview*/
               // ViewBag.ImageURL = filepathtosave;
                /*
                 * HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE
                 *
                 */

                //ViewBag.Message = "File Uploaded successfully.";
            }
            catch
            {
                ViewBag.Error = "Error in uploading image,please try again";
                return View(post);
            }
            
            //ending of code for image



            if (ModelState.IsValid)
            {
                Account account = db.Accounts.Find(post.Username);
                post.FullName = account.FullName;
                post.image = filepathtosave;
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index", routeValues: new { id = SessionPersister.Username ,userlevel="user"});
            }

           
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            //ViewBag.Username = new SelectList(db.Accounts, "Username", "Email", post.Username);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post,HttpPostedFileBase file)
        {

            //code for image
            string filepathtosave;
            try
            {
                if (file != null)
                {
                    int byteCount = file.ContentLength;
                    if (byteCount > 400000)//400KB
                    {
                        ViewBag.warning = "Can't Upload images more than 100KB";
                        return View(post);
                    }
                }

                /*Geting the file name*/
                string filename = System.IO.Path.GetFileName(file.FileName);
                //removing whitespaces
                filename = filename.ToCharArray()
                 .Where(c => !Char.IsWhiteSpace(c))
                 .Select(c => c.ToString())
                 .Aggregate((a, b) => a + b);

                /*Saving the file in server folder*/
                file.SaveAs(Server.MapPath("~/Images/Post/" + filename));
                filepathtosave = "Images/Post/" + filename;
                /*Storing image path to show preview*/
                // ViewBag.ImageURL = filepathtosave;
                /*
                 * HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE
                 *
                 */

                //ViewBag.Message = "File Uploaded successfully.";
            }
            catch
            {
                ViewBag.warning = "Error in uploading image,please try again";
                return View(post);
            }

            //ending of code for image

            
                if (ModelState.IsValid)
                {

                    post.image = filepathtosave;
                    db.Entry(post).State = EntityState.Modified;
                    db.SaveChanges();
                    //ViewBag.Bug = "RED";
                    if (SessionPersister.Username==null)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    return RedirectToAction("Index", routeValues: new { id = SessionPersister.Username ,userlevel="user"});
                }
                else
                {
                   // ViewBag.Bug = "RED";
                    //return RedirectToAction("Home", "Index");
                    ViewBag.warning = "Can't be saved";
                    return View(post);
                }
            
            
           
            
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection formCollection)
        {

            string id = formCollection["postID"];

            Post post = db.Posts.Find(id);
            db.Comments.RemoveRange(db.Comments.Where(x => x.PostId == post.ID));
            
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", routeValues: new { id = SessionPersister.Username, userlevel = "user" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
