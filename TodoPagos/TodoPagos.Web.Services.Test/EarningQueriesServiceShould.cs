using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain.Repository;
using Moq;

namespace TodoPagos.Web.Services.Test
{
    [TestClass]
    public class EarningQueriesServiceShould
    {
        [TestMethod]
        public void ReceiveAUnitOfWorkOnCreation()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            EarningQueriesService service = new EarningQueriesService(mockUnitOfWork.Object);
        }
    }
}
