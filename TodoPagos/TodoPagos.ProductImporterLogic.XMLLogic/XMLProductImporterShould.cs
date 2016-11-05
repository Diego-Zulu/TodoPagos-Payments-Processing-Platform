using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
