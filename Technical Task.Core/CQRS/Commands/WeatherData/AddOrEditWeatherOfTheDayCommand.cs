using System;
using System.Collections.Generic;
using MediatR;

namespace Technical_Task.Core.CQRS.Commands.WeatherData
{
    public class AddOrEditWeatherOfTheDayCommand : IRequest<bool>
    {
        public long SelectedCityId { get; set; }
        public DateTime SelectedDate { get; set; }
        public double Cloudiness { get; set; }
        public double WindSpeed { get; set; }
        public double Humidity { get; set; }
        public double RainChance { get; set; }
        public double Pressure { get; set; }
        public List<WeatherDayDataTemperatureCommand> Temperatures { get; set; }
    }
    
    public class WeatherDayDataTemperatureCommand
    {
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
    }
}
