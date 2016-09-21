using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class RoleShould
    {
        const int FIRST_POSITION = 0;

        [TestMethod]
        public void NotCreateMultipleInstancesOfSameRole()
        {
            Role firstCashierRole = CashierRole.GetInstance();
            Role secondCashierRole = CashierRole.GetInstance();

            Assert.AreSame(firstCashierRole, secondCashierRole);
        }

        [TestMethod]
        public void KnowIfItHasACertainPrivilege()
        {
            Role cashierRole = CashierRole.GetInstance();

            ICollection<Privilege> cashierPrivileges = cashierRole.Privileges;
            Privilege firstPrivilege = cashierPrivileges.ElementAt(FIRST_POSITION);

            Assert.IsTrue(cashierRole.HasPrivilege(firstPrivilege));
        }
    }
}
