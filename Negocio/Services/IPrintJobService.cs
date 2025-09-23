using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public interface IPrintJobService
    {
        PrintJob CreateJob(string name, string content, int priority);
        PrintJob GetJob(Guid id);
        PrintJob RetryJob(Guid id);
        PrintedDocument GetPrintedDocument(string name);
        bool IsDocumentPrinted(string name);
    }
}
