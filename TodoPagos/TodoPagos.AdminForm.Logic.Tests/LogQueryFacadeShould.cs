using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;
using Domain;
using System.Collections.Generic;
using System.Collections;

namespace TodoPagos.AdminForm.Logic.Tests
{
    [TestClass]
    public class LogQueryFacadeShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfILogStrategyIsNullOnCreation()
        {
            ILogStrategy strategy = null;

            LogQueryFacade facade = new LogQueryFacade(strategy);
        }

        [TestMethod]
        public void BeAbleToReturnAllLogEntriesBetweenTwoGivenDates()
        {
            var mockStrategy = new Mock<ILogStrategy>();
            LogQueryFacade facade = new LogQueryFacade(mockStrategy.Object);
            LogEntry entry = new LogEntry(ActionType.LOGIN, "hola@hola.com");
            ICollection<LogEntry> allEntries = new List<LogEntry>() { entry };
            mockStrategy.Setup(u => u.GetEntries(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(allEntries);

            ICollection<LogEntry> returnedEntries = facade.GetEntries(DateTime.MinValue, DateTime.MaxValue);

            mockStrategy.VerifyAll();
            CollectionAssert.AreEqual((ICollection)allEntries, (ICollection)returnedEntries);
        }
    }
}
