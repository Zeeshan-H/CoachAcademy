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
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System.Web.Routing;



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

       
       
    }

}
