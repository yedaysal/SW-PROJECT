using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SW_PROJECT.Models;

namespace SW_PROJECT.Controllers
{
    public class LectureController : Controller
    {
        // GET: Lecture
        SIS_DB_Entities db = new SIS_DB_Entities();

        [HttpGet]
        public ActionResult AddLecture()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLecture(string LectureName, string LectureTerm, string LectureCredit, int DeptID)
        {
            Lectures new_lec = new Lectures();
            new_lec.LectureName = LectureName;
            new_lec.LectureTerm = LectureTerm;
            new_lec.LecAKTS = LectureCredit;
            new_lec.DeptID = DeptID;

            db.Lectures.Add(new_lec);
            db.SaveChanges();

            return RedirectToAction("AddLecture", "Lecture");
        }

        [HttpGet]
        public ActionResult DepartmentLectureList()
        {
            return View(db.Departments.ToList());
        }

        class dep_lec
        {
            public int depID { get; set; }
            public string depName { get; set; }
            public string depKredi { get; set; }
            public string depDonem { get; set; }
        }

        [HttpPost]
        public JsonResult DepartmentLectureList(int id)
        {
            List<dep_lec> dep_lecs = new List<dep_lec>();
            foreach (var lec in db.Lectures.Where(row => row.DeptID == id).ToList())
            {
                dep_lec new_lec = new dep_lec();
                new_lec.depID = lec.ID;
                new_lec.depName = lec.LectureName;
                new_lec.depKredi = lec.LecAKTS;
                new_lec.depDonem = lec.LectureTerm;
                dep_lecs.Add(new_lec);
            }
            return Json(dep_lecs, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLecture(int ID)
        {
            db.Lectures.Remove(db.Lectures.Where(row => row.ID == ID).FirstOrDefault());
            db.SaveChanges();

            return RedirectToAction("DepartmentLectureList", "Lecture");

        }


        [HttpGet]
        public ActionResult EditLecture(int id)
        {
            var model = db.Lectures.Find(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditLecture(int id, string LectureName, string LectureTerm, string LectureCredit, int DeptID)
        {
            var model = db.Lectures.Find(id);
            model.LectureName = LectureName;
            model.LectureTerm = LectureTerm;
            model.LecAKTS = LectureCredit;
            model.DeptID = DeptID;

            db.SaveChanges();

            return RedirectToAction("DepartmentLectureList", "Lecture");
        }
    }
}