using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical_Task.Core.CQRS.Commands.WeatherData;
using Technical_Task.Core.CQRS.Queries.WeatherData;
using Technical_Task.Models;

namespace Technical_Task.Api.Controllers
{
    public class WeatherController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WeatherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherOfTheDay(DateTime selectedDate,
            long selectedCityId)
        {
            var result = await _mediator.Send(new GetWeatherOfTheDayQuery
            {
                SelectedDate = selectedDate,
                SelectedCityId = selectedCityId
            });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditWeatherOfTheDayAsync(WeatherDayData model)
        {
            var result = await _mediator.Send(new AddOrEditWeatherOfTheDayCommand
            {
                SelectedCityId = model.selectedCityId,
                SelectedDate = model.selectedDate,
                Cloudiness = model.cloudiness,
                WindSpeed = model.wind,
                Humidity = model.humidity,
                RainChance = model.rainChance,
                Pressure = model.pressure,
                Temperatures = model.temperatures.Select(x=> new WeatherDayDataTemperatureCommand
                {
                    Time = x.time,
                    Temperature = x.temperature
                }).ToList()
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWeatherOfTheDayAsync(DateTime selectedDate, long selectedCityId)
        {
            var result = await _mediator.Send(new DeleteWeatherOfTheDayCommand
            {
                SelectedCityId = selectedCityId,
                SelectedDate = selectedDate
            });
            return Ok(result);
        }
    }
}
