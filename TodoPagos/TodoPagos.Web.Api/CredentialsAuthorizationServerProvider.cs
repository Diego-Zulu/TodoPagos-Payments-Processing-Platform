using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;
using TodoPagos.Domain.DataAccess;
using TodoPagos.UserAPI;
using TodoPagos.Domain.Repository;
using Domain;

namespace TodoPagos.Web.Api
{
    public class CredentialsAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<Object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

           using (TodoPagosContext db = new TodoPagosContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Email == context.UserName);
                if (user == null || !Hashing.VerifyHash(context.Password, user.Salt, user.Password))
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return Task.FromResult<Object>(null);
                }
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            context.Validated(identity);
            LogThisLoginInDatabase(context.UserName);
     
            return Task.FromResult<Object>(null);
        }

        private void LogThisLoginInDatabase(string email)
        {
            TodoPagosContext databaseContext = new TodoPagosContext();
            IUnitOfWork unitOfWork = new UnitOfWork(databaseContext);
            ILogStrategy log = new LogDatabaseConcreteStrategy(unitOfWork);
            LogEntry entry = new LogEntry(ActionType.LOGIN, email);
            log.SaveEntry(entry);
            unitOfWork.Dispose();
        }
    }
}