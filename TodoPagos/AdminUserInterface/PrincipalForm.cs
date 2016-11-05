using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TodoPagos.AdminForm.Form
{
    public partial class PrincipalForm : System.Windows.Forms.Form
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
            if (UserIsValid())
            {
                ChangeActivePanel(new PrincipalUserControl());
            }
        }

        private bool UserIsValid()
        {
            string email = this.txtEmailLogin.Text;
            string password = this.txtPasswordLogin.Text;
            return true;
        }

        public void ChangeActivePanel(UserControl userControl)
        {
            activePanel.Visible = false;
            activePanel.Controls.Clear();
            activePanel.Controls.Add(userControl);
            activePanel.Visible = true;
        }
    }
}
