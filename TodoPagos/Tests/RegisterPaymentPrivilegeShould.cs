using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserAPI;

namespace Tests
{
    [TestClass]
    public class RegisterPaymentPrivilegeShould
    {
        [TestMethod]
        public void HaveItsNamesHashCode()
        {
            Privilege registerPaymentPrivilege = RegisterPaymentPrivilege.GetInstance();
            int registerPaymentPrivilegeHashCode = registerPaymentPrivilege.GetHashCode();

            Assert.AreEqual(registerPaymentPrivilegeHashCode, registerPaymentPrivilege.Name.GetHashCode());
        }

        [TestMethod]
        public void ReturnFalseWhenComparedWithANonRoleObject()
        {
            Privilege registerPaymentPrivilege = RegisterPaymentPrivilege.GetInstance();
            string nonRoleObject = "Goodbye World!";

            Assert.AreNotEqual(registerPaymentPrivilege, nonRoleObject);
        }
    }
}
