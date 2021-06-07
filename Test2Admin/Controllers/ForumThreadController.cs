using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test2Admin.Models;
using System.Data.Entity;
using Test2Admin.ViewModels;

namespace Test2Admin.Controllers
{
    public class ForumThreadController : Controller
    {
        AdminPageEntities db = new AdminPageEntities();
        // GET: Forum
       
        public ActionResult Index(string searching)
        {
            //int userID = Convert.ToInt32(Session["UserID"]);
            //if (userID !=0)
            //{
                var comments = db.Comments.Include(x => x.Replies).OrderByDescending(x => x.CreatedOn).ToList();
                return View(db.Comments.Where(x => x.Title.Contains(searching) || searching == null).ToList());
            //} 
            //else
           // {
                //return RedirectToAction("Login", "Login");
           // }
        }

        // GET: Forum/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Forum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Forum/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Forum/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Forum/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Forum/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Forum/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //ForumReply
        [HttpPost]
        public  ActionResult PostReply(ReplyModel obj)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            if (userID == 0)
            { return RedirectToAction("Login", "Login");
            }
            else
            {
                Reply r = new Reply();
                r.Text = obj.Reply;
                r.CommentID = obj.CommentID;
                r.UserID = userID;
                r.CreatedOn = DateTime.Now;
                db.Replies.Add(r);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //ForumComment
        [HttpPost]
        public ActionResult PostComment(CommentModel obj)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            if (userID == 0)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Comment c = new Comment();
                c.Title = obj.CommentTitle;
                c.Text = obj.CommentInput;
                c.CreatedOn = DateTime.Now;
                c.UserID = userID;
                db.Comments.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
