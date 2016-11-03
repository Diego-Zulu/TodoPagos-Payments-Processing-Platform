using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PointsManagerShould
    {
        [TestMethod]
        public void HaveEmptyBlackListAnd150MoneyToPointsConversion()
        {
            PointsManager newPointsManager = PointsManager.GetInstance();

            Assert.AreEqual(150, newPointsManager.MoneyPerPoint);
            Assert.AreEqual(0, newPointsManager.Blacklist.Count);
        }

        [TestMethod]
        public void AlwaysHaveOnlyOneInstance()
        {
            PointsManager firstPointsManager = PointsManager.GetInstance();
            PointsManager secondPointsManager = PointsManager.GetInstance();

            Assert.AreSame(firstPointsManager, secondPointsManager);
        }

        [TestMethod]
        public void BeAbleToAddProvidersWithoutRepetitionToBlacklist()
        {
            PointsManager newPointsManager = PointsManager.GetInstance();

            Provider newProvider = new Provider("Antel", 10, new List<IField>());
            Provider repeatedProvider = new Provider("Antel", 10, new List<IField>());

            newPointsManager.AddProviderToBlacklist(newProvider);
            newPointsManager.AddProviderToBlacklist(repeatedProvider);

            Assert.AreEqual(1, newPointsManager.Blacklist.Count);
        }

        [TestMethod]
        public void BeAbleToRemoveABlacklistedProvider()
        {
            PointsManager newPointsManager = PointsManager.GetInstance();

            Provider newProvider = new Provider("Antel", 10, new List<IField>());

            newPointsManager.AddProviderToBlacklist(newProvider);
            newPointsManager.RemoveProviderFromBlacklist(newProvider);

            Assert.AreEqual(0, newPointsManager.Blacklist.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeRemovedBlacklistedProviderIsNotInBlacklist()
        {
            PointsManager newPointsManager = PointsManager.GetInstance();

            Provider newProvider = new Provider("Antel", 10, new List<IField>());

            newPointsManager.RemoveProviderFromBlacklist(newProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWithArgumentExceptionIfToBeChangedMoneyPerPointIsNegative()
        {
            PointsManager newPointsManager = PointsManager.GetInstance();

            newPointsManager.ChangeMoneyPerPointRatio(-1);
        }

        [TestMethod]
        public void BeAbleToAddPointsToClientIfProviderIsNotBlacklisted()
        {
            PointsManager newPointsManager = PointsManager.GetInstance();
            Client newClient = new Client("Diego", "49018830", "26666666");
            Provider blacklistedProvider = new Provider("Antel", 10, new List<IField>());
            double paidMoney = 200;

            bool couldAdd = newPointsManager.AddPointsToClientIfProviderIsNotBlacklisted(
                paidMoney, newClient, blacklistedProvider);

            Assert.IsTrue(couldAdd);
        }
    }
}
