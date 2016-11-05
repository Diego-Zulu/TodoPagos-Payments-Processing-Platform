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
            ICollection<Product> importedProducts = new List<Product>();

            var json = System.IO.File.ReadAllText(filePath);

            dynamic parentObject = JObject.Parse(json);

            JArray productsJsonArray = (JArray)parentObject.Productos;

            foreach (dynamic jsonProduct in productsJsonArray)
            {
                try
                {
                    Product newProduct = new Product();
                    string productName = jsonProduct.Nombre;
                    string productDescription = jsonProduct.Descripcion;
                    int productNeededPoints = jsonProduct.CantidadPuntos;
                    int productStock = jsonProduct.Stock;

                    newProduct.Name = productName;
                    newProduct.Description = productDescription;
                    newProduct.NeededPoints = productNeededPoints;
                    newProduct.Stock = productStock;

                    if (newProduct.IsComplete())
                    {
                        importedProducts.Add(newProduct);
                    }

                }
                catch (Exception)
                {

                }
            }

            return importedProducts;
        }
    }
}
