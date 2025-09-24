using System;

namespace Domain.Entities
{
    public class PrintJob
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Priority { get; set; }
        public JobStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PrintedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public PrintJob()
        {
            Id = Guid.NewGuid();
            Status = JobStatus.PENDING;
            CreatedAt = DateTime.UtcNow;
        }

        public PrintJob(string name, string content, int priority) : this()
        {
            Name = name;
            Content = content;
            Priority = priority;
        }

        public void MarkAsPrinted()
        {
            Status = JobStatus.PRINTED;
            PrintedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsError()
        {
            Status = JobStatus.ERROR;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Reset()
        {
            Status = JobStatus.PENDING;
            PrintedAt = null;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsPending() => Status == JobStatus.PENDING;
        public bool IsPrinted() => Status == JobStatus.PRINTED;
        public bool HasError() => Status == JobStatus.ERROR;
    }
}