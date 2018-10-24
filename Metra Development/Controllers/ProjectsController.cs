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
    public class ProjectsController : Controller
    {
        Metraentities db;
        void CheckConnection()                                          //check if entities is connected
        {
            if (db == null)
            {
                db = new Metraentities();
            }
        }
        // GET: Projects
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddProject()
        {
            CheckConnection();


            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Projects PR)
        {
            CheckConnection();
            if (string.IsNullOrEmpty(PR.Name) || string.IsNullOrEmpty(PR.Address))
            {
                ViewBag.Error = "შეავსეთ ყველა ველი";
                return View();
            }
            else
            {
                Project pr = new Project();
                pr.Name = PR.Name;
                pr.Address = PR.Address;
                pr.Status = false;

                db.Projects.Add(pr);
                db.SaveChanges();
                return View();
            }
        }
        public ActionResult AddHouse()
        {
            CheckConnection();
            var getProjectlist = db.Projects.ToList();
            getProjectlist.Insert(0, new Project { ID = 0, Name = "აირჩიეთ პროექტი" });
            ViewBag.getProjectlist = getProjectlist;
            ViewBag.selectProjecttype = 0;

            return View();

        }
        [HttpPost]
        public ActionResult AddHouse(Projects Pr)
        {
            CheckConnection();
            var getProjectlist = db.Projects.ToList();
            getProjectlist.Insert(0, new Project { ID = 0, Name = "აირჩიეთ პროექტი" });
            ViewBag.getProjectlist = getProjectlist;
            ViewBag.selectProjecttype = 0;



            House h = new House();
            h.HouseID = Pr.HouseNumber;
            h.Parking = true;
            h.ProjectID = Pr.ProjectID;
            db.Houses.Add(h);
            db.SaveChanges();

            return View();

        }

        public ActionResult AddStorey()
        {
            CheckConnection();
            var getHouselist = db.Houses.ToList();
            var newList = new List<HouseCustom>();
            foreach (var house in getHouselist)
            {
                var newHouse = new HouseCustom() { ID = house.ID, HouseIDANDProjectName = house.HouseID + " კორპუსი - " + house.Project.Name };
                newList.Add(newHouse);
            }

            newList.Insert(0, new HouseCustom { ID = 0, HouseIDANDProjectName = "აირჩიეთ სახლი" });
            ViewBag.getHouselist = newList;
            ViewBag.selecttype = 0;

            return View();
        }
        [HttpPost]
        public ActionResult AddStorey(Floor st)
        {
            CheckConnection();
            var getHouselist = db.Houses.ToList();
            var newList = new List<HouseCustom>();
            foreach (var house in getHouselist)
            {
                var newHouse = new HouseCustom() { ID = house.ID, HouseIDANDProjectName = house.HouseID + " კორპუსი - " + house.Project.Name };
                newList.Add(newHouse);
            }

            newList.Insert(0, new HouseCustom { ID = 0, HouseIDANDProjectName = "აირჩიეთ სახლი" });
            ViewBag.getHouselist = newList;
            ViewBag.selecttype = 0;

            Floor s = new Floor();
            s.HouseID = st.HouseID;
            s.Floor1 = st.Floor1;
            db.Floors.Add(s);
            db.SaveChanges();

            return View();


        }
        public ActionResult AddApartment()
        {
            CheckConnection();
            var getFloorList = db.Floors.ToList();
            var newList = new List<ApartmentCustom>();
            foreach (var Floor in getFloorList)
            {
                var NewFloor = new ApartmentCustom() { ID = Floor.ID, FloodIDHouseIDProjectName = Floor.Floor1 + " სართული - " + Floor.House.HouseID + " კორპუსი - " + Floor.House.Project.Name };
                newList.Add(NewFloor);
            }

            newList.Insert(0, new ApartmentCustom { ID = 0, FloodIDHouseIDProjectName = "აირჩიეთ სართული" });
            ViewBag.getFloorlist = newList;
            ViewBag.selecttype = 0;


            return View();
        }
        [HttpPost]
        public ActionResult AddApartment(Apartment ap,ApartmentCustom apc)
        {
            CheckConnection();

            string name = MainHelper.Random32();
            var getFloorList = db.Floors.ToList();
            var newList = new List<ApartmentCustom>();
            foreach (var Floor in getFloorList)
            {
                var NewFloor = new ApartmentCustom() { ID = Floor.ID, FloodIDHouseIDProjectName = Floor.Floor1 + " სართული - " + Floor.House.HouseID + " კორპუსი - " + Floor.House.Project.Name };
                newList.Add(NewFloor);
            }

            newList.Insert(0, new ApartmentCustom { ID = 0, FloodIDHouseIDProjectName = "აირჩიეთ სართული" });
            ViewBag.getFloorlist = newList;
            ViewBag.selecttype = 0;



            Apartment a = new Apartment();
            a.Area = ap.Area;
            a.Bedroom = ap.Bedroom;
            a.Washroom = ap.Washroom;
            a.FloorID = ap.FloorID;
            a.Status = false;
            a.Price = ap.Price;
            a.ArchitectualDraw = name;

            db.Apartments.Add(a);
            db.SaveChanges();


            using (var newImage = ScaleImage(Image.FromStream(apc.Photo.InputStream, true, true), 300, 400))
            {
                string path = Server.MapPath("/Content/Images/" + name + ".png");
                newImage.Save(path, ImageFormat.Png);
            }
            return View();
        }

        public ActionResult AddRoom()
        {
            CheckConnection();
            var getApartmentList = db.Apartments.ToList();
            var newList = new List<RoomCustom>();
            foreach (var Apartment in getApartmentList)
            {
                var NewRoom = new RoomCustom() { ID = Apartment.ID, ApartamentID_FloodID_HouseID_ProjectName = Apartment.ID + " ბინის აიდი - " + Apartment.Floor.Floor1 + " სართული - " + Apartment.Floor.House.HouseID + " კორპუსი - " + Apartment.Floor.House.Project.Name };
                newList.Add(NewRoom);
            }

            newList.Insert(0, new RoomCustom { ID = 0, ApartamentID_FloodID_HouseID_ProjectName = "აირჩიეთ ბინის ნომერი" });
            ViewBag.getApartmentlist = newList;
            ViewBag.selecttype = 0;


            return View();
        }
        [HttpPost]
        public ActionResult AddRoom(Room Rm)
        {
            CheckConnection();
            var getApartmentList = db.Apartments.ToList();
            var newList = new List<RoomCustom>();
            foreach (var Apartment in getApartmentList)
            {
                var NewRoom = new RoomCustom() { ID = Apartment.ID, ApartamentID_FloodID_HouseID_ProjectName = Apartment.ID + " ბინის აიდი - " + Apartment.Floor.Floor1 + " სართული - " + Apartment.Floor.House.HouseID + " კორპუსი - " + Apartment.Floor.House.Project.Name };
                newList.Add(NewRoom);
            }

            newList.Insert(0, new RoomCustom { ID = 0, ApartamentID_FloodID_HouseID_ProjectName = "აირჩიეთ ბინის ნომერი" });
            ViewBag.getApartmentlist = newList;
            ViewBag.selecttype = 0;

            Room r = new Room();
            r.Name = Rm.Name;
            r.Area = Rm.Area;
            r.ApartmentID = Rm.ApartmentID;

            db.Rooms.Add(r);
            db.SaveChanges();

            return View();
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
  


