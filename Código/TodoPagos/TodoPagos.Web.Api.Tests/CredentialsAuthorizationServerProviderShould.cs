using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin;
using System.Collections.Generic;

namespace TodoPagos.Web.Api.Tests
{
    [TestClass]
    public class CredentialsAuthorizationServerProviderShould
    {
        [TestMethod]
        public void BeAbleToGrantCredentialsToUser()
        {
            CredentialsAuthorizationServerProvider prov = new CredentialsAuthorizationServerProvider();

            prov.GrantResourceOwnerCredentials(
                new OAuthGrantResourceOwnerCredentialsContext(
                    new OwinContext(), new OAuthAuthorizationServerOptions(), "1", "diego@bruno.com", "HOLA1234", new List<string>()));
        }

        [TestMethod]
        public void FailToGrantCredentialsToUserIfPasswordIsIncorrect()
        {
            CredentialsAuthorizationServerProvider prov = new CredentialsAuthorizationServerProvider();

            prov.GrantResourceOwnerCredentials(
                new OAuthGrantResourceOwnerCredentialsContext(
                    new OwinContext(), new OAuthAuthorizationServerOptions(), "1", "diego@bruno.com", "sjdlkjasdlkjasd;kajsd", new List<string>()));
        }
    }
}
