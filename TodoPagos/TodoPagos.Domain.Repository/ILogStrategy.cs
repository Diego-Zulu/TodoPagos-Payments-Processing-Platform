using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoPagos.Domain.Repository
{
    public interface ILogStrategy
    {
        void SaveEntry(LogEntry entryToBeSaved);

        ICollection<LogEntry> GetEntries(DateTime from, DateTime to); 
    }
}
