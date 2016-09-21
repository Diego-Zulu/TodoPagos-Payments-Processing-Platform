using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class RoleShould
    {
        [TestMethod]
        public void NotCreateMultipleInstancesOfSameRole()
        {
            Role firstCashierRole = CashierRole.GetInstance();
            Role secondCashierRole = CashierRole.GetInstance();

            Assert.AreSame(firstCashierRole, secondCashierRole);
        }
    }
}
