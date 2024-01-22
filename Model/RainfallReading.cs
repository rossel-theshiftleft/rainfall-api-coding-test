using Microsoft.AspNetCore.Mvc;

namespace RainfallApi.Model
{
    public class RainfallReading
    {
        public DateTime dateMeasured { get; set; }
        public decimal amountMeasured { get; set; }
    }
}
