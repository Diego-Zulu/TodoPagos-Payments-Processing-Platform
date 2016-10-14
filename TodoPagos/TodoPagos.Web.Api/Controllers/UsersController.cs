using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Description;
using System.Web.Http;
using TodoPagos.Web.Services;
using TodoPagos.UserAPI;
using TodoPagos.Domain.Repository;
using TodoPagos.Domain.DataAccess;
using System.Web;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/Users")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly string nameOfUser;

        public UsersController()
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            userService = new UserService(unitOfWork);
            nameOfUser = HttpContext.Current.User.Identity.Name;
        }

        public UsersController(IUserService oneService)
        {
            FailIfServiceArgumentIsNull(oneService);
            userService = oneService;
            nameOfUser = "TESTING";
        }

        private void FailIfServiceArgumentIsNull(IUserService oneService)
        {
            if (oneService == null)
            {
                throw new ArgumentException();
            }
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetUsers()
        {
            IEnumerable<User> users = userService.GetAllUsers();
            ClearPasswordsAndSaltsFromTargetUsers(users);
            return Ok(users);
        }

        private void ClearPasswordsAndSaltsFromTargetUsers(IEnumerable<User> targetUsers)
        {
            foreach (User oneUser in targetUsers)
            {
                oneUser.ClearPassword();
                oneUser.ClearSalt();
            }
        }

        [HttpGet]
        [Authorize]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                User user = userService.GetSingleUser(id);
                user.ClearPassword();
                user.ClearSalt();
                return Ok(user);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User newUser, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return TryToCreateUserWhileCheckingForArgumentNullAndArgumentException(newUser, password);
        }

        private IHttpActionResult TryToCreateUserWhileCheckingForArgumentNullAndArgumentException(User newUser, string password)
        {
            try
            {
                return TryToCreateUserWhileCheckingForInvalidOperationException(newUser, password);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return BadRequest();
            }
        }

        private IHttpActionResult TryToCreateUserWhileCheckingForInvalidOperationException(User newUser, string password)
        {
            try
            {
                int id = userService.CreateUser(newUser, password, nameOfUser);
                return CreatedAtRoute("TodoPagosApi", new { id = newUser.ID }, newUser);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } else
            {
                return TryToUpdateUser(id, user, password);
            }
        }

        private IHttpActionResult TryToUpdateUser(int id, User user, string password)
        {
            if (!userService.UpdateUser(id, user, password, nameOfUser))
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
        [Authorize]
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
