using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.ProductImporterLogic.JSONLogic
{
    public partial class JSONProductImporterControl : UserControl
    {
        private string txtWithJsonFilePath;

        public JSONProductImporterControl()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtWithJsonFilePath = dialog.FileName;
                this.pathTextBox.Text = dialog.FileName;
            }
            else
            {
                txtWithJsonFilePath = null;
                this.pathTextBox.Text = "-Sin seleccionar-";
            }
        }

        public string GetJSONFilePath()
        {
            return txtWithJsonFilePath;
        }
    }
}
