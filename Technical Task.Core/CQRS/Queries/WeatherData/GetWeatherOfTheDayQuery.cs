using System;
using System.Collections.Generic;
using MediatR;
using Technical_Task.Core.Logic;

namespace Technical_Task.Core.CQRS.Queries.WeatherData
{
    public class GetWeatherOfTheDayQuery : IRequest<WeatherQueryResult>
    {
        public DateTime SelectedDate;
        public long SelectedCityId { get; set; }
    }

    public class WeatherQueryResult
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public double TemperatureC { get; set; }

        public double TemperatureF => 32 + (int)(TemperatureC / 0.5556); //used in JS

        public string ForecastMessage => new WeatherDescriptionCreator(this) //used in JS
            .BuildTemperatureDescription()
            .WeatherDescription;

        public double Cloudiness { get; set; }
        public double WindSpeed { get; set; }
        public double Humidity { get; set; }
        public double RainChance { get; set; }
        public double Pressure { get; set; }

        public IEnumerable<WeatherDayTemperatureResult> DayTemperatures { get; set; }
    }
    public class WeatherDayTemperatureResult
    {
        public DateTime DayTime { get; set; }

        public double TemperatureC { get; set; }
    }
}
