using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TodoPagos.Domain;
using TodoPagos.Domain.Repository;

namespace TodoPagos.AdminForm.Form
{
    public partial class PointsManagementUserControl : UserControl
    {
        private IUnitOfWork unitOfWork;
        private PointsManager pointsManager;

        public PointsManagementUserControl(IUnitOfWork aUnitOfWork)
        {
            InitializeComponent();
            unitOfWork = aUnitOfWork;
            InitializeConfiguration();
        }

        private void InitializeConfiguration()
        {
            AssignPointsManager();
            SetPointActualValueLabel();
            LoadActualBlackListAndRemovableProvidersList();
            LoadProvidersToAddToBlackList();
        }

        private void AssignPointsManager()
        {
            PointsManager attemptedPointsManager = unitOfWork.PointsManagerRepository.GetByID(1);
            if(attemptedPointsManager == null)
            {
                attemptedPointsManager = PointsManager.GetInstance();
                unitOfWork.PointsManagerRepository.Insert(attemptedPointsManager);
                unitOfWork.Save();
            }
            pointsManager = attemptedPointsManager;
        }

        private void SetPointActualValueLabel()
        {
            this.lblActualValueLoad.Text = "";
            this.lblActualValueLoad.Text = pointsManager.MoneyPerPoint.ToString();
        }

        private void LoadActualBlackListAndRemovableProvidersList()
        {
            ICollection<Provider> actualBlackList = pointsManager.Blacklist;
            FillActualBlackListAndRemoveFromBlackListLists(actualBlackList);
        }

        private void FillActualBlackListAndRemoveFromBlackListLists(ICollection<Provider> actualBlackList)
        {
            lstActualBlacklist.Items.Clear();
            lstRemoveFromBlacklist.Items.Clear();
            foreach (Provider provider in actualBlackList)
            {
                lstActualBlacklist.Items.Add(provider);
                lstRemoveFromBlacklist.Items.Add(provider);
            }
        }

        private void LoadProvidersToAddToBlackList()
        {
            ICollection<Provider> actualBlackList = pointsManager.Blacklist;
            IEnumerable<Provider> allProviders = unitOfWork.ProviderRepository.Get(null, null, "");
            FillAddToBlackListListWithActiveProviders(actualBlackList, allProviders);
        }

        private void FillAddToBlackListListWithActiveProviders(ICollection<Provider> actualBlackList, IEnumerable<Provider> allProviders)
        {
            lstAddToBlacklist.Items.Clear();
            foreach (Provider provider in allProviders)
            {
                if (!actualBlackList.Contains(provider) && provider.Active)
                {
                    lstAddToBlacklist.Items.Add(provider);
                }
            }
        }

        private void btnNewPointValue_Click(object sender, EventArgs e)
        {
            try
            {
                SetNewPointValue();
            }
            catch (Exception ex) when (ex is FormatException || ex is ArgumentException)
            {
                ShowErrorMessage("El nuevo valor del Punto debe ser un número mayor a cero", "Error");
            }
            finally
            {
                ResetNewPointValueLabel();
            }
        }

        private void SetNewPointValue()
        {
            double newValue = Double.Parse(this.txtNewPointValue.Text);
            pointsManager.ChangeMoneyPerPointRatio(newValue);
            unitOfWork.Save();
            SetPointActualValueLabel();
        }

        private void ResetNewPointValueLabel()
        {
            this.txtNewPointValue.Text = "";
        }

        public void ShowErrorMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK
                , MessageBoxIcon.Error);
        }

        private void btnAddToBlacklist_Click(object sender, EventArgs e)
        {
            try
            {
                Provider selectedProvider = (Provider)lstAddToBlacklist.SelectedItem;
                CheckForNullSelectedProvider(selectedProvider);
                pointsManager.AddProviderToBlacklist(selectedProvider);
                unitOfWork.Save();
                ReloadLists();
            }
            catch (InvalidOperationException)
            {
                ShowErrorMessage("Debe seleccionar un Proveedor a agregar a la lista negra de Proveedores", "Error");
            }
        }

        private void CheckForNullSelectedProvider(Provider selectedProvider)
        {
            if (selectedProvider == null) throw new InvalidOperationException();
        }

        private void ReloadLists()
        {
            LoadActualBlackListAndRemovableProvidersList();
            LoadProvidersToAddToBlackList();
        }

        private void btnRemoveFromBlacklist_Click(object sender, EventArgs e)
        {
            try
            {
                Provider selectedProvider = (Provider)lstRemoveFromBlacklist.SelectedItem;
                CheckForNullSelectedProvider(selectedProvider);
                pointsManager.RemoveProviderFromBlacklist(selectedProvider);
                unitOfWork.Save();
                ReloadLists();
            }
            catch (InvalidOperationException)
            {
                ShowErrorMessage("Debe seleccionar un Proveedor a quitar de la lista negra de Proveedores", "Error");
            }
        }
    }
}
