using System;
using System.Linq;
using Technical_Task.Core.CQRS.Queries.WeatherData;
using Technical_Task.Core.Enums;
using Technical_Task.Core.Logic.Extensions;

namespace Technical_Task.Core.Logic
{
    public class WeatherDescriptionCreator
    {
        private readonly WeatherQueryResult _model;

        public string WeatherDescription { get; private set; }

        public WeatherDescriptionCreator(WeatherQueryResult model)
        {
            _model = model;
        }

        public WeatherDescriptionCreator BuildTemperatureDescription()
        {
            if(-273.15 > _model.TemperatureC )
                throw new Exception("Temperature cannot be below absolute zero");
            if (100 < _model.TemperatureC)
                throw new Exception("Earth nowadays had the most 58 degrees of celsius, given temperature seems too high");

            var valuesFromTheHighestToTheLowest = Enum.GetValues(typeof(TemperatureEnum)).Cast<int>().OrderByDescending(x=>x).ToList();
            foreach (var value in valuesFromTheHighestToTheLowest)
            {
                if (_model.TemperatureC >= value)
                {
                    WeatherDescription = ((TemperatureEnum)value).GetDescription();
                    return this;
                }
            }
            throw new Exception("Didn't find the description");
        }
    }
}
