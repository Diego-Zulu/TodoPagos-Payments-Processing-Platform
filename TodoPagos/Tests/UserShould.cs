using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

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

            User notValidUser = new User(name, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyEmail()
        {
            string email = "";
            string name = "Bruno";

            User notValidUser = new User(name, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullEmail()
        {
            string email = null;
            string name = "Nacho";

            User notValidUser = new User(name, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyName()
        {
            string email = "emptyMan@gmail.com";
            string name = "";

            User notValidUser = new User(name, email);
        }
    }
}
