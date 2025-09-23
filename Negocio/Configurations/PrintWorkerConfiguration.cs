using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Configurations
{
    public class PrintWorkerConfiguration
    {
        public int ProcessingIntervalMs { get; set; } = 5000; // Cada 5 segundos
        public int SimulatedProcessingTimeMs { get; set; } = 2000; // 2 segundos de procesamiento
        public double SuccessProbabilityPercentage { get; set; } = 70.0; // 70% de éxito
    }
}
