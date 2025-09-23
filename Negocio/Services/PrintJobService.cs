using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Services
{
    public class PrintJobService : IPrintJobService
    {
        private readonly IPrintJobRepository _printJobRepository;
        private readonly IPrintedDocumentRepository _printedDocumentRepository;

        public PrintJobService(
            IPrintJobRepository printJobRepository,
            IPrintedDocumentRepository printedDocumentRepository)
        {
            _printJobRepository = printJobRepository;
            _printedDocumentRepository = printedDocumentRepository;
        }

        public PrintJob CreateJob(string name, string content, int priority)
        {
            ValidatePriority(priority);

            var job = new PrintJob
            {
                Id = Guid.NewGuid(),
                Name = name,
                Content = content,
                Priority = priority,
                Status = JobStatus.PENDING,
                CreatedAt = DateTime.UtcNow
            };

            _printJobRepository.Save(job);
            return job;
        }

        public PrintJob GetJob(Guid id)
        {
            return _printJobRepository.GetById(id);
        }

        public PrintJob RetryJob(Guid id)
        {
            var job = _printJobRepository.GetById(id);
            if (job == null)
                throw new InvalidOperationException($"Job with ID {id} not found");

            job.Status = JobStatus.PENDING;
            job.UpdatedAt = DateTime.UtcNow;

            _printJobRepository.Save(job);
            return job;
        }

        public PrintedDocument GetPrintedDocument(string name)
        {
            return _printedDocumentRepository.GetByName(name);
        }

        public bool IsDocumentPrinted(string name)
        {
            return _printedDocumentRepository.ExistsByName(name);
        }

        private void ValidatePriority(int priority)
        {
            if (priority < 1 || priority > 10)
                throw new ArgumentException("Priority must be between 1 and 10", nameof(priority));
        }
    }
}
