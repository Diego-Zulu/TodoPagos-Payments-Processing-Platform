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
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        /*public UsersController()
        {
            userService = new UserService();
        }*/

        public UsersController(IUserService oneService)
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
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return TryToCreateUser(newUser);
        }

        private IHttpActionResult TryToCreateUser(User newUser)
        {
            try
            {
                int id = userService.CreateUser(newUser);
                return CreatedAtRoute("TodoPagosApi", new { id = newUser.ID }, newUser);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {
                return checkIfUserIdIsCorrectAndTryToUpdate(id, user);
            }
        }

        private IHttpActionResult checkIfUserIdIsCorrectAndTryToUpdate(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            } else
            {
                return tryToUpdateUser(id, user);
            } 
        }

        private IHttpActionResult tryToUpdateUser(int id, User user)
        {
            if (!userService.UpdateUser(id, user))
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteUser(int id)
        {
            if (userService.DeleteUser(id))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return NotFound();
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
