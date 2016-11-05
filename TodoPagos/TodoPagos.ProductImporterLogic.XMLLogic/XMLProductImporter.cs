using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            throw new NotImplementedException();
        }
    }
}
