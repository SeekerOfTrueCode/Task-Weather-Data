using System;
using Technical_Task.Core.CQRS.Queries;
using Technical_Task.Core.CQRS.Queries.WeatherData;
using Technical_Task.Core.Enums;
using Technical_Task.Core.Logic;
using Xunit;

namespace Technical_Task.Test
{
    public class WeatherDescriptionCreatorTest
    {
        [Theory]
        [InlineData(TemperatureEnum.AbsoluteZeroHumansCannotSurvive, "Humans cannot survive such low temperatures - it's an absolute zero")]
        [InlineData(TemperatureEnum.TooColdLifeInDanger, "Live in danger - too cold")]
        [InlineData(TemperatureEnum.Freezing, "Freezing")]
        [InlineData(TemperatureEnum.VeryCold, "Very cold")]
        [InlineData(TemperatureEnum.Bracing, "Bracing")]
        [InlineData(TemperatureEnum.Chilly, "Chilly")]
        [InlineData(TemperatureEnum.Cool, "Cool")]
        [InlineData(TemperatureEnum.Mild, "Mild")]
        [InlineData(TemperatureEnum.Warm, "Warm")] // OK
        [InlineData(TemperatureEnum.Balmy, "Balmy")]
        [InlineData(TemperatureEnum.Hot, "Hot")]
        [InlineData(TemperatureEnum.Sweltering, "Sweltering")]
        [InlineData(TemperatureEnum.Scorching, "Scorching")]
        [InlineData(TemperatureEnum.TooWarmLiveInDanger, "Live danger - temperature")]
        [InlineData(TemperatureEnum.TooHotHumansCannotSurvive, "Humans cannot survive such high temperatures - too hot")]
        public void TemperatureToArbitralFeelingDescription(TemperatureEnum temperatureC, string expected)
        {
            var model = new WeatherQueryResult
            {
                TemperatureC = (double)temperatureC
            };
            var creator = new WeatherDescriptionCreator(model);
            var actual = creator
                .BuildTemperatureDescription();
            Assert.Equal(actual.WeatherDescription, expected);
        }
    }
}
