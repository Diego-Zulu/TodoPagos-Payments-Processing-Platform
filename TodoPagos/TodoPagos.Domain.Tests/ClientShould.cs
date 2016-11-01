using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class ClientShould
    {
        [TestMethod]
        public void RecieveNameIDCardAndPhoneAtCreation()
        {
            string name = "Diego Zuluaga";
            int idCard = 49018830;
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfNameIsNullOrWhitespaceOnCreation()
        {
            string name = null;
            int idCard = 49018830;
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfIDCardIsNotValid()
        {
            string name = "Diego Zuluaga";
            int idCard = 49018830;
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);
        }
    }
}
