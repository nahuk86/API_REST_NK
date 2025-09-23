using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Job
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }
        public JobStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PrintedAt { get; set; }

        public Job()
        {
            Id = Guid.NewGuid().ToString();
            Status = JobStatus.PENDING;
            CreatedAt = DateTime.UtcNow;
        }

        public Job(string name, string content, int priority) : this()
        {
            Name = name;
            Content = content;
            Priority = priority;
        }

        public void MarkAsPrinted()
        {
            Status = JobStatus.PRINTED;
            PrintedAt = DateTime.UtcNow;
        }

        public void MarkAsError()
        {
            Status = JobStatus.ERROR;
        }

        public void Reset()
        {
            Status = JobStatus.PENDING;
            PrintedAt = null;
        }

        public bool IsPending() => Status == JobStatus.PENDING;
        public bool IsPrinted() => Status == JobStatus.PRINTED;
        public bool HasError() => Status == JobStatus.ERROR;
    }
}
