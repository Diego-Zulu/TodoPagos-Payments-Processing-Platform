using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.AdminForm.Form
{
    public partial class PrincipalUserControl : UserControl
    {

        public PrincipalUserControl()
        {
            InitializeComponent();
        }

        private void btnPoints_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new PointsManagementUserControl(this));
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
            ChangeSecondPanel(new LogUserControl(this));
        }

        private void btnAvailableProducts_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new AvailableProductsUserControl(this));
        }

        private void btnProductLoad_Click(object sender, EventArgs e)
        {
            ChangeSecondPanel(new LoadNewProductsUserControl(this));
        }
    }
}
