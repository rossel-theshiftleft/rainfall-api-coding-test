using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RainfallApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RainfallApiController : ControllerBase
    {
        readonly string apiURL = "https://environment.data.gov.uk/flood-monitoring/";

        // GET api/<RainfallApiController>/id/3901/readings
        [HttpGet("id/{stationId}/readings")]
        public async Task<IActionResult> Get(
            [Description("The id of the reading station")] string stationId,
            [FromQuery][Range(1, 100)][Description("The number of readings to return\r\n")] int? count = 10
         )
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"{apiURL}id/stations/{stationId}/readings?_limit={count}");

                    if (response.IsSuccessStatusCode)
                    {
                        // Read and parse the JSON response
                        var responseData = await response.Content.ReadAsStringAsync();

                        return Ok(responseData);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error" + ex.Message);
                }
            }
            return Ok();
        }
    }
}
