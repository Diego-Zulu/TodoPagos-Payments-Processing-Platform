using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests
{
    [TestClass]
    public class TextFieldShould
    {
        [TestMethod]
        public void BeAbleToReturnItsDataTest()
        {
            TextField textField = new TextField();

            textField.Data = "hola";
            string expectedResult = "hola";

            Assert.AreEqual(expectedResult, textField.GetData());
        }

        [TestMethod]
        public void AllowToBeFilledTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone("hola");
            string expectedResult = "hola";

            Assert.AreEqual(expectedResult, newTextField.GetData());
        }

        [TestMethod]
        public void ReturnANewTextFieldWhenFilledTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone("hola");

            Assert.AreNotSame(textField, newTextField);
        }

        [TestMethod]
        public void TellItIsValidWhenItStoresANotEmptyTextTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone("hola");

            Assert.IsTrue(newTextField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresAnEmptyTextTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone("");

            Assert.IsFalse(newTextField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresAFullWhiteSpaceTextTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone("      ");

            Assert.IsFalse(newTextField.IsValid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNullTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone(null);
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherTextFieldTest()
        {
            TextField firstTextField = new TextField();
            TextField secondTextField = new TextField();

            IField firstNewTextField = firstTextField.FillAndClone("hola");
            IField secondNewTextField = secondTextField.FillAndClone("hola");

            Assert.IsTrue(firstNewTextField.Equals(secondNewTextField));
        }
    }
}
