using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.ProductImporterLogic.XMLLogic
{
    internal partial class XMLProductImporterControl : UserControl
    {
        string xmlFilePath;

        public XMLProductImporterControl()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                xmlFilePath = dialog.FileName;
                this.pathTextBox.Text = dialog.FileName;
            } else
            {
                xmlFilePath = null;
                this.pathTextBox.Text = "-Sin seleccionar-";
            }
        }

        public string GetXMLFilePath()
        {
            return xmlFilePath;
        }
    }
}
