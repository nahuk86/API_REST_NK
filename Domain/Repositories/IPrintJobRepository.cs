using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IPrintJobRepository
    {
        void Save(PrintJob job);
        PrintJob GetById(Guid id);
        PrintJob GetNextPendingJob();
        PrintJob GetHighestPriorityPending();
        IEnumerable<PrintJob> GetAll();
        IEnumerable<PrintJob> GetPendingJobs();
    }
}