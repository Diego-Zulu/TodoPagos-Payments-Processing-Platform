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
using Domain;

namespace TodoPagos.AdminForm.Form
{
    public partial class LogUserControl : UserControl
    {
        private ILogStrategy logStrategy;

        public LogUserControl(ILogStrategy aStrategy)
        {
            InitializeComponent();
            logStrategy = aStrategy;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DateTime fromUTC = GenerateFromDateInUTC();
            DateTime toUTC = GenerateToDateInUTC();

            LoadEntriesToListBox(fromUTC, toUTC);
        }

        private DateTime GenerateFromDateInUTC()
        {
            DateTime from;
            DateTime.TryParse(this.from.Text, out from);
            return from.ToUniversalTime();
        }

        private DateTime GenerateToDateInUTC()
        {
            DateTime to;
            DateTime.TryParse(this.to.Text, out to);
            return to.ToUniversalTime();
        }

        private void LoadEntriesToListBox(DateTime fromUTC, DateTime toUTC)
        {
            ICollection<LogEntry> entries = logStrategy.GetEntries(fromUTC, toUTC);
            lstLog.Items.Clear();
            foreach (LogEntry entry in entries)
            {
                lstLog.Items.Add(entry);
            }
        }
    }
}
