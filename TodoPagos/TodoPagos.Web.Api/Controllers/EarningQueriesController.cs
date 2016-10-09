using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TodoPagos.Domain;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{

    [RoutePrefix("api/v1/query/earnings")]
    public class EarningQueriesController : ApiController
    {
        private readonly IEarningQueriesService earningQueriesService;
        private readonly DateTime DEFAULT_FROM_DATE = DateTime.ParseExact("Wed, 29 Aug 1962 00:00:00 GMT",
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
        private readonly DateTime DEFAULT_TO_DATE = DateTime.ParseExact("Tue, 31 Dec 2030 00:00:00 GMT",
                 "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);

        public EarningQueriesController(IEarningQueriesService service)
        {
            CheckForNullEarningQueriesService(service);
            earningQueriesService = service;
        }

        private void CheckForNullEarningQueriesService(IEarningQueriesService service)
        {
            if (service == null) throw new ArgumentException();
        }

        [HttpGet]
        [ResponseType(typeof(IDictionary<Provider, int>))]
        [Route("earningsPerProvider")]
        public IHttpActionResult GetEarningsPerProvider(DateTime? from = null, DateTime? to = null)
        {
            from = AssignFromDateInCaseOfNull(from);
            to = AssignToDateInCaseOfNull(to);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(earningQueriesService.GetEarningsPerProvider(from.Value, to.Value));
            }
        }

        private DateTime? AssignFromDateInCaseOfNull(DateTime? from)
        {
            if (from == null) from = DEFAULT_FROM_DATE;
            return from;
        }

        private DateTime? AssignToDateInCaseOfNull(DateTime? to)
        {
            if (to == null) to = DEFAULT_TO_DATE;
            return to;
        }
    }
}