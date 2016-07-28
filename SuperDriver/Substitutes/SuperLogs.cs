using OpenQA.Selenium;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Fenton.Selenium.SuperDriver.Substitutes
{
    public class SuperLogs : ILogs
    {
        private readonly ParallelQuery<ILogs> _query;

        public SuperLogs(IEnumerable<ILogs> alerts)
        {
            _query = alerts.ToConcurrentQuery();
        }

        ReadOnlyCollection<string> ILogs.AvailableLogTypes
        {
            get
            {
                List<string> allAvailableLogTypes = new List<string>();
                _query.ForAll(a => allAvailableLogTypes.AddRange(a.AvailableLogTypes));
                return allAvailableLogTypes.AsReadOnly();
            }
        }

        public ReadOnlyCollection<LogEntry> GetLog(string logKind)
        {
            List<LogEntry> allLogs = new List<LogEntry>();
            _query.ForAll(a => allLogs.AddRange(a.GetLog(logKind)));
            return allLogs.AsReadOnly();
        }
    }
}
