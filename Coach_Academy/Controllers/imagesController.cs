using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Coach_Academy.Controllers
{

    [RoutePrefix("api/images")]
    
    public class imagesController : ApiController
    {

        
        public HttpResponseMessage Get(int id)
        {
            coachEntities db = new coachEntities();
            var data = from i in db.Clubs
                       where i.ID == id
                       select i;
            Club img = (Club)data.SingleOrDefault();
            byte[] imgData = img.img;
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }
    }
}
