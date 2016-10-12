using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Description;
using System.Web.Http;
using TodoPagos.Web.Services;
using TodoPagos.UserAPI;
using TodoPagos.Domain.Repository;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/Users")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        /*public UsersController()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            userService = new UserService(unitOfWork);
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
            ClearPasswordsFromTargetUsers(users);
            return Ok(users);
        }

        private void ClearPasswordsFromTargetUsers(IEnumerable<User> targetUsers)
        {
            foreach (User oneUser in targetUsers)
            {
                oneUser.ClearPassword();
            }
        }

        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                User user = userService.GetSingleUser(id);
                user.ClearPassword();
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
            return TryToCreateUserWhileCheckingForArgumentNullAndArgumentException(newUser);
        }

        private IHttpActionResult TryToCreateUserWhileCheckingForArgumentNullAndArgumentException(User newUser)
        {
            try
            {
                return TryToCreateUserWhileCheckingForInvalidOperationException(newUser);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return BadRequest();
            }
        }

        private IHttpActionResult TryToCreateUserWhileCheckingForInvalidOperationException(User newUser)
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
                return TryToUpdateUser(id, user);
            }
        }

        private IHttpActionResult TryToUpdateUser(int id, User user)
        {
            if (!userService.UpdateUser(id, user))
            {
                return DecideWhatErrorMessageToReturn(id, user);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        private IHttpActionResult DecideWhatErrorMessageToReturn(int id, User user)
        {
            if (user == null || id != user.ID)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
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
