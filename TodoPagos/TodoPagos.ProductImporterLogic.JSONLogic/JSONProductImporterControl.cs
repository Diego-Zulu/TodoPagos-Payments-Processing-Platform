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
        private string jsonFilePath;

        public JSONProductImporterControl()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "json files (*.json)|*.json";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                jsonFilePath = dialog.FileName;
                this.pathTextBox.Text = dialog.FileName;
            }
            else
            {
                jsonFilePath = null;
                this.pathTextBox.Text = "-Sin seleccionar-";
            }
        }

        public string GetJSONFilePath()
        {
            return jsonFilePath;
        }
    }
}
