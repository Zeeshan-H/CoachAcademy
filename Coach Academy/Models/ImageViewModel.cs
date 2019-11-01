using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coach_Academy.Models
{
    public class ImageViewModel
    {
        public int ID { get; set; }
        public Nullable<int> userid { get; set; }
        public string Names { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }

        public HttpPostedFileWrapper ImageFile { get; set; }

    }
}