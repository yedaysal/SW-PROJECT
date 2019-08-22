using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SW_PROJECT.Models;

namespace SW_PROJECT.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        SIS_DB_Entities db = new SIS_DB_Entities();

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string Username, string Password, string Email, string UserNum, string CitizenNum, string Phone, string Address, int GroupID, int DeptID, string UserDegree)
        {

            Users new_user = new Users();

            new_user.Username = Username;
            new_user.Password = Password;
            new_user.Email = Email;
            new_user.UserNum = UserNum;
            new_user.CitizenNum = CitizenNum;
            new_user.Phone = Phone;
            new_user.Address = Address;
            new_user.RegDate = DateTime.Now;
            new_user.GroupID = GroupID;
            new_user.DeptID = DeptID;
            new_user.UserDegree = UserDegree;

            db.Users.Add(new_user);
            db.SaveChanges();

            return RedirectToAction("AddUser", "User");
        }

        public ActionResult Userlist()
        {
            return View(db.Users.ToList());
        }

        public ActionResult DeleteUser(int ID)
        {


            if (db.Users.Where(row => row.ID == ID).Any())
            {
                db.Users.Remove(db.Users.Where(row => row.ID == ID).FirstOrDefault());
                db.SaveChanges();
            }

            return RedirectToAction("UserList", "User");
        }

        [HttpGet]
        public ActionResult UpdateUser(int id)
        {
            var model = db.Users.Where(r => r.ID == id).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateUser(int id, string Username, string Password, string Email, string UserNum, string CitizenNum, string Phone, string Address, int GroupID, int DeptID, string UserDegree)
        {
            var model = db.Users.Find(id);

            model.Username = Username;
            model.Password = Password;
            model.Email = Email;
            model.UserNum = UserNum;
            model.CitizenNum = CitizenNum;
            model.Phone = Phone;
            model.Address = Address;

            db.SaveChanges();

            return RedirectToAction("UserList", "User");
        }
        
        [HttpGet]
        public ActionResult AssignLecture()
        {
            return View(db.Departments.ToList());
        }

        class dep_Acad
        {
            public int acadID { get; set; }
            public string acadName { get; set; }
            public int acadDeptID { get; set; }
        }

        class dep_acad_lec
        {
            public int dep_acad_lecID { get; set; }
            public string dep_acad_lecName { get; set; }
            public int dep_acad_lecDeptID { get; set; }
            public int assignedLecID { get; set; }
        }

        [HttpPost]
        public JsonResult GetAcademician(int id)
        {
            List<dep_Acad> dep_acads = new List<dep_Acad>();
            foreach (var acad in db.Users.Where(row => row.DeptID == id && row.GroupID == 2).ToList())
            {
                dep_Acad new_acad = new dep_Acad();
                new_acad.acadID = acad.ID;
                new_acad.acadName = acad.Username;
                new_acad.acadDeptID = (int)acad.DeptID;
                dep_acads.Add(new_acad);
            }
            return Json(dep_acads, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetLecture(int id)
        {
            List<dep_acad_lec> dep_acad_lecs = new List<dep_acad_lec>();
            var dep_id = db.Users.Where(row => row.ID == id).FirstOrDefault().DeptID;
            foreach (var lecture in db.Lectures.Where(row => row.DeptID == dep_id).ToList())
            {
                if (db.Lecturers.Where(row => row.UserID == id && row.LecID == lecture.ID).Any()) continue;
                else
                {
                    dep_acad_lec new_lec = new dep_acad_lec();
                    new_lec.dep_acad_lecID = lecture.ID;
                    new_lec.dep_acad_lecName = lecture.LectureName;
                    new_lec.dep_acad_lecDeptID = (int)lecture.DeptID;


                    dep_acad_lecs.Add(new_lec);
                }

            }
            return Json(dep_acad_lecs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AssignLecture(int lecID, int acaID)
        {
            Lecturers new_lec = new Lecturers();
            new_lec.UserID = acaID;
            new_lec.LecID = lecID;
            db.Lecturers.Add(new_lec);
            db.SaveChanges();

            return RedirectToAction("AssignLecture", "User");
        }

        public ActionResult UserProfile()
        {
            var logged_id = ((Users)Session["Kullanici_giris"]).ID;
            var model = db.Users.Where(row => row.ID == logged_id).ToList();

            return View(model);
        }
    }
}