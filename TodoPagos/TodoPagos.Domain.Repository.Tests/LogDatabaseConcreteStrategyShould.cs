using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;
using System.Collections.Generic;
using System.Collections;
using TodoPagos.Domain.Repository;
using Moq;

namespace Tests
{
    [TestClass]
    public class LogShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUnitOfWorkIsNullOnCreation()
        {
            IUnitOfWork unitOfWork = null;
            LogDatabaseConcreteStrategy newLog = new LogDatabaseConcreteStrategy(unitOfWork);
        }

        [TestMethod]
        public void BeAbleToSaveALogEntry()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            string userEmail = "bferr42@gmail.com";
            LogDatabaseConcreteStrategy newLog = new LogDatabaseConcreteStrategy(mockUnitOfWork.Object);
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN, userEmail);
            ICollection<LogEntry> allEntries = new List<LogEntry>(){ newLogEntry};
            mockUnitOfWork.Setup(u => u.EntriesRepository.Insert(It.IsAny<LogEntry>()));
            mockUnitOfWork.Setup(u => u.Save());
            mockUnitOfWork.Setup(u => u.EntriesRepository.Get(null, null, "")).Returns(allEntries);

            newLog.SaveEntry(newLogEntry);

            CollectionAssert.AreEqual((ICollection)allEntries, (ICollection)newLog.GetEntries(DateTime.MinValue, DateTime.MaxValue));
            mockUnitOfWork.VerifyAll();
        }

        [TestMethod]
        public void BeAbleToReturnAllEntriesBetweenTwoDates()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            string userEmail = "bferr42@gmail.com";
            LogDatabaseConcreteStrategy newLog = new LogDatabaseConcreteStrategy(mockUnitOfWork.Object);
            LogEntry newLogEntry = new LogEntry(ActionType.LOGIN, userEmail);
            ICollection<LogEntry> allEntries = new List<LogEntry>() { newLogEntry };
            mockUnitOfWork.Setup(u => u.EntriesRepository.Insert(It.IsAny<LogEntry>()));
            mockUnitOfWork.Setup(u => u.Save());
            mockUnitOfWork.Setup(u => u.EntriesRepository.Get(null, null, "")).Returns(allEntries);

            newLog.SaveEntry(newLogEntry);
            ICollection<LogEntry> returnedEntries = newLog.GetEntries(DateTime.MinValue, DateTime.MaxValue);

            CollectionAssert.AreEqual((ICollection)allEntries, (ICollection)returnedEntries);
            mockUnitOfWork.VerifyAll();
        }
    }
}
