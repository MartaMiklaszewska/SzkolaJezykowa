using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Windows.Forms;
using Test2Admin.Models;
using Test2Admin.Services;

namespace Test2Admin.Controllers
{
    public class LoginController : Controller
    {
        SecurityService securityService = new SecurityService();
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (AdminPageEntities db = new AdminPageEntities())
                {
                    var obj = db.Users.Where(a => a.Role.Equals(objUser.Role) && a.Name.Equals(objUser.Name) && a.Password.Equals(objUser.Password)).FirstOrDefault();

                    if (obj != null)
                    {
                        if (securityService.AuthenticateAdmin(objUser))
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.Name.ToString();
                            return RedirectToAction("../AddUser/Index");


                        }
                        else if (securityService.AuthenticateStudent(objUser))
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.Name.ToString();
                            return RedirectToAction("../Student/Index");
                        }


                        else if (securityService.AuthenticateTeacher(objUser))
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.Name.ToString();
                            return RedirectToAction("../Teacher/Index");
                        }


                     
                    }   
                    else
                        {
                       
                        MessageBox.Show("Proszę sprawdź czy podane dane są poprawne", "Niepoprawne dane logowania",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        
                }
            }

            return View(objUser);
        }

        public ActionResult Logout()
        {
            Session["UserInfo"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        }
    }

