using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UserAPI;

namespace Tests
{
    [TestClass]
    public class UserShould
    {
        const int FIRST_POSITION = 0;

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnInvalidEmail()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "invalid";
            string name = "Diego";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyEmail()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "";
            string name = "Bruno";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullEmail()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = null;
            string name = "Nacho";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyName()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "emptyMan@gmail.com";
            string name = "";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAWhiteSpaceName()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "MrWhite@outlook.com";
            string name = "          ";

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullName()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "TheNuller@yahoo.com";
            string name = null;

            User notValidUser = new User(name, email, adminRole);
        }

        [TestMethod]
        public void KnowIfHeDoesntHaveAGivenRole()
        {
            Role cashierRole = CashierRole.GetInstance();
            Role adminRole = AdminRole.GetInstance();
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
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "Mad_Cashier_404@ort.com.uy";
            string userName = "Raul";
            User newUser = new User(userName, userEmail, cashierRole);

            newUser.RemoveRole(cashierRole);
        }

        [TestMethod]
        public void NotAddDuplicateRoles()
        {
            Role firstCashierRole = CashierRole.GetInstance();
            Role secondCashierRole = CashierRole.GetInstance();
            string userEmail = "DoubleCASHBABY@ort.com.uy";
            string userName = "Riki";
            User newUser = new User(userName, userEmail, firstCashierRole);
            int roleAmountBeforeAddition = newUser.GetRoleCount();

            newUser.AddRole(secondCashierRole);

            Assert.AreEqual(roleAmountBeforeAddition, newUser.GetRoleCount());
        }

        [TestMethod]
        public void KnowIfItHasACertainPrivilege()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "DoubleCASHBABY@ort.com.uy";
            string userName = "Riki";
            User newUser = new User(userName, userEmail, cashierRole);

            ICollection<Privilege> cashierPrivileges = cashierRole.Privileges;
            Privilege firstPrivilege = cashierPrivileges.ElementAt(FIRST_POSITION);

            Assert.IsTrue(newUser.HasPrivilege(firstPrivilege));
        }

        [TestMethod]
        public void BeAbleToAddRoles()
        {
            Role cashierRole = CashierRole.GetInstance();
            Role adminRole = AdminRole.GetInstance();
            string userEmail = "MoneyMoneyMoney@gmail.com";
            string userName = "Mani";
            User newUser = new User(userName, userEmail, cashierRole);

            newUser.AddRole(adminRole);

            Assert.IsTrue(newUser.HasThisRole(adminRole) && newUser.HasThisRole(cashierRole));
        }

        [TestMethod]
        public void BeAbleToRemoveRoles()
        {
            Role cashierRole = CashierRole.GetInstance();
            Role adminRole = AdminRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            User newUser = new User(userName, userEmail, cashierRole);

            newUser.AddRole(adminRole);
            newUser.RemoveRole(cashierRole);

            Assert.IsFalse(newUser.HasThisRole(cashierRole));
        }
    }
}
