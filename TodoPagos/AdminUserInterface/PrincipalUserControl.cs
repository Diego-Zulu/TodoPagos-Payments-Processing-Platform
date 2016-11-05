using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoPagos.Domain.Repository;

namespace TodoPagos.AdminForm.Form
{
    public partial class PrincipalUserControl : UserControl
    {

        private ILogStrategy logStrategy;

        public PrincipalUserControl(ILogStrategy aStrategy)
        {
            InitializeComponent();
            logStrategy = aStrategy;
        }

        private void btnPoints_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new PointsManagementUserControl());
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

        private void btnAvailableProducts_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new AvailableProductsUserControl());
        }

        private void btnProductLoad_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new LoadNewProductsUserControl());
        }
    }
}
