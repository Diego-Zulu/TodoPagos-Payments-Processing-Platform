using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoPagos.Web.Services;
using TodoPagos.Domain;
using System.Web.Http.Description;
using TodoPagos.Domain.Repository;
using TodoPagos.Domain.DataAccess;
using System.Web.Http.ModelBinding;
using TodoPagos.Web.Api.Models;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/providers")]
    public class ProvidersController : ApiController
    {
        private readonly IProviderService providerService;

        public ProvidersController()
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            providerService = new ProviderService(unitOfWork);
        }

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

        [HttpGet]
        public IHttpActionResult GetProviders(bool getActiveProviders)
        {
            IEnumerable<Provider> users = providerService.GetAllProvidersAcoordingToState(getActiveProviders);
            return Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetProviders()
        {
            IEnumerable<Provider> users = providerService.GetAllProviders();
            return Ok(users);
        }

        [HttpGet]
        [ResponseType(typeof(Provider))]
        public IHttpActionResult GetProvider(int id)
        {
            try
            {
                Provider targetProvider = providerService.GetSingleProvider(id);
                return Ok(targetProvider);
            } catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProvider(int id, [ModelBinder(typeof(ProviderModelBinder))] Provider oneProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return TryToUpdateProvider(id, oneProvider);
            }
        }

        private IHttpActionResult TryToUpdateProvider(int id, Provider oneProvider)
        {
            if (!providerService.UpdateProvider(id, oneProvider))
            {
                return DecideWhatErrorMessageToReturn(id, oneProvider);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        private IHttpActionResult DecideWhatErrorMessageToReturn(int id, Provider oneProvider)
        {
            if (oneProvider == null || id != oneProvider.ID)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(Provider))]
        public IHttpActionResult PostProvider([ModelBinder(typeof(ProviderModelBinder))] Provider newProvider)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return TryToCreateProviderWhileCheckingForArgumentNullException(newProvider);
        }

        private IHttpActionResult TryToCreateProviderWhileCheckingForArgumentNullException(Provider newProvider)
        {
            try
            {
                return TryToCreateProviderWhileCheckingForInvalidOperationException(newProvider);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        private IHttpActionResult TryToCreateProviderWhileCheckingForInvalidOperationException(Provider newProvider)
        {
            try
            {
                return TryToCreateProviderWhileCheckingForArgumentException(newProvider);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        private IHttpActionResult TryToCreateProviderWhileCheckingForArgumentException(Provider newProvider)
        {
            try
            {
                int id = providerService.CreateProvider(newProvider);
                return CreatedAtRoute("TodoPagosApi", new { id = newProvider.ID }, newProvider);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteProvider(int id)
        {
            if (providerService.MarkProviderAsDeleted(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                providerService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
