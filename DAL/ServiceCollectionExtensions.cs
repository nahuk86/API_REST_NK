using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;


namespace DAL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string dataPath = null)
        {
            // Configuramos las rutas de los archivos
            var basePath = dataPath ?? "Data";

            // Creamos el directorio si no existe
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            var printJobsPath = Path.Combine(basePath, "printjobs.json");
            var printedDocumentsPath = Path.Combine(basePath, "printed_documents.json");

            // Registramos los repositorios como singletons para evitar problemas de concurrencia
            services.AddSingleton<IPrintJobRepository>(provider =>
                new JsonPrintJobRepository(printJobsPath));

            services.AddSingleton<IPrintedDocumentRepository>(provider =>
                new JsonPrintedDocumentRepository(printedDocumentsPath));

            return services;
        }
    }
}
