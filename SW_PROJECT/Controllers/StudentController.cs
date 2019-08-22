using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SW_PROJECT.Models;

namespace SW_PROJECT.Controllers
{
    public class StudentController : Controller
    {
        SIS_DB_Entities db = new SIS_DB_Entities();

        // GET: Student

        public ActionResult MainPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LectureSelection()
        {

            return View();
        }

        [HttpPost]
        public ActionResult LectureSelection(int id)
        {
            Students std = new Students();
            std.UserID = ((Users)Session["kullanici_giris"]).ID;
            std.LecID = id;
            db.Students.Add(std);
            db.SaveChanges();

            return RedirectToAction("LectureSelection", "Student");
        }

        public ActionResult TakenLectureList()
        {
            var kul = ((Users)Session["kullanici_giris"]);
            var lec = db.Students.Where(w => w.UserID == kul.ID).ToList();

            return View(lec);
        }

        public ActionResult LectureInfoList()
        {
            var user_id = ((Users)Session["kullanici_giris"]).ID;
            var model = db.Students.Where(row => row.UserID == user_id).ToList();

            return View(model);
        }
    }
}