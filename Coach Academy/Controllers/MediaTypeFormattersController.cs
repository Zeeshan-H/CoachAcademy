using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace Coach_Academy.Controllers
{
    public class MediaTypeFormattersController : ApiController
    {
        [Route("Formatters")]

        public List<string> GetFormatters() {

            return Configuration.Formatters.Select(x => x.ToString()).ToList(); 
        }
    }
}
