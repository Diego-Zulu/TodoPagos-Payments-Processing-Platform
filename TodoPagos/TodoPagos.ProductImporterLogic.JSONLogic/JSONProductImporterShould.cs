using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.ProductImporterLogic.JSONLogic
{
    [TestClass]
    public class JSONProductImporterShould
    {
        [TestMethod]
        public void BeAbleToReturnAUserControl()
        {
            JSONProductImporter importer = new JSONProductImporter();

            UserControl importerUserControl = importer.GetUIForNeededAttributes();

            Assert.IsNotNull(importerUserControl);
        }

        [TestMethod]
        public void BeAbleToReturnImportedProducts()
        {
            JSONProductImporter importer = new JSONProductImporter();

            ICollection<Product> testProducts = importer.ImportProducts("products.json");

            Assert.IsNotNull(testProducts);
            Assert.IsFalse(testProducts.Count == 0);
        }
    }
}
