using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Coach_Academy.Controllers
{
        [RoutePrefix("api/getclubs")]

    public class getclubsController : ApiController
    {
        [HttpGet]

       
        public IEnumerable<Club> Get()
        {

            using (coachEntities entities = new coachEntities())
            {

                return entities.Clubs.ToList();
            }
        }

    }
}
