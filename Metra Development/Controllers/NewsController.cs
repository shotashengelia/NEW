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
    public class NewsController : BaseController
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities 
        {
            if (db==null)
            {
                db = new Metraentities();
            }
        }
        // GET: News
        public ActionResult Index()
        {
            CheckConnection();
            var NewsList = db.DailyNews.Where(x=>x.IsPublished==true).ToList();

            return View(NewsList);
        }
        public ActionResult AdminShowNews()
        {
            CheckConnection();
            var News = db.DailyNews.ToList();
            return View(News);
        }
        public ActionResult AddNews()
        {
            CheckConnection();
            return View();
        }



  
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNews(NewsModel Obj)
        {
            CheckConnection();

            string name = MainHelper.Random32();
            if (string.IsNullOrEmpty(Obj.Title) || string.IsNullOrEmpty(Obj.Text) || Obj.File == null)
            {
                ViewBag.Error = "შეავსეთ ყველა ველი";
                return View();
            }
            else
            {
                DailyNew obj = new DailyNew()
                {
                    Title = Obj.Title,
                    Text = Obj.Text,
                    CreateDate = DateTime.Now,
                    IsPublished = false,
                    Photo = name

                };
                db.DailyNews.Add(obj);

                db.SaveChanges();

            
                using (var newImage = ScaleImage(Image.FromStream(Obj.File.InputStream, true, true), 400, 500))
                {
                    string path = Server.MapPath("/Content/Images/" + name + ".png");
                    newImage.Save(path, ImageFormat.Png);
                }


                return View();
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