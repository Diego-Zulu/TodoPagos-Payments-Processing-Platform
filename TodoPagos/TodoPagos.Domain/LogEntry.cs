using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LogEntry
    { 
        public ActionType Action { get; set; }

        public DateTime Date { get; set; }

        public LogEntry(ActionType actionType)
        {
            Date = DateTime.UtcNow;
            Action = actionType;
        }
    }
}
