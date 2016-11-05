using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class LogEntryShould
    {
        [TestMethod]
        public void HaveAnActionType()
        {
            string userEmail = "bferr42@gmail.com";
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN, userEmail);

            Assert.IsNotNull(newLogEntry.Action);
        }

        [TestMethod]
        public void HaveAnOcurrenceDate()
        {
            string userEmail = "bferr42@gmail.com";
            LogEntry newLogEntry = new LogEntry(ActionType.PRODUCT_LOAD, userEmail);

            Assert.IsNotNull(newLogEntry.Date);
        }

        [TestMethod]
        public void HaveTheRelatedUserEmail()
        {
            string userEmail = "bferr42@gmail.com";
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN, userEmail);

            Assert.IsNotNull(newLogEntry.UserEmail);
        }
    }
}
