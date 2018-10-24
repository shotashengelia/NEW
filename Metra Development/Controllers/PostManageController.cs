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
    public class PostManageController : BaseController
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities is connected
        {
            if (db == null)
            {
                db = new Metraentities();
            }
        }
        public ActionResult AddNewPost()
        {
            CheckConnection();
            var getcategorylist = db.Categories.ToList();
            getcategorylist.Insert(0, new Category { ID = 0, Name = "აირჩიე ბლოგის კატეგორია" });
            ViewBag.getcategorylist = getcategorylist;
            ViewBag.selectCategorytype = 0;
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNewPost(PostManageModel PostData)
        {
            CheckConnection();

            var getcategorylist = db.Categories.ToList();
            ////var blog = db.Blogs.FirstOrDefault(x => x.ID == Id);
            ////var blogID = blog.CategoryID;
            ////var categoriesbyid = db.Categories.Where(x => x.ID == blogID).ToList();
            ////SelectList list = new SelectList(getcategorylist, "ID", "Name");
            ////ViewBag.categorylist = list;
            getcategorylist.Insert(0, new Category { ID = 0, Name = "აირჩიე ბლოგის კატეგორია" });
            ViewBag.getcategorylist = getcategorylist;
            ViewBag.selectCategorytype = 0;

            if (string.IsNullOrEmpty(PostData.Title) || string.IsNullOrEmpty(PostData.Text) || PostData.Photo == null )
            {
                ViewBag.Error = "შეავსეთ ყველა ველი";
                return View();
            }
            else
            {
                string name = MainHelper.Random32();
                if (ModelState.IsValid)
                {
                   
                Blog obj = new Blog
                {
                    Title = PostData.Title,
                    Text = PostData.Text,
                    CreateDate = DateTime.Now,
                    IsPublished = false,
                    Photo = name,
                    CategoryID = PostData.CategoryID
                    
                    };
                    db.Blogs.Add(obj);
                        db.SaveChanges();

                }

                using (var newImage = ScaleImage(Image.FromStream(PostData.Photo.InputStream, true, true), 300, 400))
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