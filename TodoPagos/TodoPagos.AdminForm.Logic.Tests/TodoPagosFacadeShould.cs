using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TodoPagos.AdminForm.Logic.Tests
{
    [TestClass]
    public class TodoPagosFacadeShould
    {
        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void FailIfUserDoesntHaveTheRightRole()
        {
            TodoPagosFacade facade = new TodoPagosFacade();
            string email = "soycajero@hotmail.com";
            string password = "Hola1234!";

            bool login = facade.Login(email, password);
        }
    }
}
