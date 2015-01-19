using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Fenton.Selenium.SuperDriver
{
    public class SuperAlert : IAlert
    {
        private readonly ParallelQuery<IAlert> _query;

        public SuperAlert(IEnumerable<IAlert> alerts)
        {
            _query = alerts.ToConcurrentQuery();
        }

        public void Accept()
        {
            _query.ForAll(a => a.Accept());
        }

        public void Dismiss()
        {
            _query.ForAll(a => a.Dismiss());
        }

        public void SendKeys(string keysToSend)
        {
            _query.ForAll(a => a.SendKeys(keysToSend));
        }

        public string Text
        {
            get { return _query.Select(a => a.Text).AssertAllMatch().First(); }
        }
    }
}
