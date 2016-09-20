using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UserShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnInvalidEmail()
        {
            string email = "invalid";
            string name = "Diego";

            UserShould notValidUser = new UserShould(name, email);
        }
    }
}
