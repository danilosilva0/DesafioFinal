using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.Helper.Exceptions
{
    [Serializable]
    public class ServiceListException : Exception
    {
        public List<string> Messages { get; set; }
        public ServiceListException() { }
        public ServiceListException(IEnumerable<string> messages)
        {
            Messages = messages.ToList();
        }
        protected ServiceListException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}