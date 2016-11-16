using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TodoPagos.AdminForm.Logic;
using TodoPagos.Domain.Repository;
using TodoPagos.ProductImporterLogic;

namespace TodoPagos.AdminForm.Form
{
    public partial class AvailableProductsUserControl : UserControl
    {
        private IUnitOfWork unitOfWork;
        private ProductFacade productFacade;

        public AvailableProductsUserControl(IUnitOfWork aUnitOfWork)
        {
            InitializeComponent();
            unitOfWork = aUnitOfWork;
            productFacade = new ProductFacade(unitOfWork);
            LoadProductsList();
        }

        private void LoadProductsList()
        {
            lstActualAvailableProducts.Items.Clear();
            IEnumerable<Product> allProducts = unitOfWork.ProductsRepository.Get(null, null, "");
            foreach(Product product in allProducts)
            {
                lstActualAvailableProducts.Items.Add(product);
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                TryToRemoveProduct();
            }
            catch (NullReferenceException)
            {
                ShowErrorMessage("Debe seleccionar un Producto a borrar!", "Error");
            }
        }

        private void TryToRemoveProduct()
        {
            Product productToBeDeleted = (Product)lstActualAvailableProducts.SelectedItem;
            productFacade.DeleteProduct(productToBeDeleted);
            ClearTextBoxes();
            ShowSuccessMessage("Producto borrado correctamente!", "Éxito");
            LoadProductsList();
        }

        private void ShowErrorMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK
                , MessageBoxIcon.Error);
        }

        private void ShowSuccessMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK
                , MessageBoxIcon.Information);
        }

        private void ClearTextBoxes()
        {
            txtName.Text = "";
            txtNeededPoints.Text = "";
            txtDescription.Text = "";
            txtStock.Text = "";
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                TryToUpdateProduct();
            }
            catch (NullReferenceException)
            {
                ShowErrorMessage("Debe seleccionar un Producto a modificar!", "Error");
            }
            catch (ArgumentException)
            {
                ShowErrorMessage("Alguno de los datos no es correcto, por favor revise " + 
                    "e intente nuevamente", "Error");
            }
        }

        private void TryToUpdateProduct()
        {
            Product productToBeModified = (Product)lstActualAvailableProducts.SelectedItem;
            Product modifiedProduct = CreateModifiedProduct(productToBeModified);
            productFacade.ModifyProduct(productToBeModified, modifiedProduct);
            ClearTextBoxes();
            ShowSuccessMessage("Producto modificado correctamente!", "Éxito");
            LoadProductsList();
        }

        private Product CreateModifiedProduct(Product productToBeModified)
        {
            string name = GetName(productToBeModified);
            string description = GetDescription(productToBeModified);
            int neededPoints = GetNeededPoints(productToBeModified);
            int stock = GetStock(productToBeModified);
            Product modifiedProduct = new Product(name, description, neededPoints);
            modifiedProduct.Stock = stock;
            return modifiedProduct;
        }

        private string GetName(Product productToBeModified)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) return productToBeModified.Name;
            return txtName.Text;
        }

        private int GetStock(Product productToBeModified)
        {
            int stock;
            int.TryParse(txtStock.Text, out stock);
            if (string.IsNullOrWhiteSpace(txtStock.Text)) return productToBeModified.Stock;
            if (stock < 0) throw new ArgumentException();
            return stock;
        }

        private int GetNeededPoints(Product productToBeModified)
        {
            int neededPoints;
            int.TryParse(txtNeededPoints.Text, out neededPoints);
            if (string.IsNullOrWhiteSpace(txtNeededPoints.Text)) return productToBeModified.NeededPoints;
            if (neededPoints < 0) throw new ArgumentException();
            return neededPoints;
        }

        private string GetDescription(Product productToBeModified)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text)) return productToBeModified.Description;
            return txtDescription.Text;
        }
    }
}
