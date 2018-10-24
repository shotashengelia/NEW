using Metra_Development.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metra_Development.Controllers
{
    public class AdminPanelController : Controller
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities 
        {
            if (db == null)
            {
                db = new Metraentities();
            }
        }
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminShowBlogs()
        {
            CheckConnection();
            var blogs = db.Blogs.ToList();

            return View(blogs);
        }
        public ActionResult DeleteBlogs(int id)
        {
            CheckConnection();
            var blogs = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            db.Blogs.Remove(blogs);
            db.SaveChanges();
            return RedirectToAction("AdminShowBlogs", "AdminPanel");
        }
        public ActionResult DeleteNews(int id)
        {
            CheckConnection();
            var dailynew = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();
            db.DailyNews.Remove(dailynew);
            db.SaveChanges();
            return RedirectToAction("AdminShowNews","News");
        }
        public ActionResult PublishNews(int id)
        {
            CheckConnection();
            DailyNew dn = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();
            dn.IsPublished = true;
            db.SaveChanges();
            return RedirectToAction("AdminShowNews", "News");
        }
        public ActionResult PublishBlogs(int id)
        {
            CheckConnection();
            Blog bl = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            bl.IsPublished = true;
            db.SaveChanges();
            return RedirectToAction("AdminShowBlogs","AdminPanel");
            
        }
        public ActionResult UnPublishNews(int id)
        {
            CheckConnection();
            DailyNew dn = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();
            dn.IsPublished = false;
            db.SaveChanges();
            return RedirectToAction("AdminShowNews", "News");
        }
        public ActionResult UnPublishBlogs(int id)
        {
            CheckConnection();
            Blog bl = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            bl.IsPublished = false;
            db.SaveChanges();
            return RedirectToAction("AdminShowBlogs","AdminPanel");
        }
        public ActionResult ReviewNews(int id)
        {
            CheckConnection();
            DailyNew dn = db.DailyNews.Where(x => x.ID == id).FirstOrDefault() ;
            return View(dn);
        }
        public ActionResult ReviewBlogs(int id)
        {
            CheckConnection();
            Blog bl = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            return View(bl);
        }
        public ActionResult DetailNews(int id)
        {
            CheckConnection();
            var detail = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();
            return View(detail);
        }
        public ActionResult DetailBlogs(int id)
        {
            CheckConnection();
            var detail = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            return View(detail);
        }
        public ActionResult EditBlogs(int id)
        {
            CheckConnection();
            var edit = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
            return View(edit);
        }
        public ActionResult EditNews(int id)
        {
            CheckConnection();
            var Edit = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();
            return View(Edit);
        }
        [HttpPost]
        public ActionResult EditBlogs(int id, PostManageModel Obj)
        {
            CheckConnection();
            if (string.IsNullOrEmpty(Obj.Title) || string.IsNullOrEmpty(Obj.Text) || Obj.Photo == null)
            {
                ViewBag.Error = "შეავსეთ ყველა ველი";
                return View();
            }
            else
            {
                Blog bl = db.Blogs.Where(x => x.ID == id).FirstOrDefault();
                if (Obj.Photo != null)
                {
                    string name = MainHelper.Random32();
                    using (var newImage = ScaleImage(Image.FromStream(Obj.Photo.InputStream, true, true), 400, 500))
                    {
                        string path = Server.MapPath("/Content/Images/" + name + ".png");
                        newImage.Save(path, ImageFormat.Png);
                    }

                    bl.Photo = name;
                }
                bl.CreateDate = DateTime.Now;
                bl.Title = Obj.Title;
                bl.Text =  Obj.Text;
                db.SaveChanges();
                return RedirectToAction("AdminShowBlogs", "AdminPanel");
            }
        }
        [HttpPost]
        public ActionResult EditNews(int id, NewsModel Obj)
        {
            CheckConnection();
            if (string.IsNullOrEmpty(Obj.Title) || string.IsNullOrEmpty(Obj.Text) || Obj.File == null)
            {
                ViewBag.Error = "შეავსეთ ყველა ველი";
                return View();
            }
            else
            {
                DailyNew dn = db.DailyNews.Where(x => x.ID == id).FirstOrDefault();
                string name = MainHelper.Random32();
                if (Obj.File != null)
                {
                    
                    using (var newImage = ScaleImage(Image.FromStream(Obj.File.InputStream, true, true), 400, 500))
                    {
                        string path = Server.MapPath("/Content/Images/" + name + ".png");
                        newImage.Save(path, ImageFormat.Png);
                    }

                    dn.Photo = name;
                }
                dn.CreateDate = DateTime.Now;
                dn.Title = Obj.Title;
                dn.Text = Obj.Text;
                dn.Photo = name;
                db.SaveChanges();
                return RedirectToAction("AdminShowNews","News");
            }
        }
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
    }
}
