using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class JobNotFoundException : BusinessException
    {
        public JobNotFoundException(Guid jobId) : base($"Job with ID {jobId} was not found") { }
    }

    public class InvalidPriorityException : BusinessException
    {
        public InvalidPriorityException(int priority) : base($"Priority {priority} is invalid. Must be between 1 and 10") { }
    }
}
