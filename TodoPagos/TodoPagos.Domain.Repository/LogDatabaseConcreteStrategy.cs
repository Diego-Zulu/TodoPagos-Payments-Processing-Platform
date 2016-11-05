using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoPagos.Domain.Repository
{
    public class LogDatabaseConcreteStrategy : ILogStrategy
    {

        private IUnitOfWork unitOfWork;

        public LogDatabaseConcreteStrategy(IUnitOfWork aUnitOfWork)
        {
            CheckForNullUnitOfWork(aUnitOfWork);
            unitOfWork = aUnitOfWork;
        }

        private void CheckForNullUnitOfWork(IUnitOfWork aUnitOfWork)
        {
            if (aUnitOfWork == null) throw new ArgumentException();
        }

        public void SaveEntry(LogEntry newEntry)
        {
            unitOfWork.EntriesRepository.Insert(newEntry);
            unitOfWork.Save();
        }

        public ICollection<LogEntry> GetEntries(DateTime from, DateTime to)
        {
            ICollection<LogEntry> resultingLogEntries = new List<LogEntry>();
            FilterEntriesAndAddThemToResultingList(resultingLogEntries, from, to);
            return resultingLogEntries;
        }

        private void FilterEntriesAndAddThemToResultingList
            (ICollection<LogEntry> resultingLogEntries, DateTime from, DateTime to)
        {
            foreach (LogEntry entry in unitOfWork.EntriesRepository.Get(null, null, ""))
            {
                if (entry.IsBetweenDates(from, to))
                {
                    resultingLogEntries.Add(entry);
                }
            }
        }
    }
}
