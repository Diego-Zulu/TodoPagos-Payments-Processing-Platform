using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using TodoPagos.Domain;
using TodoPagos.Domain.DataAccess;
using TodoPagos.Domain.Repository;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/clients")]
    public class ClientsController : ApiController
    {
        private readonly IClientService clientService;
        private readonly string signedInUsername;

        public ClientsController()
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            clientService = new ClientService(unitOfWork);
            signedInUsername = HttpContext.Current.User.Identity.Name;
        }

        public ClientsController(string username)
        {
            MakeSureUsernameIsNotNullOrWhiteSpace(username);
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            clientService = new ClientService(unitOfWork);
            signedInUsername = username;
        }

        private void MakeSureUsernameIsNotNullOrWhiteSpace(string aUsername)
        {
            if (string.IsNullOrWhiteSpace(aUsername))
            {
                throw new ArgumentException("El nombre de usuario para un controller no puede ser nulo");
            }
        }

        public ClientsController(IClientService oneService)
        {
            FailIfServiceArgumentIsNull(oneService);
            clientService = oneService;
            signedInUsername = "TESTING";
        }

        private void FailIfServiceArgumentIsNull(IClientService oneService)
        {
            if (oneService == null)
            {
                throw new ArgumentException("El servicio de cliente en el controller de cliente no puede ser nulo");
            }
        }

        [HttpGet]
        public IHttpActionResult GetClients()
        {
            try
            {
                IEnumerable<Client> clients = clientService.GetAllClients(signedInUsername);
                return Ok(clients);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            try
            {
                Client client = clientService.GetSingleClient(id, signedInUsername);
                return Ok(client);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client newClient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return TryToCreateClientWhileCheckingForArgumentNullAndArgumentException(newClient);
        }

        private IHttpActionResult TryToCreateClientWhileCheckingForArgumentNullAndArgumentException(Client newClient)
        {
            try
            {
                return TryToCreateClientWhileCheckingForInvalidOperationException(newClient);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        private IHttpActionResult TryToCreateClientWhileCheckingForInvalidOperationException(Client newClient)
        {
            try
            {
                int id = clientService.CreateClient(newClient, signedInUsername);
                return CreatedAtRoute("TodoPagosApi", new { id = newClient.ID }, newClient);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return TryToUpdateClient(id, client);
            }
        }

        private IHttpActionResult TryToUpdateClient(int id, Client client)
        {
            try
            {
                if (!clientService.UpdateClient(id, client, signedInUsername))
                {
                    return DecideWhatErrorMessageToReturn(id, client);
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        private IHttpActionResult DecideWhatErrorMessageToReturn(int id, Client client)
        {
            if (client == null || id != client.ID)
            {
                return BadRequest("El cliente actualizado es nulo o su id no coincide con la del cliente a actualizar");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteClient(int id)
        {
            try
            {
                if (clientService.DeleteClient(id, signedInUsername))
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                return NotFound();
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
                clientService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}