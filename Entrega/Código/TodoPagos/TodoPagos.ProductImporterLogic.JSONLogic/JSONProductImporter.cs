using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TodoPagos.ProductImporterLogic.JSONLogic
{
    public class JSONProductImporter : IProductImporter
    {
        private JSONProductImporterControl jsonControl;

        public JSONProductImporter()
        {
            jsonControl = new JSONProductImporterControl();
        }

        public UserControl GetUIForNeededAttributes()
        {
            return jsonControl;
        }

        public ICollection<Product> ImportProducts()
        {
            string path = jsonControl.GetJSONFilePath();

            return ImportProducts(path);
        }

        public ICollection<Product> ImportProducts(string filePath)
        {
            var json = System.IO.File.ReadAllText(filePath);
            dynamic parentObject = JObject.Parse(json);
            JArray productsJsonArray = (JArray)parentObject.Productos;

            return SearchJArrayAndTryToParseProducts(productsJsonArray);
        }

        private ICollection<Product> SearchJArrayAndTryToParseProducts(JArray productsJsonArray)
        {
            ICollection<Product> importedProducts = new List<Product>();

            foreach (dynamic jsonProduct in productsJsonArray)
            {
                try
                {
                    AddParsedProductFromJSONToImportedProductsIfCorrect(jsonProduct, importedProducts);
                }
                catch (Exception)
                {
                }
            }

            return importedProducts;
        }

        private void AddParsedProductFromJSONToImportedProductsIfCorrect(
            dynamic jsonProduct, ICollection<Product> importedProducts)
        {
            Product newProduct = ParseProductFromJSON(jsonProduct);

            if (newProduct.IsComplete())
            {
                importedProducts.Add(newProduct);
            }
        }

        private Product ParseProductFromJSON(dynamic jsonProduct)
        {
            Product newProduct = new Product();

            newProduct.Name = jsonProduct.Nombre;
            newProduct.Description = jsonProduct.Descripcion;
            newProduct.NeededPoints = jsonProduct.CantidadPuntos;
            newProduct.Stock = jsonProduct.Stock;

            return newProduct;
        }
    }
}
