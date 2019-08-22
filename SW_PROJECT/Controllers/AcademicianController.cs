using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SW_PROJECT.Models;

namespace SW_PROJECT.Controllers
{
    public class AcademicianController : Controller
    {
        SIS_DB_Entities db = new SIS_DB_Entities();
        // GET: Academician
        public ActionResult MainPage()
        {
            return View();
        }

        public ActionResult GivenLectures()
        {
            var acad_id = ((Users)Session["kullanici_giris"]).ID;
            var model = db.Lecturers.Where(r => r.UserID == acad_id).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult NotGirisi(int id)
        {
            var model = db.Students.Where(row => row.LecID == id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult NotGirisi(int id, int disC = 0, int midterm = 0, int final = 0)
        {
            var model = db.Students.Where(r => r.ID == id).FirstOrDefault();
            var ders_id = db.Students.Where(row => row.ID == id).FirstOrDefault().LecID;

            model.DisC = disC;
            model.Midterm = midterm;
            model.Final = final;
            db.SaveChanges();

            return RedirectToAction("NotGirisi", "Academician", new { id = ders_id });

        }
    }
}