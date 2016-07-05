using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyWebSite.Controllers
{
    
    public class SampleController : ApiController
    {
        [HttpGet]
        [Route("api/sample/Values")]
        public IEnumerable<string> Values()
        {
            return new string[] { "value1", "value2" };
        }
       
        [HttpGet]
        [Route("api/sample/ThrowException")]
        public IEnumerable<string> ThrowException()
        {
           throw new ApplicationException("This my exception");
        }

        // GET api/Sample/MakeBadRequest
        [HttpGet]
        [Route("api/sample/MakeBadRequest")]
        public IHttpActionResult MakeBadRequest()
        {
            return BadRequest("example of bad request");
        }

        // GET api/Sample/WriteTrace
        [HttpGet]
        [Route("api/sample/WriteTrace")]
        public IHttpActionResult WriteTrace()
        {
            Trace.WriteLine(string.Format($"site: {Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME")} My message"));
            Trace.WriteLine(string.Format($"site: {Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID")} My message"));

            return Ok();
        }

    }
}
