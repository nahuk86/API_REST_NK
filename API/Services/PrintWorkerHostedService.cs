using Negocio.Workers;

namespace API.Services
{
    public class PrintWorkerHostedService : BackgroundService
    {
        private readonly IPrintWorker _printWorker;
        private readonly ILogger<PrintWorkerHostedService> _logger;

        public PrintWorkerHostedService(IPrintWorker printWorker, ILogger<PrintWorkerHostedService> logger)
        {
            _printWorker = printWorker;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Print Worker Hosted Service started");

            await _printWorker.StartAsync(stoppingToken);

            _logger.LogInformation("Print Worker Hosted Service stopped");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Print Worker Hosted Service is stopping");

            await _printWorker.StopAsync();

            await base.StopAsync(cancellationToken);
        }
    }
}
