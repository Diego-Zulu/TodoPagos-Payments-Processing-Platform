using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminUserInterface
{
    public partial class PrincipalForm : Form
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new PrincipalForm());
        }
        public PrincipalForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            activePanel.Visible = false;
            activePanel.Controls.Clear();
            activePanel.Controls.Add(new PrincipalUserControl());
            activePanel.Visible = true;
        }
    }
}
