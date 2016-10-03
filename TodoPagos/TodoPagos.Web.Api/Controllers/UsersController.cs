using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoPagos.Web.Services;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService usersService;

        public UsersController(IUserService oneService)
        {
            FailIfServiceArgumentIsNull(oneService);
            usersService = oneService;
            
        }

        private void FailIfServiceArgumentIsNull(IUserService oneService)
        {
            if (oneService == null)
            {
                throw new ArgumentException();
            }
        }

        public IHttpActionResult GetUsers()
        {
            return Ok();
        }
    }
}
