using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;
using System.Collections;

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
            ICollection<LogEntry> allEntries = new List<LogEntry>(){ newLogEntry};

            newLog.AddEntry(newLogEntry);

            CollectionAssert.AreEqual((ICollection)allEntries, (ICollection)newLog.Entries);
        }

        [TestMethod]
        public void BeAbleToReturnAllEntriesBetweenTwoDates()
        {
            string userEmail = "bferr42@gmail.com";
            Log newLog = new Log();
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN, userEmail);
            ICollection<LogEntry> allEntries = new List<LogEntry>() { newLogEntry };

            newLog.AddEntry(newLogEntry);
            ICollection<LogEntry> returnedEntries = newLog.CheckLogBetweenDates(DateTime.MinValue, DateTime.MaxValue);

            CollectionAssert.AreEqual((ICollection)allEntries, (ICollection)returnedEntries);
        }
    }
}
