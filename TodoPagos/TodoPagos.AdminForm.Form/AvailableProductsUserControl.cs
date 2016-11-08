using System.Collections.Generic;
using System.Windows.Forms;
using TodoPagos.Domain.Repository;
using TodoPagos.ProductImporterLogic;

namespace TodoPagos.AdminForm.Form
{
    public partial class AvailableProductsUserControl : UserControl
    {
        private IUnitOfWork unitOfWork;

        public AvailableProductsUserControl(IUnitOfWork aUnitOfWork)
        {
            InitializeComponent();
            unitOfWork = aUnitOfWork;
            LoadProductsList();
        }

        private void LoadProductsList()
        {
            IEnumerable<Product> allProducts = unitOfWork.ProductsRepository.Get(null, null, "");
            foreach(Product product in allProducts)
            {
                lstActualAvailableProducts.Items.Add(product);
            }
        }
    }
}
