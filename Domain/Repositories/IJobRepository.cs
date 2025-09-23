using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IJobRepository
    {
        void Save(Job job);
        Job GetById(string id);
        List<Job> GetPendingJobsOrderedByPriority();
        void Update(Job job);
        bool Exists(string id);
    }
}
