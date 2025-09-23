using Negocio.Configurations;
using Negocio.Services;
using Negocio.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Negocio
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Servicios de negocio
            services.AddScoped<IPrintJobService, PrintJobService>();

            // Worker de procesamiento (Singleton para que funcione en segundo plano)
            services.AddSingleton<IPrintWorker, PrintWorker>();

            // Configuración del worker
            services.AddSingleton(new PrintWorkerConfiguration
            {
                ProcessingIntervalMs = 3000, // Cada 3 segundos
                SimulatedProcessingTimeMs = 1500, // 1.5 segundos de procesamiento
                SuccessProbabilityPercentage = 70.0 // 70% de éxito
            });

            return services;
        }
    }
}
