﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TodoPagos.Domain;
using System.Linq;
using System.Collections.Generic;

namespace TodoPagos.Domain.Tests
{
    [TestClass]
    public class ProviderShould
    {
        [TestMethod]
        public void BeAbleToModifyHisCommission()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("CI");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);

            provider.ChangeCommission(15);

            Assert.AreEqual(15, provider.Commission);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenChangingCommissionToANegativeValue()
        {
            List<IField> list = new List<IField>();
            TextField aTextField = new TextField("Nombre");
            list.Add(aTextField);
            Provider provider = new Provider("Antel", 20, list);

            provider.ChangeCommission(-15);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenCreatingNewInstanceWithNegativeCommissionValue()
        {
            List<IField> list = new List<IField>();
            TextField aTextField = new TextField("Apellido");
            list.Add(aTextField);
            Provider provider = new Provider("Antel", -20, list);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenCreatingNewInstanceWithMoreThan100CommissionValue()
        {
            List<IField> list = new List<IField>();
            TextField aTextField = new TextField("Apellido");
            list.Add(aTextField);
            Provider provider = new Provider("Antel", 101, list);
        }

        [TestMethod]
        public void BeAbleToAddNewFields()
        {
            List<IField> list = new List<IField>();
            TextField aTextField = new TextField("Ciudad");
            list.Add(aTextField);
            Provider provider = new Provider("Antel", 20, list);
            NumberField numericField = new NumberField("Monto");

            provider.AddField(numericField);

            Assert.IsTrue(provider.ContainsField(numericField));
        }

        [TestMethod]
        public void BeAbleToRemoveAField()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Fecha");
            list.Add(aDateField);
            Provider provider = new Provider("Antel", 20, list);
            NumberField numericField = new NumberField("Monto");

            provider.AddField(numericField);
            provider.RemoveField(numericField);

            Assert.IsFalse(provider.ContainsField(numericField));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfUserTriesToRemoveAFieldThatIsNotInFieldsList()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Vencimiento");
            list.Add(aDateField);
            Provider provider = new Provider("Antel", 20, list);
            NumberField numericField = new NumberField("Monto");
            DateField dateField = new DateField("Fecha");

            provider.AddField(numericField);
            provider.RemoveField(dateField);
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherProvider()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Vencimiento");
            list.Add(aDateField);
            Provider firstProvider = new Provider("Antel", 20, list);
            Provider secondProvider = new Provider("Antel", 10, list);

            Assert.IsTrue(firstProvider.Equals(secondProvider));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObject()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("RUT");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);

            Assert.IsFalse(provider.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfObject()
        {
            List<IField> list = new List<IField>();
            NumberField aNumberField = new NumberField("ID");
            list.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, list);

            DateField aDateField = new DateField("Fecha");

            Assert.IsFalse(provider.Equals(aDateField));
        }

        [TestMethod]
        public void GiveTheOptionToBeCreatedWithAnExistingFieldsList()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Fecha");
            list.Add(aDateField);

            Provider provider = new Provider("Antel", 20, list);

            Assert.AreEqual(list, provider.Fields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfFieldsListIsNull()
        {
            Provider provider = new Provider("Antel", 20, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfFieldsListHasCompletedField()
        {
            NumberField aNumberField = new NumberField("ID");
            IField completedNumberField = aNumberField.FillAndClone("8000");
            List<IField> completedFields = new List<IField>();
            completedFields.Add(completedNumberField);

            Provider provider = new Provider("Antel", 20, completedFields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfNameIsWhitespace()
        {
            NumberField aNumberField = new NumberField("ID");
            List<IField> fields = new List<IField>();
            fields.Add(aNumberField);

            Provider provider = new Provider("     ", 20, fields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailIfNameIsNull()
        {
            NumberField aNumberField = new NumberField("ID");
            List<IField> fields = new List<IField>();
            fields.Add(aNumberField);

            Provider provider = new Provider(null, 20, fields);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailOnCommissionChangeToMoreThan100Percent()
        {
            NumberField aNumberField = new NumberField("ID");
            List<IField> fields = new List<IField>();
            fields.Add(aNumberField);
            Provider provider = new Provider("Antel", 20, fields);

            provider.ChangeCommission(101);
        }

        [TestMethod]
        public void BeAbleToMarkItselfAsDeleted()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Fecha");
            list.Add(aDateField);

            Provider provider = new Provider("Antel", 20, list);
            provider.MarkAsInactiveToShowItIsDeleted();

            Assert.IsFalse(provider.Active);
        }

        [TestMethod]
        public void BeAbleToTellItsEqualToAnotherProviderById()
        {
            Provider firstProvider = new Provider("Antel", 20, new List<IField>());
            Provider secondProvider = new Provider("Antel", 20, new List<IField>());
            firstProvider.ID = secondProvider.ID + 1;

            Assert.AreEqual(firstProvider, secondProvider);
        }

        [TestMethod]
        public void BeAbleToTellItsEqualToAnotherProviderByName()
        {
            Provider firstProvider = new Provider("Antel", 20, new List<IField>());
            Provider secondProvider = new Provider("Antitel", 20, new List<IField>());
            firstProvider.ID = secondProvider.ID;

            Assert.AreEqual(firstProvider, secondProvider);
        }

        [TestMethod]
        public void BeAbleToTellTwoProvidersAreCompletelyEqual()
        {
            List<IField> list = new List<IField>();
            DateField aDateField = new DateField("Vencimiento");
            list.Add(aDateField);
            Provider firstProvider = new Provider("Antel", 10, list);
            Provider secondProvider = new Provider("Antel", 10, list);

            Assert.IsTrue(firstProvider.IsCompletelyEqualTo(secondProvider));
        }

        [TestMethod]
        public void BeAbleToTellTwoProvidersWithDifferentListsAreNotCompletelyEqual()
        {
            List<IField> firstList = new List<IField>();
            DateField aDateField = new DateField("Vencimiento");
            firstList.Add(aDateField);
            List<IField> secondList = new List<IField>();
            NumberField aNumberField = new NumberField("Vencimiento");
            secondList.Add(aNumberField);
            Provider firstProvider = new Provider("Antel", 10, firstList);
            Provider secondProvider = new Provider("Antel", 10, secondList);

            Assert.IsFalse(firstProvider.IsCompletelyEqualTo(secondProvider));
        }
    }
}
