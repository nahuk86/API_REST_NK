using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime? UpdatedAt { get; set; }
        public DateTime? PrintedAt { get; set; }
    }
}
