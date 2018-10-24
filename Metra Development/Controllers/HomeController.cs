using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metra_Development.Controllers
{
    public class HomeController : BaseController
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities 
        {
            if (db == null)
            {
                db = new Metraentities();
            }
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult showmap()
        {
            return View();
        }
        public ActionResult DetailNews(int id)
        {
            CheckConnection();
            var details = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();

            return View(details);
        }
        public ActionResult DetailBlogs(int id)
        {
            CheckConnection();
            var details = db.Blogs.Where(x => x.ID == id).FirstOrDefault(); ////////////add view for this!

            return View(details);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}