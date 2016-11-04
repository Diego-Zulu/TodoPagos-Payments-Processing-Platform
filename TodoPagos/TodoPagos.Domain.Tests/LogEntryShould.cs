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
    }
}
