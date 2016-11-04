using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminUserInterface
{
    public partial class LogUserControl : UserControl
    {
        private PrincipalUserControl principalUserControl;

        public LogUserControl(PrincipalUserControl principalUserControl)
        {
            this.principalUserControl = principalUserControl;
            InitializeComponent();
        }
    }
}
