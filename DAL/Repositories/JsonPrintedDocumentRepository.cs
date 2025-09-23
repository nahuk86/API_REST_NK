using Domain.Entities;
using Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DAL.Repositories
{
    public class JsonPrintedDocumentRepository : IPrintedDocumentRepository
    {
        private readonly string _filePath;
        private readonly object _lockObject = new object();

        public JsonPrintedDocumentRepository(string filePath = "printed_documents.json")
        {
            _filePath = filePath;
            EnsureFileExists();
        }

        public void Save(PrintedDocument document)
        {
            lock (_lockObject)
            {
                var documents = LoadAllDocuments();

                // Verificamos si ya existe un documento con el mismo nombre
                var existingIndex = documents.FindIndex(d => d.Name.Equals(document.Name, StringComparison.OrdinalIgnoreCase));
                if (existingIndex >= 0)
                {
                    // Actualizamos el documento existente
                    documents[existingIndex] = document;
                }
                else
                {
                    // Agregamos nuevo documento
                    documents.Add(document);
                }

                SaveAllDocuments(documents);
            }
        }

        public PrintedDocument GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            lock (_lockObject)
            {
                var documents = LoadAllDocuments();
                return documents.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
        }

        public bool ExistsByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            lock (_lockObject)
            {
                var documents = LoadAllDocuments();
                return documents.Any(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
        }

        public IEnumerable<PrintedDocument> GetAll()
        {
            lock (_lockObject)
            {
                return LoadAllDocuments();
            }
        }

        private List<PrintedDocument> LoadAllDocuments()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<PrintedDocument>();

                var json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<PrintedDocument>();

                return JsonConvert.DeserializeObject<List<PrintedDocument>>(json) ?? new List<PrintedDocument>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error loading printed documents from file: {ex.Message}", ex);
            }
        }

        private void SaveAllDocuments(List<PrintedDocument> documents)
        {
            try
            {
                var json = JsonConvert.SerializeObject(documents, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving printed documents to file: {ex.Message}", ex);
            }
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }
    }
}
