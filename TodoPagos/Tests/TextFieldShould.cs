using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TextFieldShould
    {
        [TestMethod]
        public void AllowToBeFilledTest()
        {
            TextField textField = new TextField();

            IField newTextField = textField.FillAndClone("hola");
            string expectedResult = "hola";

            Assert.AreEqual(expectedResult, newTextField.Data);
        }
    }
}
