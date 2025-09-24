using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Negocio.Configurations;   

namespace Negocio.Workers
{
    public class PrintWorker : IPrintWorker, IDisposable
    {
        private readonly IPrintJobRepository _printJobRepository;
        private readonly IPrintedDocumentRepository _printedDocumentRepository;
        private readonly PrintWorkerConfiguration _configuration;
        private readonly System.Timers.Timer _timer;
        private readonly Random _random;
        private bool _isDisposed = false;

        public PrintWorker(
            IPrintJobRepository printJobRepository,
            IPrintedDocumentRepository printedDocumentRepository,
            PrintWorkerConfiguration configuration)
        {
            _printJobRepository = printJobRepository;
            _printedDocumentRepository = printedDocumentRepository;
            _configuration = configuration;
            _random = new Random();

            _timer = new System.Timers.Timer(_configuration.ProcessingIntervalMs);
            _timer.Elapsed += ProcessPendingJobs;
            _timer.AutoReset = true;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _timer.Stop();
            return Task.CompletedTask;
        }

        private void ProcessPendingJobs(object sender, ElapsedEventArgs e)
        {
            try
            {
                // ✅ Cambiar GetHighestPriorityPending() por GetNextPendingByPriority()
                var pendingJob = _printJobRepository.GetNextPendingByPriority();
                if (pendingJob == null)
                    return;

                ProcessJob(pendingJob);
            }
            catch (Exception ex)
            {
                // Log error but continue processing
                Console.WriteLine($"Error processing job: {ex.Message}");
            }
        }

        private void ProcessJob(PrintJob job)
        {
            // Simular tiempo de procesamiento
            Thread.Sleep(_configuration.SimulatedProcessingTimeMs);

            // Decidir si la impresión es exitosa basado en la probabilidad configurada
            var successProbability = _random.NextDouble() * 100;

            if (successProbability <= _configuration.SuccessProbabilityPercentage)
            {
                // Éxito: marcar como PRINTED y guardar en registro de impresiones
                job.Status = JobStatus.PRINTED;
                job.PrintedAt = DateTime.UtcNow;
                job.UpdatedAt = DateTime.UtcNow;

                _printJobRepository.Save(job);

                // ✅ Guardar en el registro de documentos impresos sin Id
                var printedDocument = new PrintedDocument(job.Name, job.PrintedAt.Value);

                _printedDocumentRepository.Save(printedDocument);
            }
            else
            {
                // Falla silenciosa: no cambiar el estado (queda PENDING para reintento)
                // Solo actualizamos la fecha de último intento para auditoria
                job.UpdatedAt = DateTime.UtcNow;
                _printJobRepository.Save(job);
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _timer?.Dispose();
                _isDisposed = true;
            }
        }
    }
}
