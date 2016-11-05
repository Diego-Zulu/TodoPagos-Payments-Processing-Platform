using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoPagos.Domain.Repository;

namespace TodoPagos.AdminForm.Logic
{
    public class LogQueryFacade
    {

        private ILogStrategy logStrategy;

        public LogQueryFacade(ILogStrategy aStrategy)
        {
            CheckForNullILogStrategy(aStrategy);
            logStrategy = aStrategy;
        }

        private void CheckForNullILogStrategy(ILogStrategy aStrategy)
        {
            if (aStrategy == null) throw new ArgumentException();
        }

        public ICollection<LogEntry> GetEntries(DateTime from, DateTime to)
        {
            return logStrategy.GetEntries(from, to);
        }
    }
}
