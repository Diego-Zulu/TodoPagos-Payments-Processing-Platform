﻿using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetEarningsPerProvider(DateTime from, DateTime to)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(earningQueriesService.GetEarningsPerProvider(from, to));
            }
        }
    }
}