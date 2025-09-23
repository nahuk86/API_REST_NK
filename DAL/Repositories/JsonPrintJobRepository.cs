using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Domain.Repositories;


namespace DAL.Repositories
{
    public class JsonPrintJobRepository : IPrintJobRepository
    {
        private readonly string _filePath;
        private readonly object _lockObject = new object();

        public JsonPrintJobRepository(string filePath = "printjobs.json")
        {
            _filePath = filePath;
            EnsureFileExists();
        }

        public void Save(PrintJob job)
        {
            lock (_lockObject)
            {
                var jobs = LoadAllJobs();

                // Si ya existe, lo actualizamos
                var existingIndex = jobs.FindIndex(j => j.Id == job.Id);
                if (existingIndex >= 0)
                {
                    jobs[existingIndex] = job;
                }
                else
                {
                    jobs.Add(job);
                }

                SaveAllJobs(jobs);
            }
        }

        public PrintJob GetById(Guid id)
        {
            lock (_lockObject)
            {
                var jobs = LoadAllJobs();
                return jobs.FirstOrDefault(j => j.Id == id);
            }
        }

        public PrintJob GetNextPendingJob()
        {
            lock (_lockObject)
            {
                var jobs = LoadAllJobs();
                return jobs
                    .Where(j => j.Status == JobStatus.PENDING)
                    .OrderByDescending(j => j.Priority) // Mayor prioridad primero
                    .ThenBy(j => j.CreatedAt) // En caso de empate, el más antiguo
                    .FirstOrDefault();
            }
        }

        public IEnumerable<PrintJob> GetAll()
        {
            lock (_lockObject)
            {
                return LoadAllJobs();
            }
        }

        public IEnumerable<PrintJob> GetPendingJobs()
        {
            lock (_lockObject)
            {
                var jobs = LoadAllJobs();
                return jobs.Where(j => j.Status == JobStatus.PENDING);
            }
        }

        public PrintJob GetHighestPriorityPending()
        {
            // This is an alias for GetNextPendingJob() to maintain compatibility
            return GetNextPendingJob();
        }

        private List<PrintJob> LoadAllJobs()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<PrintJob>();

                var json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<PrintJob>();

                return JsonConvert.DeserializeObject<List<PrintJob>>(json) ?? new List<PrintJob>();
            }
            catch (Exception ex)
            {
                // Log del error si fuera necesario
                throw new InvalidOperationException($"Error loading jobs from file: {ex.Message}", ex);
            }
        }

        private void SaveAllJobs(List<PrintJob> jobs)
        {
            try
            {
                var json = JsonConvert.SerializeObject(jobs, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error saving jobs to file: {ex.Message}", ex);
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
