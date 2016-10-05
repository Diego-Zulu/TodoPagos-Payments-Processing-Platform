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
            MakeSureProvidedProviderServiceIsNotNull(oneService);
            providerService = oneService;
        }

        private void MakeSureProvidedProviderServiceIsNotNull(IProviderService providedProviderService)
        {
            if (providedProviderService == null)
            {
                throw new ArgumentException();
            }
        }
    }
}
