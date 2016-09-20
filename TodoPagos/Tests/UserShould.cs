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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAWhiteSpaceName()
        {
            string email = "MrWhite@outlook.com";
            string name = "          ";

            User notValidUser = new User(name, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullName()
        {
            string email = "TheNuller@yahoo.com";
            string name = null;

            User notValidUser = new User(name, email);
        }

        [TestMethod]
        public void KnowIfHeDoesntHaveAGivenRole()
        {
            IRole cashierRole = new CashierRole();
            IRole adminRole = new AdminRole();
            string userEmail = "BirdFriend@hotmail.com";
            string userName = "Holly";
            User newUser = new User(userName, userEmail, cashierRole);

            Assert.IsFalse(newUser.HasThisRole(adminRole));

        }
    }
}
