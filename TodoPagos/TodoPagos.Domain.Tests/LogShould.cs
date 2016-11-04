using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class LogShould
    {
        [TestMethod]
        public void HaveAListOfAllLogEntries()
        {
            string userEmail = "bferr42@gmail.com";
            Log newLog = new Log();
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN, userEmail);
            List<LogEntry> allEntries = new List<LogEntry>(){ newLogEntry};

            newLog.AddEntry(newLogEntry);

            Assert.AreSame(allEntries, newLog.Entries);
        }
    }
}
