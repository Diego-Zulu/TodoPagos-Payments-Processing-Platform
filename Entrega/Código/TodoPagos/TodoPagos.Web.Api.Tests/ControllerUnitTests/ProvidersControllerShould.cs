﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TodoPagos.Web.Api.Controllers;
using TodoPagos.Web.Services;
using TodoPagos.Domain;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;

namespace TodoPagos.Web.Api.Tests.ControllerUnitTests
{
    [TestClass]
    public class ProvidersControllerShould
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FailWithNullReferenceExceptionWhenNoUserIsLogedIn()
        {
            ProvidersController controller = new ProvidersController();
        }

        [TestMethod]
        public void ReceiveAProviderServiceOnCreation()
        {
            var mockProviderService = new Mock<IProviderService>();

            ProvidersController controller = new ProvidersController(mockProviderService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfProviderServiceIsNullOnCreation()
        {
            IProviderService nullProviderService = null;

            ProvidersController controller = new ProvidersController(nullProviderService);
        }

        [TestMethod]
        public void BeAbleToReturnAllProvidersInRepository()
        {
            var allProviders = new[]
            {
                new Provider("Antel", 10, new List<IField>()),
            new Provider("Devoto", 15, new List<IField>())
            };
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetAllProviders()).Returns(allProviders);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.GetProviders();
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;

            Assert.AreSame(contentResult.Content, allProviders);
        }

        [TestMethod]
        public void BeAbleToReturnAllActiveProvidersInRepository()
        {
            var allProviders = new[]
            {
                new Provider("Antel", 10, new List<IField>()),
            new Provider("Devoto", 15, new List<IField>())
            };
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetAllProvidersAcoordingToState(true)).Returns(allProviders);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            bool getActives = true;
            IHttpActionResult actionResult = controller.GetProviders(getActives);
            OkNegotiatedContentResult<IEnumerable<Provider>> contentResult = (OkNegotiatedContentResult<IEnumerable<Provider>>)actionResult;

            Assert.AreSame(contentResult.Content, allProviders);
        }

        [TestMethod]
        public void BeAbleToReturnASingleProviderFromRepository()
        {
            Provider singleProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetSingleProvider(singleProvider.ID)).Returns(singleProvider);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.GetProvider(singleProvider.ID);
            OkNegotiatedContentResult<Provider> contentResult = (OkNegotiatedContentResult<Provider>)actionResult;

            Assert.AreSame(contentResult.Content, singleProvider);
        }

        [TestMethod]
        public void FailWithNotFoundIfUserIdIsNotInRepository()
        {
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.GetSingleProvider(1)).Throws(new ArgumentOutOfRangeException());
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.GetProvider(1);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToUpdateUserInRepositoryAndReturnNoContent()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.UpdateProvider(oneProvider.ID, oneProvider, It.IsAny<string>())).Returns(true);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PutProvider(oneProvider.ID, oneProvider);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithBadRequestIfToBeUpdatedProviderIdAndSuppliedIdDontMatch()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.UpdateProvider(oneProvider.ID + 1, oneProvider, It.IsAny<string>())).Returns(false);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PutProvider(oneProvider.ID + 1, oneProvider);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfToBeUpdatedProviderIsNull()
        {
            Provider nullProvider = null;
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.UpdateProvider(1, nullProvider, It.IsAny<string>())).Returns(false);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PutProvider(1, nullProvider);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeUpdatedProviderIsNotInRepository()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.UpdateProvider(oneProvider.ID, oneProvider, It.IsAny<string>())).Returns(false);
            mockProviderService.Setup(x => x.GetSingleProvider(oneProvider.ID)).Throws(new ArgumentOutOfRangeException());
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PutProvider(oneProvider.ID, oneProvider);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void BeAbleToPostNewProviderIntoRepository()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.CreateProvider(oneProvider, It.IsAny<string>())).Returns(1);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PostProvider(oneProvider);
            CreatedAtRouteNegotiatedContentResult<Provider> contentResult = (CreatedAtRouteNegotiatedContentResult<Provider>)actionResult;

            Assert.AreSame(contentResult.Content, oneProvider);
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewProviderIsAlreadyInRepository()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.CreateProvider(oneProvider, It.IsAny<string>())).Throws(new InvalidOperationException());
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PostProvider(oneProvider);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void FailWithBadRequestIfPostedNewProviderIsNull()
        {
            Provider nullProvider = null;
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.CreateProvider(nullProvider, It.IsAny<string>())).Throws(new ArgumentNullException());
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.PostProvider(nullProvider);

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public void BeAbleToDeleteAProvider()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.MarkProviderAsDeleted(oneProvider.ID, It.IsAny<string>())).Returns(true);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.DeleteProvider(oneProvider.ID);
            StatusCodeResult contentResult = (StatusCodeResult)actionResult;

            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void FailWithNotFoundIfToBeDeletedProviderDoesntExistInRepository()
        {
            Provider oneProvider = new Provider("Antel", 10, new List<IField>());
            var mockProviderService = new Mock<IProviderService>();
            mockProviderService.Setup(x => x.MarkProviderAsDeleted(oneProvider.ID, It.IsAny<string>())).Returns(false);
            ProvidersController controller = new ProvidersController(mockProviderService.Object);

            IHttpActionResult actionResult = controller.DeleteProvider(oneProvider.ID);

            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
