using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LogEntry
    { 
        public virtual ActionType Action { get; set; }

        public virtual DateTime Date { get; set; }

        public string UserEmail { get; set; }

        public LogEntry(ActionType actionType, string userEmail)
        {
            UserEmail = userEmail;
            Date = DateTime.UtcNow;
            Action = actionType;
        }
    }
}
