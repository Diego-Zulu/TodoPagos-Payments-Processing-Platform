using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TodoPagos.Domain;
using TodoPagos.Domain.DataAccess;
using TodoPagos.Domain.Repository;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{

    [RoutePrefix("api/v1/query/earnings")]
    public class EarningQueriesController : ApiController
    {
        private readonly IEarningQueriesService earningQueriesService;
        private readonly DateTime DEFAULT_FROM_DATE = DateTime.ParseExact("Wed, 29 Aug 1962 00:00:00 GMT",
                "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'", CultureInfo.InvariantCulture);
        private readonly DateTime DEFAULT_TO_DATE = DateTime.Today;
        private readonly string signedInUsername;

        public EarningQueriesController()
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            earningQueriesService = new EarningQueriesService(unitOfWork);
            signedInUsername = HttpContext.Current.User.Identity.Name;
        }

        public EarningQueriesController(IEarningQueriesService service)
        {
            CheckForNullEarningQueriesService(service);
            earningQueriesService = service;
            signedInUsername = "TESTING";
        }

        public EarningQueriesController(string oneUsername)
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            earningQueriesService = new EarningQueriesService(unitOfWork);
            signedInUsername = oneUsername;
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
                return TryToProcessRequestToGetEarningsPerProvider(from, to);
            }
        }

        private IHttpActionResult TryToProcessRequestToGetEarningsPerProvider(DateTime? from, DateTime? to)
        {
            try
            {
                return Ok(earningQueriesService.GetEarningsPerProvider(from.Value, to.Value, signedInUsername));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
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

        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("allEarnings")]
        public IHttpActionResult GetAllEarnings(DateTime? from = null, DateTime? to = null)
        {
            from = AssignFromDateInCaseOfNull(from);
            to = AssignToDateInCaseOfNull(to);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return TryToProcessRequestToGetAllEarnings(from, to);
            }
        }

        private IHttpActionResult TryToProcessRequestToGetAllEarnings(DateTime? from, DateTime? to)
        {
            try
            {
                return Ok(earningQueriesService.GetAllEarnings(from.Value, to.Value, signedInUsername));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}