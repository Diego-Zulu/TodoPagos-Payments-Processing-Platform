﻿using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/v1/clients")]
    [Authorize]
    public class ClientsController : ApiController
    {
        private readonly IClientService clientService;
        private readonly string signedInUsername;

        public ClientsController()
        {
            //TodoPagosContext context = new TodoPagosContext();
            //IUnitOfWork unitOfWork = new UnitOfWork(context);
            //clientService = new ClientService(unitOfWork);
            signedInUsername = HttpContext.Current.User.Identity.Name;
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
    }
}