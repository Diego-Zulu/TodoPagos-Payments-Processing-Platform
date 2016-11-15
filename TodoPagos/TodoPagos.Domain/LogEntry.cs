using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain
{
    public class LogEntry
    { 
        public virtual string Action { get; set; }

        public virtual DateTime Date { get; set; }

        public string UserEmail { get; set; }

        public int ID { get; set; }

        protected LogEntry()
        {

        }

        public LogEntry(string actionType, string userEmail)
        {
            UserEmail = userEmail;
            Date = DateTime.UtcNow;
            Action = actionType;
        }

        public bool IsBetweenDates(DateTime from, DateTime to)
        {
            return Date >= from && Date <= to;
        }

        public override string ToString()
        {
            return "Acción: " + this.Action + " --- Fecha: " + this.Date + " --- Email: " + this.UserEmail;
        }
    }
}
