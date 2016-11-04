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
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN);

            Assert.IsNotNull(newLogEntry.Action);
        }

        [TestMethod]
        public void HaveAnOcurrenceDate()
        {
            LogEntry newLogEntry = new LogEntry(ActionType.PRODUCT_LOAD);

            Assert.IsNotNull(newLogEntry.Date);
        }
    }
}
