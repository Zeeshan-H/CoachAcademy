using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net.Http.Headers;
using Coach_Academy.Models;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;



namespace Coach_Academy.Controllers
{

    [RoutePrefix("api/users")]

    public class usersController : ApiController
    {
        [AllowAnonymous]

        [HttpGet]

        public IEnumerable<User> Get()
        {

            using (coachEntities entities = new coachEntities())
            {

                return entities.Users.ToList();
            }
        }

        [HttpPost]
        [ResponseType(typeof(User))]

        public IHttpActionResult Post(User tb)
        {

            coachEntities db = new coachEntities();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Users.Add(tb);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new
            {
                id = tb.ID

            }, tb);



        }
       
        [Route("upload")]
        [HttpPost]

        public async Task<string> Upload()
        {
            var ctx = HttpContext.Current;
            var root =  ctx.Server.MapPath("/uploads/");
            var provider =
                new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content
                    .ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers
                        .ContentDisposition
                        .FileName;

                    // remove double quotes from string.
                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(
                        root, "files", name);

                    //File.Move(localFileName, filePath);

                 //SaveFilePathSQLServerADO(localFileName, filePath);
                    //SaveFileBinarySQLServerADO(localFileName, name);

                    //SaveFilePathSQLServerEF(localFileName, filePath);
                        SaveFileBinarySQLServerEF(localFileName, name);

                    if (File.Exists(localFileName))
                        File.Delete(localFileName);
                }
            }
            catch 
            {
                return "Unable to upload, try again!";
            }

            return "File uploaded!";

        }


        public void SaveFileBinarySQLServerEF(
         string localFile, string fileName)
        {
            // 1) Get file binary
            byte[] fileBytes;
            using (var fs = new FileStream(
                localFile, FileMode.Open, FileAccess.Read))
            {
                fileBytes = new byte[fs.Length];
                fs.Read(
                    fileBytes, 0, Convert.ToInt32(fs.Length));
            }

            // 2) Create a Files object
            var file = new tblimage()
            {
                Data = fileBytes,
                Names = fileName,

            };

            // 3) Add and save it in database
            using (var ctx = new coachEntities())
            {
                ctx.tblimages.Add(file);

                ctx.SaveChanges();
            }

        }

        [Route("getimg")]
        [HttpGet]

        public HttpResponseMessage Getimg(int id)
        {
            coachEntities db = new coachEntities();
            var data = from i in db.tblimages
                       where i.ID == id
                       select i;
            tblimage img = (tblimage)data.SingleOrDefault();
            byte[] imgData = img.Data;
            MemoryStream ms = new MemoryStream(imgData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            return response;
        }
      
    }
      
    
}
