using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/providers")]
    public class ProvidersController : ApiController
    {
        private readonly IProviderService providerService;

        public ProvidersController(IProviderService oneService)
        {
            if (oneService == null)
            {
                throw new ArgumentException();
            }
            providerService = oneService;
        }


    }
}
