using System;
using System.Collections.Generic;
using System.Linq;

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

        public ICollection<LogEntry> CheckLogBetweenDates(DateTime from, DateTime to)
        {
            ICollection<LogEntry> resultingLogEntries = new List<LogEntry>();
            FilterEntriesAndAddThemToResultingList(resultingLogEntries, from, to);
            return resultingLogEntries;
        }

        private void FilterEntriesAndAddThemToResultingList
            (ICollection<LogEntry> resultingLogEntries, DateTime from, DateTime to)
        {
            foreach (LogEntry entry in Entries)
            {
                if (entry.IsBetweenDates(from, to))
                {
                    resultingLogEntries.Add(entry);
                }
            }
        }
    }
}
