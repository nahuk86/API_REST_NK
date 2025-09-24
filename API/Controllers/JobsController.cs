using Microsoft.AspNetCore.Mvc;
using Negocio.Services;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IPrintJobService _printJobService;

        public JobsController(IPrintJobService printJobService)
        {
            _printJobService = printJobService;
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] CreateJobRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (request.Priority < 1 || request.Priority > 10)
                {
                    return BadRequest(new { error = "Priority must be between 1 and 10" });
                }

                var job = _printJobService.CreateJob(request.Name, request.Content, request.Priority);

                return Ok(new
                {
                    id = job.Id,
                    status = job.Status.ToString()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetJob(Guid id)
        {
            try
            {
                var job = _printJobService.GetJob(id);

                if (job == null)
                {
                    return NotFound(new { error = "Job not found" });
                }

                var response = new
                {
                    id = job.Id,
                    name = job.Name,
                    content = job.Content,
                    priority = job.Priority,
                    status = job.Status.ToString(),
                    createdAt = job.CreatedAt,
                    printedAt = job.PrintedAt
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }

        [HttpPost("{id}/retry")]
        public IActionResult RetryJob(Guid id)
        {
            try
            {
                var job = _printJobService.RetryJob(id);

                if (job == null)
                {
                    return NotFound(new { error = "Job not found" });
                }

                return Ok(new
                {
                    id = job.Id,
                    status = job.Status.ToString(),
                    message = "Job has been reset to PENDING status"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }

    public class CreateJobRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public string Content { get; set; }

        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }
    }
}
