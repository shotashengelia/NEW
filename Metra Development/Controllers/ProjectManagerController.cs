using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Metra_Development.Models;

namespace Metra_Development.Controllers
{
    public class ProjectManagerController : Controller
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities is connected
        {
            if (db == null)
            {
                db = new Metraentities();
            }
        }
        public ActionResult Index()
        {
            return View();
        }
        
    }
}