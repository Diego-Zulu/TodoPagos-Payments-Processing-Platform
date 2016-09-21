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
            Role adminRole = new AdminRole();
            string email = "invalid";
            string name = "Diego";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyEmail()
        {
            Role adminRole = new AdminRole();
            string email = "";
            string name = "Bruno";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullEmail()
        {
            Role adminRole = new AdminRole();
            string email = null;
            string name = "Nacho";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyName()
        {
            Role adminRole = new AdminRole();
            string email = "emptyMan@gmail.com";
            string name = "";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAWhiteSpaceName()
        {
            Role adminRole = new AdminRole();
            string email = "MrWhite@outlook.com";
            string name = "          ";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullName()
        {
            Role adminRole = new AdminRole();
            string email = "TheNuller@yahoo.com";
            string name = null;

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        public void KnowIfHeDoesntHaveAGivenRole()
        {
            Role cashierRole = new CashierRole();
            Role adminRole = new AdminRole();
            string userEmail = "BirdFriend@hotmail.com";
            string userName = "Holly";
            User newUser = new User(userName, userEmail, cashierRole);

            Assert.IsFalse(newUser.HasThisRole(adminRole));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullRole()
        {
            Role nullRole = null;
            string userEmail = "TheNuller2_0@hotmail.com";
            string userName = "Molly";
            User newUser = new User(userName, userEmail, nullRole);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotRemoveLastRemainingRole()
        {
            Role cashierRole = new CashierRole();
            string userEmail = "Mad_Cashier_404@ort.com.uy";
            string userName = "Raul";
            User newUser = new User(userName, userEmail, cashierRole);

            newUser.RemoveRole(cashierRole);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotAddDuplicateRoles()
        {
            Role firstCashierRole = new CashierRole();
            Role secondCashierRole = new CashierRole();
            string userEmail = "DoubleCASHBABY@ort.com.uy";
            string userName = "Riki";
            User newUser = new User(userName, userEmail, firstCashierRole);

            newUser.AddRole(secondCashierRole);
        }
    }
}
