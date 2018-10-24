using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metra_Development.Models
{
    public class ApartmentCustom
    {
        public int ID { get; set; }
        public string FloodIDHouseIDProjectName { get; set; }
        public HttpPostedFileBase Photo { get; set; }

    }
}