using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class JobNotFoundException : Exception
    {
        public JobNotFoundException(string jobId)
            : base($"Job with ID '{jobId}' not found")
        {
        }
    }
}
