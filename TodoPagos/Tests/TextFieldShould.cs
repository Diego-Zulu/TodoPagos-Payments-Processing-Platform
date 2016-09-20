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

            string expectedResult = "";

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
    }
}
