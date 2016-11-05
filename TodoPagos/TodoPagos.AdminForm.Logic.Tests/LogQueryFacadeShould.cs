using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;

namespace TodoPagos.AdminForm.Logic.Tests
{
    [TestClass]
    public class LogQueryFacadeShould
    {
        [TestMethod]
        public void FailIfILogStrategyIsNullOnCreation()
        {
            ILogStrategy strategy = null;

            LogQueryFacade facade = new LogQueryFacade(strategy);
        }
    }
}
