using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metra_Development.Models
{
    public class Projects
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Address { get; set; }
        public bool Parking { get; set; }
        public int ProjectID { get; set; }
        public int HouseNumber { get; set; }
        public int HouseID { get; set; }
        public int Floor { get; set; }

    }
}