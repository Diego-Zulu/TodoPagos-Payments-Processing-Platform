﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Description;
using System.Web.Http;
using TodoPagos.Web.Services;
using TodoPagos.UserAPI;
using TodoPagos.Domain.Repository;
using TodoPagos.Domain.DataAccess;
using System.Web;
using System.Linq;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http.ModelBinding;
using TodoPagos.Web.Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoPagos.Web.Api.Controllers
{
    [RoutePrefix("api/v1/users")]
    [Authorize]
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

        public UsersController(string oneUsername)
        {
            FailIfUsernameArgumentIsNull(oneUsername);
            TodoPagosContext context = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            userService = new UserService(unitOfWork);
            signedInUsername = oneUsername;
        }

        private void FailIfUsernameArgumentIsNull(string oneUsername)
        {
            if (oneUsername == null)
            {
                throw new ArgumentException("El nombre de usuario no puede ser nulo");
            }
        }

        private void FailIfServiceArgumentIsNull(IUserService oneService)
        {
            if (oneService == null)
            {
                throw new ArgumentException("El servicio no puede ser nulo");
            }
        }

        [HttpGet]
        public IHttpActionResult GetUsers()
        {
            try
            {
                IEnumerable<User> users = userService.GetAllUsers(signedInUsername);
                IEnumerable<User> usersWithoutSaltAndPasswords = ClearPasswordsAndSaltsFromNewInstanceOfTargetUsers(users);
                return Ok(usersWithoutSaltAndPasswords);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        private IEnumerable<User> ClearPasswordsAndSaltsFromNewInstanceOfTargetUsers(IEnumerable<User> targetUsers)
        {
            ICollection<User> newInstances = new List<User>();
            foreach (User oneUser in targetUsers)
            {
                User newInstanceOfUser = oneUser.CloneAndReturnNewUserWithoutPasswordAndSalt();
                newInstances.Add(newInstanceOfUser);
            }

            return newInstances;
        }

        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            try
            {
                User user = userService.GetSingleUser(id, signedInUsername);
                return Ok(user.CloneAndReturnNewUserWithoutPasswordAndSalt());
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

        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        [Route("getRoles")]
        public IHttpActionResult GetRolesOfUser(string userEmail)
        {
            try
            {
                IEnumerable<string> rolesOfUser = userService.GetRolesOfUser(userEmail, signedInUsername);
                return Ok(rolesOfUser);
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
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        private IHttpActionResult TryToCreateUserWhileCheckingForInvalidOperationException(User newUser)
        {
            try
            {
                int id = userService.CreateUser(newUser, signedInUsername);
                return CreatedAtRoute("TodoPagosApi", new { id = newUser.ID }, newUser.CloneAndReturnNewUserWithoutPasswordAndSalt());
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
        public IHttpActionResult PutUser([FromUri]int id, [ModelBinder(typeof(UserModelBinder))] User user)
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
                return BadRequest("El usuario actualizado es nulo o su id no coincide con la del usuario a actualizar");
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
