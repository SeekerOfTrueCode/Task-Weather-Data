using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Technical_Task.Data.DTO
{
    [Table("TTWeatherTemperatureOfTheDay")]
    public class WeatherTemperatureOfTheDay
    {
        [Key]
        public long Id { get; set; }

        public DateTime DayTimeUtc { get; set; }
        public double TemperatureC { get; set; }


        public long WeatherOfTheDayId { get; set; }
        public WeatherOfTheDay WeatherOfTheDay { get; set; }
    }
}
