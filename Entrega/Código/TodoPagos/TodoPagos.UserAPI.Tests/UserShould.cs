﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TodoPagos.UserAPI;
using System.Security.Cryptography;
using TodoPagos.Domain;
using System.Collections;

namespace TodoPagos.UserAPI.Tests
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
            string password = "HolaCom1";

            User notValidUser = new User(name, email, password, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyEmail()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "";
            string name = "Bruno";
            string password = "HolaCom1";

            User notValidUser = new User(name, email, password, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullEmail()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = null;
            string name = "Nacho";
            string password = "HolaCom1";

            User notValidUser = new User(name, email, password, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAnEmptyName()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "emptyMan@gmail.com";
            string name = "";
            string password = "HolaCom1";

            User notValidUser = new User(name, email, password, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveAWhiteSpaceName()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "MrWhite@outlook.com";
            string name = "          ";
            string password = "HolaCom1";

            User notValidUser = new User(name, email, password, adminRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullName()
        {
            Role adminRole = AdminRole.GetInstance();
            string email = "TheNuller@yahoo.com";
            string name = null;
            string password = "HolaCom1";

            User notValidUser = new User(name, email, password, adminRole);
        }

        [TestMethod]
        public void KnowIfHeDoesntHaveAGivenRole()
        {
            Role cashierRole = CashierRole.GetInstance();
            Role adminRole = AdminRole.GetInstance();
            string userEmail = "BirdFriend@hotmail.com";
            string userName = "Holly";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            Assert.IsFalse(newUser.HasThisRole(adminRole));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullRole()
        {
            Role nullRole = null;
            string userEmail = "TheNuller2_0@hotmail.com";
            string userName = "Molly";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, nullRole);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NotRemoveLastRemainingRole()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "Mad_Cashier_404@ort.com.uy";
            string userName = "Raul";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            newUser.RemoveRole(cashierRole);
        }

        [TestMethod]
        public void NotAddDuplicateRoles()
        {
            Role firstCashierRole = CashierRole.GetInstance();
            Role secondCashierRole = CashierRole.GetInstance();
            string userEmail = "DoubleCASHBABY@ort.com.uy";
            string userName = "Riki";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, firstCashierRole);
            int roleAmountBeforeAddition = newUser.GetRoleCount();

            newUser.AddRole(secondCashierRole);

            Assert.AreEqual(roleAmountBeforeAddition, newUser.GetRoleCount());
        }

        [TestMethod]
        public void KnowIfItHasACertainPrivilege()
        {
            Role adminRole = AdminRole.GetInstance();
            string userEmail = "DoubleCASHBABY@ort.com.uy";
            string userName = "Riki";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, adminRole);

            ICollection<Privilege> adminPrivileges = adminRole.Privileges;
            Privilege firstPrivilege = adminPrivileges.ElementAt(FIRST_POSITION);

            Assert.IsTrue(newUser.HasPrivilege(firstPrivilege));
        }

        [TestMethod]
        public void BeAbleToAddRoles()
        {
            Role cashierRole = CashierRole.GetInstance();
            Role adminRole = AdminRole.GetInstance();
            string userEmail = "MoneyMoneyMoney@gmail.com";
            string userName = "Mani";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

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
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            newUser.AddRole(adminRole);
            newUser.RemoveRole(cashierRole);

            Assert.IsFalse(newUser.HasThisRole(cashierRole));
        }

        [TestMethod]
        public void HaveAPasswordWithAtLeastOneUppercaseLetterAndOneNumber()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            Assert.IsTrue(Hashing.VerifyHash(password, newUser.Salt, newUser.Password));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfPasswordDoesntHaveAtLeastOneUppercaseLetter()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "holacom1";
            User newUser = new User(userName, userEmail, password, cashierRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfPasswordDoesntHaveAtLeastOneNumber()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "Holacoma";
            User newUser = new User(userName, userEmail, password, cashierRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfPasswordIsWhiteSpace()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "    ";
            User newUser = new User(userName, userEmail, password, cashierRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfPasswordIsNull()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = null;
            User newUser = new User(userName, userEmail, password, cashierRole);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfPasswordIsLessThan8CharactersLong()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCo1";
            User newUser = new User(userName, userEmail, password, cashierRole);
        }

        [TestMethod]
        public void BeEqualToAnotherUserIfIDsAreEqual()
        {
            string firstUserName = "Mike Aruba";
            string firstEmail = "FunnyJoke@gmail.com";
            string firstpassword = "BatmanPassTheMus1ardPl3ase";
            string secondUserName = "RGB";
            string secondEmail = "Scart@hotmail.com";
            string secondPassword = "#CompositeHater123";
            User firstUser = new User(firstUserName, firstEmail, firstpassword, CashierRole.GetInstance());
            User secondUser = new User(secondUserName, secondEmail, secondPassword, AdminRole.GetInstance());
            int sameId = 1;

            firstUser.ID = sameId;
            secondUser.ID = sameId;

            Assert.AreEqual(firstUser, secondUser);
        }

        [TestMethod]
        public void BeEqualToAnotherUserIfEmailsAreEqual()
        {
            string equalUserEmail = "LastOneOutOfBeachCity@gmail.com";
            string firstUserName = "Mike Krol";
            string firstpassword = "Wabadabadubdub1";
            string secondUserName = "I Hate Jazz";
            string secondPassword = "HeHasABandCamp1";
            User firstUser = new User(firstUserName, equalUserEmail, firstpassword, CashierRole.GetInstance());
            User secondUser = new User(secondUserName, equalUserEmail, secondPassword, AdminRole.GetInstance());

            Assert.AreEqual(firstUser, secondUser);
        }

        [TestMethod]
        public void BeAbleToTellItHasAPassword()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            Assert.IsTrue(newUser.HasPassword());
        }

        [TestMethod]
        public void BeAbleToTellItHasASalt()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            Assert.IsTrue(newUser.HasSalt());
        }

        [TestMethod]
        public void BeAbleToClearItsSalt()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            newUser.ClearSalt();

            Assert.IsNull(newUser.Salt);
        }

        [TestMethod]
        public void BeAbleToClearItsPassword()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            newUser.ClearPassword();

            Assert.IsNull(newUser.Password);
        }

        [TestMethod]
        public void BeAbleToCloneItselfAndReturnTheCloneWithoutPassowordOrSalt()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            User clonedUser = newUser.CloneAndReturnNewUserWithoutPasswordAndSalt();

            Assert.IsNull(clonedUser.Password);
            Assert.IsNull(clonedUser.Salt);
            Assert.AreEqual(newUser, clonedUser);
        }

        [TestMethod]
        public void BeAbleToReturnAllOfItRolesInAStringEnumerable()
        {
            Role cashierRole = CashierRole.GetInstance();
            string userEmail = "LeUser@gmail.com";
            string userName = "Andrea";
            string password = "HolaCom1";
            User newUser = new User(userName, userEmail, password, cashierRole);

            IEnumerable<string> expectedRoles = new List<string> { CashierRole.GetInstance().Name};
            IEnumerable<string> resultingRoles = newUser.GetRoles();

            CollectionAssert.AreEqual((ICollection)expectedRoles, (ICollection)resultingRoles);
        }
    }
}
