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
        public UserControl GetUIForNeededAttributes()
        {
            return new XMLProductImporterControl();
        }

        public ICollection<Product> ImportProducts()
        {
            throw new NotImplementedException();
        }
    }
}
