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
    public partial class LoadNewProductsUserControl : UserControl
    {
        private PrincipalUserControl principalUserControl;

        public LoadNewProductsUserControl(PrincipalUserControl principalUserControl)
        {
            this.principalUserControl = principalUserControl;
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
            }
        }
    }
}
