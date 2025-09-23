using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PrintedDocument
    {
        public string Name { get; set; }
        public DateTime PrintedAt { get; set; }
        public DateTime InsertedAt { get; set; }

        public PrintedDocument()
        {
            InsertedAt = DateTime.UtcNow;
        }

        public PrintedDocument(string name, DateTime printedAt) : this()
        {
            Name = name;
            PrintedAt = printedAt;
        }
    }
}
