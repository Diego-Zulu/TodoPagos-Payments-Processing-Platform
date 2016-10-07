using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{

    [RoutePrefix("api/v1/query/earnings")]
    public class EarningQueriesController : ApiController
    {
        private readonly IEarningQueriesService earningQueriesService;

        public EarningQueriesController(IEarningQueriesService service)
        {
            earningQueriesService = service;
        }
    }
}