using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvApi.Models;
using CsvApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CsvApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DataController : ControllerBase
    {
        private readonly ICsvService _csvService;
        private readonly string _csvFilePath;

        public DataController(ICsvService csvService, IConfiguration configuration)
        {
            _csvService = csvService;
            _csvFilePath = configuration["CsvFilePath"] ?? throw new InvalidOperationException("CsvFilePath is not configured");
        }

        /// <summary>
        /// Get data from CSV file with optional limit parameter.
        /// </summary>
        /// <param name="limit">Number of rows to return. If not specified, all rows are returned</param>
        /// <returns>A JSON array of data from the CSV file.</returns>
        /// <response code="200">Returns the list of people</response>
        /// <response code="204">No content available</response>
        /// <reponse code="400">Invalid limit parameter</reponse>
        /// <response code="500">File not found or invalid CSV</response>
        [HttpGet]
        public IActionResult Get([FromQuery] int? limit)
        {
            try
            {
                // Return 400 if limit parameter is negative
                if (limit.HasValue && limit < 0)
                {
                    return BadRequest(new { message = "Limit must be a non-negative integer." });
                }

                var people = _csvService.ReadCsv(_csvFilePath);

                // Return 204 No Content if no data
                if (people.Count == 0)
                {
                    return NoContent();
                }

                // Apply limit parameter if provided
                if (limit.HasValue)
                {
                    people = people.Take(limit.Value).ToList();
                }

                return Ok(people);
            }
            // Handle specific exceptions
            catch (FileNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (InvalidDataException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
