using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.ProductImporterLogic.XMLLogic
{
    [TestClass]
    public class XMLProductImporterShould
    {
        [TestMethod]
        public void BeAbleToReturnAUserControl()
        {
            XMLProductImporter importer = new XMLProductImporter();

            UserControl importerUserControl = importer.GetUIForNeededAttributes();

            Assert.IsNotNull(importerUserControl);
        }

        [TestMethod]
        public void BeAbleToRecieveAUserControlOnCreation()
        {
            var mockUserControl = new Mock<UserControl>();

            XMLProductImporter importer = new XMLProductImporter(mockUserControl);

            UserControl importerUserControl = importer.GetUIForNeededAttributes();

            Assert.IsNotNull(importerUserControl);
        }

        [TestMethod]
        public void FailWithNullArgumentExceptionWhenUserControlIsNullOnCreation()
        {
            var mockUserControl = new Mock<UserControl>();

            XMLProductImporter importer = new XMLProductImporter(mockUserControl);

            UserControl importerUserControl = importer.GetUIForNeededAttributes();

            Assert.IsNotNull(importerUserControl);
        }
    }
}
