using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidPriorityException : Exception
    {
        public InvalidPriorityException(int priority)
            : base($"Priority must be between 1 and 10. Received: {priority}")
        {
        }
    }
}
