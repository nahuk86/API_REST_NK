using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    public class PersistenceConfiguration
    {
        public string DataPath { get; set; } = "Data";
        public string PrintJobsFileName { get; set; } = "printjobs.json";
        public string PrintedDocumentsFileName { get; set; } = "printed_documents.json";

        public string PrintJobsFilePath => Path.Combine(DataPath, PrintJobsFileName);
        public string PrintedDocumentsFilePath => Path.Combine(DataPath, PrintedDocumentsFileName);
    }
}
