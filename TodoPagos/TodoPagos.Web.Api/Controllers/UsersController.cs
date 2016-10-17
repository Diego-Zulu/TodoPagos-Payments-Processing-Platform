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
using System.Web.Http.ModelBinding;
using TodoPagos.Web.Api.Models;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly string signedInUsername;

        public UsersController()
        {
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            userService = new UserService(unitOfWork);
            signedInUsername = HttpContext.Current.User.Identity.Name;
        }

        public UsersController(IUserService oneService)
        {
            FailIfServiceArgumentIsNull(oneService);
            userService = oneService;
            signedInUsername = "TESTING";
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
            try
            {
                IEnumerable<User> users = userService.GetAllUsers(signedInUsername);
                ClearPasswordsAndSaltsFromTargetUsers(users);
                return Ok(users);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
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
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                User user = userService.GetSingleUser(id, signedInUsername);
                user.ClearPassword();
                user.ClearSalt();
                return Ok(user);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser([ModelBinder(typeof(UserModelBinder))] User newUser)
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
                int id = userService.CreateUser(newUser, signedInUsername);
                newUser.ClearSalt();
                newUser.ClearPassword();
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
        public IHttpActionResult PutUser(int id, [ModelBinder(typeof(UserModelBinder))] User user)
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
            try
            {

                if (!userService.UpdateUser(id, user, signedInUsername))
                {
                    return DecideWhatErrorMessageToReturn(id, user);
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
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
            try
            {
                if (userService.DeleteUser(id, signedInUsername))
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
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
