using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metra_Development.Controllers
{
    public class BlogController : Controller
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities 
        {
            if (db == null)
            {
                db = new Metraentities();
            }
        }
        // GET: Blog
        public ActionResult Index()
        {
            CheckConnection();
            var blogs = db.Blogs.Where(x => x.IsPublished == true).ToList();

            return View(blogs);
        }
    }
}