using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPrintJobRepository
    {
        void Save(PrintJob job);
        PrintJob GetById(Guid id);
        PrintJob GetNextPendingByPriority();
        IEnumerable<PrintJob> GetAll();
        IEnumerable<PrintJob> GetPendingJobs();
    }
}
