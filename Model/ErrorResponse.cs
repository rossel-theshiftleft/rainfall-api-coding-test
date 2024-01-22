using Microsoft.AspNetCore.Mvc;

namespace RainfallApi.Model
{
    public class ErrorResponse
    {
        public string propertyName { get; set; }
        public string message { get; set; }
    }
}
