using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Net;
using RainfallApi.Model;

namespace RainfallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallApiController : ControllerBase
    {
        readonly string apiURL = "https://environment.data.gov.uk/flood-monitoring/";

        // GET api/<RainfallApiController>/id/stations/3901/readings
        [ProducesResponseType(200, Type = typeof(RainfallReadingResponse))]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(404, Type = typeof(ErrorResponse))]
        [ProducesResponseType(500, Type = typeof(ErrorResponse))]
        [HttpGet("id/{stationId}/readings")]
        public async Task<IActionResult> Get(
            [Description("The id of the reading station")] string stationId,
            [FromQuery][Range(1, 100)][Description("The number of readings to return\r\n")] int? count = 10
         )
        {
            using (HttpClient httpClient = new ())
            {
                try
                {
                    var url = $"{apiURL}id/stations/{stationId}/readings?_limit={count}";
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and parse the JSON response
                        var responseData = await response.Content.ReadAsStringAsync();

                        return Ok(responseData);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound) // 404 Not Found
                    {
                        return NotFound("Resource not found.");
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest) // 400 Bad Request
                    {
                        return BadRequest("Bad request.");
                    }
                    else if (response.StatusCode == HttpStatusCode.InternalServerError) // 500 Internal Server Error
                    {
                        // Log the error or take other appropriate actions
                        return StatusCode((int)response.StatusCode, "Internal server error.");
                    }
                    else
                    {
                        // Handle other status codes as needed
                        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest("Bad request.");
                }
            }
        }
    }
}
