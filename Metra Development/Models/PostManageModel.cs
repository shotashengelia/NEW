﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Metra_Development.Models
{
    public class PostManageModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public bool IsPublished { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
    }
}