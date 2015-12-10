using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQLDependancySignalR.Controllers
{
    
    public class JobController : ApiController
    {
        JobInfoRepository objRepo = new JobInfoRepository();
        [Route("Job")]
        public IEnumerable<JobInfo> Get()
        {
            return objRepo.GetData();
        }
    }
}
