using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TodoPagos.ProductImporterLogic.XMLLogic
{
    public class XMLProductImporter : IProductImporter
    {
        private XMLProductImporterControl xmlControl;

        public XMLProductImporter()
        {
            xmlControl = new XMLProductImporterControl();
        }

        public UserControl GetUIForNeededAttributes()
        {
            return xmlControl;
        }

        public ICollection<Product> ImportProducts()
        {
           string filePath = xmlControl.GetXMLFilePath();

            return ImportProducts(filePath);
        }

        public ICollection<Product> ImportProducts(string filePath)
        {
            
            XmlDocument xmlProductList = new XmlDocument();
            xmlProductList.Load(filePath);

            XmlNodeList productListNodes = xmlProductList.SelectSingleNode("Productos").ChildNodes;

            return SearchXMLProductsListAndTryToImportProducts(productListNodes);
        }

        private ICollection<Product> SearchXMLProductsListAndTryToImportProducts(XmlNodeList productListNodes)
        {
            ICollection<Product> importedProducts = new List<Product>();
            foreach (XmlNode productNode in productListNodes)
            {
                try
                {
                    AddProductToImportedProductsIfParseWasSuccessful(productNode, importedProducts);
                }
                catch (NullReferenceException)
                {

                }
            }
            return importedProducts;
        }

        private void AddProductToImportedProductsIfParseWasSuccessful(XmlNode productNode, ICollection<Product> importedProducts)
        {
            Product newProduct = new Product();
            AddNameToProduct(productNode, newProduct);
            AddDescriptionToProduct(productNode, newProduct);
            bool parsedNeededPoints = TryToAddNeededPointsToProduct(productNode, newProduct);
            bool parsedStock = TryToAddStockToProduct(productNode, newProduct);

            if (parsedStock && parsedNeededPoints && newProduct.IsComplete())
            {
                importedProducts.Add(newProduct);
            }
        }

        private void AddNameToProduct(XmlNode productNode, Product targetProduct)
        {
            string productName = productNode.Attributes["Nombre"].Value;
            targetProduct.Name = productName;
        }

        private void AddDescriptionToProduct(XmlNode productNode, Product targetProduct)
        {
            string productDescription = productNode.SelectSingleNode("Descripcion").InnerText;
            targetProduct.Description = productDescription;
        }

        private bool TryToAddNeededPointsToProduct(XmlNode productNode, Product targetProduct)
        {
            int productNeededPoints;
            bool parsedNeededPoints = int.TryParse(
                productNode.SelectSingleNode("CantidadPuntos").InnerText, out productNeededPoints);

            if (parsedNeededPoints)
            {
                targetProduct.NeededPoints = productNeededPoints;
                return true;
            }
            return false;
        }

        private bool TryToAddStockToProduct(XmlNode productNode, Product targetProduct)
        {
            int productStock;
            bool parsedStock = int.TryParse(
                productNode.SelectSingleNode("Stock").InnerText, out productStock);

            if (parsedStock)
            {
                targetProduct.Stock = productStock;
                return true;
            }
            return false;
        }
    }
}
