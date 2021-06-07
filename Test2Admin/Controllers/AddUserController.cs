using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Test2Admin.Models;

namespace Test2Admin.Controllers
{
    public class AddUserController : Controller
    {
        // GET: AddUser
        public ActionResult Index()

        {
            using (AdminPageEntities db = new AdminPageEntities())
            {
                return View(db.Users.ToList());
            }
        }

        // GET: AddUser/Create
        public ActionResult Create()
        {
            using (AdminPageEntities db = new AdminPageEntities())
            {

                return View();
            }

        }


        // POST: AddUser/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // TODO: Add insert logic here
                using (AdminPageEntities db = new AdminPageEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                }

                Session["LastAdded"] = user.Name; //sesja użytkownika
                System.Web.HttpContext.Current.Application["LastAdded"] = user.Name; //sesja aplikacyjna

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AddUser/Edit/5
        public ActionResult Edit(int id)
        {
            using (AdminPageEntities db = new AdminPageEntities())
            {
                return View(db.Users.Where(x => x.UserID == id).FirstOrDefault());
            };
        }

        // POST: AddUser/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                // TODO: Add update logic here

                using (AdminPageEntities db = new AdminPageEntities())
                {
                    db.Entry(collection).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: AddUser/Delete/5
        public ActionResult Delete(int id)
         {
             using (AdminPageEntities db = new AdminPageEntities())
             {
                 return View(db.Users.Where(x => x.UserID == id).FirstOrDefault());
             };
         } 


        // POST: AddUser/Delete/5
        [HttpPost]


         public ActionResult Delete(int id, User collection)
         {
             try
             {
                 // TODO: Add delete logic here
                 using (AdminPageEntities db = new AdminPageEntities())
                 {
                     collection = db.Users.Where(x => x.UserID == id).FirstOrDefault();
                     db.Users.Remove(collection);
                     db.SaveChanges();

                 }
                 return RedirectToAction("Index");
             }
             catch
             {
                 return View();
             }
         }
        
    
    }
}
