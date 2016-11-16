using System;
using System.Windows.Forms;
using TodoPagos.Domain.Repository;

namespace TodoPagos.AdminForm.Form
{
    public partial class PrincipalUserControl : UserControl
    {

        private ILogStrategy logStrategy;
        private IUnitOfWork unitOfWork;
        private string signedInUserEmail;

        public PrincipalUserControl(ILogStrategy aStrategy, IUnitOfWork aUnitOfWork, string nameOfUser, string userEmail)
        {
            InitializeComponent();
            LoadWelcomeMessage(nameOfUser);
            logStrategy = aStrategy;
            unitOfWork = aUnitOfWork;
            signedInUserEmail = userEmail;
        }

        private void LoadWelcomeMessage(string nameOfUser)
        {
            this.lblWelcomeLoad.Text = nameOfUser + " !";
        }

        private void btnPoints_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new PointsManagementUserControl(unitOfWork));
        }

        public void ChangeSecondPanel(UserControl userControl)
        {
            this.splitContainer.Panel2.Visible = false;
            this.splitContainer.Panel2.Controls.Clear();
            this.splitContainer.Panel2.Controls.Add(userControl);
            this.splitContainer.Panel2.Visible = true;
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new LogUserControl(logStrategy));
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new AvailableProductsUserControl(unitOfWork));
        }

        private void btnProductLoad_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new LoadNewProductsUserControl(unitOfWork, logStrategy, signedInUserEmail));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro de que desea salir del programa?",
                "Salir", MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK) Application.Exit();
        }
    }
}
