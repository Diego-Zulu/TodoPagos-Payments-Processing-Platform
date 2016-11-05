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
            throw new NotImplementedException();
        }

        public ICollection<Product> ImportProducts(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
