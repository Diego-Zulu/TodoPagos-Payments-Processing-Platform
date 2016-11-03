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
            string idCard = "49018830";
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfNameIsNullOrWhitespaceOnCreation()
        {
            string name = null;
            string idCard = "49018830";
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfIDCardIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018834";
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfPhoneNumberIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            int phone = 1;

            Client newClient = new Client(name, idCard, phone);
        }

        [TestMethod]
        public void BeCreatedWith0Points()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);

            Assert.AreEqual(0, newClient.Points);
        }

        [TestMethod]
        public void BeEqualToAnotherOneWithEqualIDCard()
        {
            string firstName = "Diego Zuluaga";
            int firstPhone = 26666666;
            string secondName = "Bruno Ferrari";
            int secondPhone = 25555555;
            string idCard = "49018830";

            Client firstNewClient = new Client(firstName, idCard, firstPhone);
            Client secondNewClient = new Client(secondName, idCard, secondPhone);

            Assert.AreEqual(firstNewClient, secondNewClient);
        }

        [TestMethod]
        public void BeEqualToAnotherOneWithEqualID()
        {
            string firstName = "Diego Zuluaga";
            string firstIDCard = "49018830";
            int firstPhone = 26666666;
            string secondName = "Bruno Ferrari";
            string secondIDCard = "12345672";
            int secondPhone = 25555555;
            

            Client firstNewClient = new Client(firstName, firstIDCard, firstPhone);
            Client secondNewClient = new Client(secondName, secondIDCard, secondPhone);

            firstNewClient.ID = secondNewClient.ID;

            Assert.AreEqual(firstNewClient, secondNewClient);
        }

        [TestMethod]
        public void HaveIDCardHashCode()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            int phone = 26666666;

            Client newClient = new Client(name, idCard, phone);

            Assert.AreEqual(idCard.GetHashCode(), newClient.GetHashCode());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedNameIsNullOrWhiteSpace()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            int phone = 26666666;
            string newName = "";

            Client newClient = new Client(name, idCard, phone);

            newClient.UpdateName(newName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedIDCardIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            int phone = 26666666;
            string newIDCard = "49018834";

            Client newClient = new Client(name, idCard, phone);

            newClient.UpdateIDCard(newIDCard);
        }
    }
}
