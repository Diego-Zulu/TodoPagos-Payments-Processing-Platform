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
            string phone = "26666666";
            string address = "1112 27th NE";

            Client newClient = new Client(name, idCard, phone, address);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfNameIsNullOrWhitespaceOnCreation()
        {
            string name = null;
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 27th NE";

            Client newClient = new Client(name, idCard, phone, address);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfIDCardIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018834";
            string phone = "26666666";
            string address = "1112 27th NE";

            Client newClient = new Client(name, idCard, phone, address);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfPhoneNumberIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "1";
            string address = "1112 27th NE";

            Client newClient = new Client(name, idCard, phone, address);
        }

        [TestMethod]
        public void BeCreatedWith0Points()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 27th NE";

            Client newClient = new Client(name, idCard, phone, address);

            Assert.AreEqual(0, newClient.Points);
        }

        [TestMethod]
        public void BeEqualToAnotherOneWithEqualIDCard()
        {
            string firstName = "Diego Zuluaga";
            string firstPhone = "26666666";
            string firstAddress = "1112 27th NE";
            string secondName = "Bruno Ferrari";
            string secondPhone = "36666666";
            string secondAddress = "1112 28th NE";
            string idCard = "49018830";

            Client firstNewClient = new Client(firstName, idCard, firstPhone, firstAddress);
            Client secondNewClient = new Client(secondName, idCard, secondPhone, secondAddress);

            Assert.AreEqual(firstNewClient, secondNewClient);
        }

        [TestMethod]
        public void BeEqualToAnotherOneWithEqualID()
        {
            string firstName = "Diego Zuluaga";
            string firstIDCard = "49018830";
            string firstPhone = "26666666";
            string firstAddress = "1112 27th NE";
            string secondName = "Bruno Ferrari";
            string secondIDCard = "12345672";
            string secondPhone = "26666665";
            string secondAddress = "1112 28th NE";


            Client firstNewClient = new Client(firstName, firstIDCard, firstPhone, firstAddress);
            Client secondNewClient = new Client(secondName, secondIDCard, secondPhone, secondAddress);

            firstNewClient.ID = secondNewClient.ID;

            Assert.AreEqual(firstNewClient, secondNewClient);
        }

        [TestMethod]
        public void HaveIDCardHashCode()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            Assert.AreEqual(idCard.GetHashCode(), newClient.GetHashCode());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedNameIsNullOrWhiteSpace()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string newName = "";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            newClient.UpdateName(newName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedIDCardIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string newIDCard = "49018834";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            newClient.UpdateIDCard(newIDCard);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeUpdatedPhoneNumberIsNotValid()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string newPhone = "8729663";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            newClient.UpdatePhone(newPhone);
        }

        [TestMethod]
        public void OnlyUpdateInfoFromRecievedClientThatIsComplete()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            Client updatedClient = new Client(name, idCard, phone, address);
            updatedClient.PhoneNumber = "";

            newClient.UpdateClientWithCompletedInfoFromTargetClient(updatedClient);

            Assert.AreEqual(idCard, newClient.IDCard);
            Assert.AreEqual(name, newClient.Name);
            Assert.AreEqual(phone, newClient.PhoneNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfUpdatedInfoClientIsNull()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            newClient.UpdateClientWithCompletedInfoFromTargetClient(null);
        }

        [TestMethod]
        public void BeAbleToKnowIfItIsComplete()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);
            newClient.Name = "";

            Assert.IsFalse(newClient.IsComplete());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfAddedPointsMakeTheOverallNegative()
        {
            string name = "Diego Zuluaga";
            string idCard = "49018830";
            string phone = "26666666";
            string address = "1112 28th NE";

            Client newClient = new Client(name, idCard, phone, address);

            newClient.AddPoints(-60);
        }
    }
}
