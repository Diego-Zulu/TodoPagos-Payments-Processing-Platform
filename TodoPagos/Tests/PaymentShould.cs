﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PaymentShould
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveANullPaymentMethod()
        {
            PayMethod paymentMethod = null;
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);

            Provider provider = new Provider("Antel", 20, emptyFieldList);

            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 10000;
            Receipt receipt = new Receipt(provider, completeFieldList, amount);

            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(receipt);

            int amountPayed = 500;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HavePositiveAmountPayed()
        {
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            List<IField> emptyFieldList = new List<IField>();
            NumberField aNumberField = new NumberField("Numerito");
            emptyFieldList.Add(aNumberField);

            Provider provider = new Provider("Antel", 20, emptyFieldList);

            IField aCompleteNumberField = aNumberField.FillAndClone("1234");
            List<IField> completeFieldList = new List<IField>();
            completeFieldList.Add(aCompleteNumberField);
            double amount = 10000;
            Receipt receipt = new Receipt(provider, completeFieldList, amount);

            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(receipt);

            int amountPayed = -1000;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NotHaveNullReceipts()
        {
            int amountPayed = 500;
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            ICollection<Receipt> receipts = null;

            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void HaveMinimumOneReceipt()
        {
            PayMethod paymentMethod = new DebitPayMethod(DateTime.Now);
            List<Receipt> receipts = new List<Receipt>();

            int amountPayed = 7500;
            Payment newPayment = new Payment(paymentMethod, amountPayed, receipts);
        }
    }
}
