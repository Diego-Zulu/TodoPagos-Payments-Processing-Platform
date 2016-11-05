using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.ProductImporterLogic
{
    public interface IProductImporter
    {
        UserControl GetUIForNeededAttributes();

        ICollection<Product> ImportProducts();
    }
}
