using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Fenton.Selenium.SuperDriver
{
    [Serializable]
    public class MismatchBetweenBrowsersException<T> : SuperException
    {
        public IEnumerable<T> Values { get; private set; }

        public MismatchBetweenBrowsersException(IEnumerable<T> values)
            : base(string.Format("Not all elements match: {0}", string.Join(",", values)))
        {
            Values = values;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo serializationInfo, StreamingContext context)
        {
            if (serializationInfo == null)
            {
                throw new ArgumentNullException("info");
            }

            serializationInfo.AddValue("Values", Values);

            base.GetObjectData(serializationInfo, context);
        }
    }
}
