using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SW_PROJECT.Models;

namespace SW_PROJECT.Controllers
{
    public class LoginController : Controller
    {
        SIS_DB_Entities db = new SIS_DB_Entities();

        // GET: Login
       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Confirm(string usernumber, string password)
        {
            if (db.Users.Where(satir => satir.UserNum == usernumber).Any())
            {
                if (db.Users.Where(satir => satir.UserNum == usernumber).Where(satir => satir.Password == password).Any())
                {
                    Session["kullanici_giris"] = db.Users.Where(satir => satir.UserNum == usernumber).Where(satir => satir.Password == password).FirstOrDefault();
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}