using System;
using System.Collections.Generic;

namespace Technical_Task.Models
{
    public class WeatherDayData
    {
        public long selectedCityId { get; set; }
        public DateTime selectedDate { get; set; }
        public double cloudiness { get; set; }
        public double wind { get; set; }
        public double humidity { get; set; }
        public double rainChance { get; set; }
        public double pressure { get; set; }
        public List<WeatherDayDataTemperatureModel> temperatures { get; set; }
    }
    public class WeatherDayDataTemperatureModel
    {
        public DateTime time { get; set; }
        public double temperature { get; set; }
    }
}
