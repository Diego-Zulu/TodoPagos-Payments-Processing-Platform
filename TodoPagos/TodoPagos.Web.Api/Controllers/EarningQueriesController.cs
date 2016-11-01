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
    [Authorize]
    public class EarningQueriesController : ApiController
    {
        private readonly IEarningQueriesService earningQueriesService;
        private readonly DateTime DEFAULT_FROM_DATE = DateTime.MinValue;
        private readonly DateTime DEFAULT_TO_DATE = DateTime.Now.ToUniversalTime();
        private readonly string signedInUsername;
        private readonly string[] ACCEPTED_DATE_FORMATS = new[]{ "yyyy-MM-ddTHH:mm:ssZ" };

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
            FailIfUsernameArgumentIsNull(oneUsername);
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            earningQueriesService = new EarningQueriesService(unitOfWork);
            signedInUsername = oneUsername;
        }

        private void FailIfUsernameArgumentIsNull(string oneUsername)
        {
            if (oneUsername == null)
            {
                throw new ArgumentException();
            }
        }

        private void CheckForNullEarningQueriesService(IEarningQueriesService service)
        {
            if (service == null) throw new ArgumentException();
        }

        [HttpGet]
        [ResponseType(typeof(IDictionary<Provider, int>))]
        [Route("earningsPerProvider")]
        public IHttpActionResult GetEarningsPerProvider(string from = null, string to = null)
        {
            DateTime dateFrom;
            DateTime dateTo;
            try
            {
                dateFrom = AssignFromDateInCaseOfNull(from);
                dateTo = AssignToDateInCaseOfNull(to);
            }
            catch (FormatException)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return TryToProcessRequestToGetEarningsPerProvider(dateFrom, dateTo);
            }
        }

        private IHttpActionResult TryToProcessRequestToGetEarningsPerProvider(DateTime from, DateTime to)
        {
            try
            {
                return Ok(earningQueriesService.GetEarningsPerProvider(from, to, signedInUsername));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        private DateTime AssignFromDateInCaseOfNull(string from)
        {
            if (from == null) return DEFAULT_FROM_DATE;
            return DateTime.ParseExact(from, ACCEPTED_DATE_FORMATS,
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        private DateTime AssignToDateInCaseOfNull(string to)
        {
            if (to == null) return DEFAULT_TO_DATE;
            return DateTime.ParseExact(to, ACCEPTED_DATE_FORMATS,
                CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        [HttpGet]
        [ResponseType(typeof(int))]
        [Route("allEarnings")]
        public IHttpActionResult GetAllEarnings(string from = null, string to = null)
        {
            DateTime dateFrom;
            DateTime dateTo;
            try
            {
                dateFrom = AssignFromDateInCaseOfNull(from);
                dateTo = AssignToDateInCaseOfNull(to);
            } catch (FormatException)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)

            {
                return BadRequest(ModelState);
            }
            else
            {
                return TryToProcessRequestToGetAllEarnings(dateFrom, dateTo);
            }
        }

        private IHttpActionResult TryToProcessRequestToGetAllEarnings(DateTime from, DateTime to)
        {
            try
            {
                return Ok(earningQueriesService.GetAllEarnings(from, to, signedInUsername));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                earningQueriesService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}