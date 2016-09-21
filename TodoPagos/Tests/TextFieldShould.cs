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
            TextField textField = new TextField("Nombre");

            textField.Data = "hola";
            string expectedResult = "hola";

            Assert.AreEqual(expectedResult, textField.GetData());
        }

        [TestMethod]
        public void AllowToBeFilledTest()
        {
            TextField textField = new TextField("Nombre");

            IField newTextField = textField.FillAndClone("hola");
            string expectedResult = "hola";

            Assert.AreEqual(expectedResult, newTextField.GetData());
        }

        [TestMethod]
        public void ReturnANewTextFieldWhenFilledTest()
        {
            TextField textField = new TextField("Nombre");

            IField newTextField = textField.FillAndClone("hola");

            Assert.AreNotSame(textField, newTextField);
        }

        [TestMethod]
        public void TellItIsValidWhenItStoresANotEmptyTextTest()
        {
            TextField textField = new TextField("Nombre");

            IField newTextField = textField.FillAndClone("hola");

            Assert.IsTrue(newTextField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresAnEmptyTextTest()
        {
            TextField textField = new TextField("Nombre");

            IField newTextField = textField.FillAndClone("");

            Assert.IsFalse(newTextField.IsValid());
        }

        [TestMethod]
        public void TellItIsNotValidWhenItStoresAFullWhiteSpaceTextTest()
        {
            TextField textField = new TextField("Nombre");

            IField newTextField = textField.FillAndClone("      ");

            Assert.IsFalse(newTextField.IsValid());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FailWhenDataToBeFilledWithIsNullTest()
        {
            TextField textField = new TextField("Nombre");

            IField newTextField = textField.FillAndClone(null);
        }

        [TestMethod]
        public void BeAbleToTellIfItIsEqualToAnotherTextFieldTest()
        {
            TextField firstTextField = new TextField("Nombre");
            TextField secondTextField = new TextField("Nombre");

            IField firstNewTextField = firstTextField.FillAndClone("hola");
            IField secondNewTextField = secondTextField.FillAndClone("hola");

            Assert.IsTrue(firstNewTextField.Equals(secondNewTextField));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToANullObjectTest()
        {
            TextField firstTextField = new TextField("Nombre");

            IField firstNewTextField = firstTextField.FillAndClone("hola");

            Assert.IsFalse(firstNewTextField.Equals(null));
        }

        [TestMethod]
        public void BeAbleToTellItIsNotEqualToAnotherTypeOfFieldTest()
        {
            TextField aTextField = new TextField("Nombre");

            DateField aDateField = new DateField("Fecha");

            Assert.IsFalse(aTextField.Equals(aDateField));
        }
    }
}
