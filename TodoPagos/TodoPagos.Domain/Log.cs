using System.Collections.Generic;

namespace Domain
{
    public class Log
    {
        public virtual ICollection<LogEntry> Entries { get; set; }

        public Log()
        {
            Entries = new List<LogEntry>();
        }

        public void AddEntry(LogEntry newEntry)
        {
            Entries.Add(newEntry);
        }
    }
}
