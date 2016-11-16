using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using TodoPagos.ProductImporterLogic;
using AdminUserInterface;
using TodoPagos.Domain.Repository;
using TodoPagos.AdminForm.Logic;
using TodoPagos.Domain;

namespace TodoPagos.AdminForm.Form
{
    public partial class LoadNewProductsUserControl : UserControl
    {

        private ProductFacade productFacade;
        private IUnitOfWork unitOfWork;
        private ILogStrategy logStrategy;
        private string signedInUserEmail;

        public LoadNewProductsUserControl(IUnitOfWork aUnitOfWork, ILogStrategy aLogStrategy, string userEmail)
        {
            InitializeComponent();
            unitOfWork = aUnitOfWork;
            logStrategy = aLogStrategy;
            productFacade = new ProductFacade(unitOfWork);
            signedInUserEmail = userEmail;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Bibliotecas de enlaces dinámicos (*.dll) | *.dll";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string path = file.FileName;
                    Assembly myAssembly = Assembly.LoadFile(path);
                    foreach (Type classType in myAssembly.GetTypes())
                    {
                        if (typeof(IProductImporter).IsAssignableFrom(classType))
                        {
                            IProductImporter productImporter = (IProductImporter)Activator.CreateInstance(classType);
                            SetNextUserControlAndAddEntryToLog(productImporter);
                        }
                     }
                }
            }
            catch (Exception)
            {
                ShowErrorMessage("Ocurrió un problema al tratar de cargar el .dll seleccionado, por favor intente " +
                    "nuevamente más tarde", "Error");
            }
        }

        public void ShowErrorMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void SetNextUserControlAndAddEntryToLog(IProductImporter productImporter)
        {
            this.Controls.Clear();
            this.Controls.Add(new LoadNewProductsAcceptedUserControl(productImporter, unitOfWork, logStrategy, signedInUserEmail));
        }
    }
}
