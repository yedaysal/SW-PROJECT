using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SW_PROJECT.Models;

namespace SW_PROJECT.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department
        SIS_DB_Entities db = new SIS_DB_Entities();

        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(string DeptName)
        {
            Departments new_dept = new Departments();
            new_dept.DeptName = DeptName;
            db.Departments.Add(new_dept);
            db.SaveChanges();

            return RedirectToAction("AddDepartment", "Department");
        }

        public ActionResult DepartmentList()
        {
            return View(db.Departments.ToList());
        }

        public ActionResult DeleteDepartment(int id)
        {
            db.Departments.Remove(db.Departments.Where(row => row.ID == id).FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("DepartmentList", "Department");
        }

        [HttpGet]
        public ActionResult EditDepartment(int ID)
        {
            var model = db.Departments.Find(ID);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditDepartment(int ID, string DeptName)
        {
            var model = db.Departments.Find(ID);
            model.DeptName = DeptName;
            db.SaveChanges();

            return RedirectToAction("DepartmentList", "Department");
        }
    }
}