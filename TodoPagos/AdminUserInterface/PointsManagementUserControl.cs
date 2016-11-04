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
    public partial class PointsManagementUserControl : UserControl
    {
        private PrincipalUserControl principalUserControl;

        public PointsManagementUserControl(PrincipalUserControl principalUserControl)
        {
            this.principalUserControl = principalUserControl;
            InitializeComponent();
        }
    }
}
