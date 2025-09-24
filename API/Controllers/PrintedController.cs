using Microsoft.AspNetCore.Mvc;
using Negocio.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrintedController : ControllerBase
    {
        private readonly IPrintJobService _printJobService;

        public PrintedController(IPrintJobService printJobService)
        {
            _printJobService = printJobService;
        }

        [HttpGet("{name}")]
        public IActionResult GetPrintedDocument(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest(new { error = "Name parameter is required" });
                }

                var printedDocument = _printJobService.GetPrintedDocument(name);

                if (printedDocument == null)
                {
                    return Ok(new
                    {
                        found = false,
                        name = name,
                        message = "Document not found or not printed yet"
                    });
                }

                return Ok(new
                {
                    found = true,
                    name = printedDocument.Name,
                    printedAt = printedDocument.PrintedAt,
                    insertedAt = printedDocument.InsertedAt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}
