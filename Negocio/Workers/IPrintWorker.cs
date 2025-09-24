using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio.Workers
{
    public interface IPrintWorker
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync();
    }
}
