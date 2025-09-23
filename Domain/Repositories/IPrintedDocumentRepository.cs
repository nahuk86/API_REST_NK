using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPrintedDocumentRepository
    {
        void Save(PrintedDocument document);
        PrintedDocument GetByName(string name);
        bool ExistsByName(string name);
    }
}
