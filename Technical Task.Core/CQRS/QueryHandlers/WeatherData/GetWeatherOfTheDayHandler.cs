using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Technical_Task.Core.CQRS.Queries.WeatherData;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.QueryHandlers.WeatherData
{
    public class GetWeatherOfTheDayHandler : IRequestHandler<GetWeatherOfTheDayQuery, WeatherQueryResult>
    {
        private readonly ApplicationDbContext _db;

        public GetWeatherOfTheDayHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<WeatherQueryResult> Handle(GetWeatherOfTheDayQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var weather = _db.Weather.Include(x => x.WeatherTemperatures).SingleOrDefault(x =>
                    !x.IsDeleted
                    && x.DateUtc.Date == request.SelectedDate.Date
                    && x.CityId == request.SelectedCityId
                );
                if (weather == null) return null;

                var model = new WeatherQueryResult
                {
                    Id = weather.Id,
                    Date = weather.DateUtc,
                    TemperatureC = Math.Round(weather
                        .WeatherTemperatures
                        .Select(x => x.TemperatureC)
                        .OrderByDescending(x => x)
                        .Take(10)
                        .Average()),
                    Cloudiness = weather.Cloudiness,
                    Humidity = weather.Humidity,
                    Pressure = weather.Pressure,
                    RainChance = weather.RainChance,
                    WindSpeed = weather.WindSpeed,

                    DayTemperatures = weather.WeatherTemperatures.Select(x => new WeatherDayTemperatureResult
                    {
                        DayTime = x.DayTimeUtc,
                        TemperatureC = x.TemperatureC
                    }).OrderBy(x=>x.DayTime).ToList()
                };
                return Task.Run(() => model, cancellationToken);
            }
            catch (Exception ex)
            {
                // hmmmm
                return null;
            }
        }
    }
}
