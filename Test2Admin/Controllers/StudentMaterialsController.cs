using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test2Admin.Models;

namespace Test2Admin.Controllers
{
    public class StudentMaterialsController : Controller
    {
        // GET: StudentMaterials
        private AdminPageEntities db = new AdminPageEntities();
        public ActionResult Index()
        {
            var material = db.Materials.Include(m => m.User);
            return View(material.ToList());
        }

        // GET: TeacherMaterials/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Material material = db.Materials.Find(id);
            if (material == null)
            {
                return HttpNotFound();
            }
            return View(material);
        }
    }
}