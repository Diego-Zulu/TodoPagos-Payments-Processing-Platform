using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoPagos.Web.Services;
using TodoPagos.UserAPI;
using System.Web.Http.Description;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UserController : ApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService oneService)
        {
            FailIfServiceArgumentIsNull(oneService);
            userService = oneService;
            
        }

        private void FailIfServiceArgumentIsNull(IUserService oneService)
        {
            if (oneService == null)
            {
                throw new ArgumentException();
            }
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            IEnumerable<User> users = userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                User user = userService.GetSingleUser(id);
                return Ok(user);
            } catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User newUser)
        {
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
