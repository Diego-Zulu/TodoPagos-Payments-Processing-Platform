using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoPagos.ProductImporterLogic;
using TodoPagos.AdminForm.Form;
using TodoPagos.Domain.Repository;
using TodoPagos.AdminForm.Logic;
using TodoPagos.Domain;

namespace AdminUserInterface
{
    public partial class LoadNewProductsAcceptedUserControl : UserControl
    {

        private IProductImporter productImporter;
        private IUnitOfWork unitOfWork;
        private ILogStrategy logStrategy;
        private ProductFacade productFacade;
        private string signedInUserEmail;

        public LoadNewProductsAcceptedUserControl(IProductImporter aProductImporter, IUnitOfWork aUnitOfWork,
                                                  ILogStrategy aLogStrategy, string userEmail)
        {
            InitializeComponent();
            productImporter = aProductImporter;
            unitOfWork = aUnitOfWork;
            logStrategy = aLogStrategy;
            productFacade = new ProductFacade(unitOfWork);
            signedInUserEmail = userEmail;
            LoadInitialConfiguration();
        }

        private void LoadInitialConfiguration()
        {
            foreignDLLPanel.Controls.Add(productImporter.GetUIForNeededAttributes());
            foreignDLLPanel.Anchor = AnchorStyles.None;
        }

        private void btnLoadProducts_Click(object sender, EventArgs e)
        {
            try
            {
                AddAllProducts();
                ShowInformationMessage("Productos cargados correctamente!", "Información");
                logStrategy.SaveEntry(new LogEntry(ActionType.PRODUCT_LOAD, signedInUserEmail));
            }
            catch (Exception)
            {
                ShowErrorMessage("Hubo un problema al cargar el archivo seleccionado, por favor asegúrese de que el formato " +
                    "es correcto e intente nuevamente", "Error");
            }
        }

        private void AddAllProducts()
        {
            ICollection<Product> products = productImporter.ImportProducts();
            foreach (Product product in products)
            {
                productFacade.AddProduct(product);
            }
        }

        public void ShowErrorMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public void ShowInformationMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            UserControl previousUserControl = new LoadNewProductsUserControl(unitOfWork, logStrategy, signedInUserEmail);
            this.Controls.Clear();
            this.Controls.Add(previousUserControl);
        }
    }
}
