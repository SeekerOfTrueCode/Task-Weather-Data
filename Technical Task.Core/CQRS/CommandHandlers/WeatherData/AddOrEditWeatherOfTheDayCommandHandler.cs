using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Technical_Task.Core.CQRS.Commands.WeatherData;
using Technical_Task.Core.Logic.Validators;
using Technical_Task.Data;
using Technical_Task.Data.DTO;

namespace Technical_Task.Core.CQRS.CommandHandlers.WeatherData
{
    public class AddOrEditWeatherOfTheDayCommandHandler : IRequestHandler<AddOrEditWeatherOfTheDayCommand, bool>
    {
        private readonly ApplicationDbContext _db;
        public AddOrEditWeatherOfTheDayCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<bool> Handle(AddOrEditWeatherOfTheDayCommand request, CancellationToken cancellationToken)
        {
            if (!IsValid(request)) return Task.Run(() => false, cancellationToken);

            var weatherOfTheDay = _db.Weather
                .Include(x => x.WeatherTemperatures)
                .SingleOrDefault(weather => weather.DateUtc.Date == request.SelectedDate.Date 
                                            && weather.CityId == request.SelectedCityId
                                            && !weather.IsDeleted);

            return weatherOfTheDay == null ? Add(request, cancellationToken) : Edit(weatherOfTheDay, request, cancellationToken);
        }
        private Task<bool> Add(AddOrEditWeatherOfTheDayCommand request, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope())
            {
                var weatherModel = new WeatherOfTheDay
                {
                    DateUtc = request.SelectedDate,
                    CityId = request.SelectedCityId,
                    Cloudiness = request.Cloudiness,
                    Humidity = request.Humidity,
                    Pressure = request.Pressure,
                    RainChance = request.RainChance,
                    WindSpeed = request.WindSpeed,
                    CreatedDateUtc = DateTime.Now,
                };
                _db.Weather.Add(weatherModel);

                if (_db.SaveChanges() <= 0) return Task.Run(() => false, cancellationToken);

                var temperaturesModel = new List<WeatherTemperatureOfTheDay>(request.Temperatures.Select(x =>
                    new WeatherTemperatureOfTheDay
                    {
                        DayTimeUtc = x.Time,
                        TemperatureC = x.Temperature,
                        WeatherOfTheDayId = weatherModel.Id
                    }).ToList());

                _db.AddRange(temperaturesModel);

                if (_db.SaveChanges() != 24) throw new Exception("Didn't add all 24 temperature measurements");

                scope.Complete();
                return Task.Run(() => true, cancellationToken);
            }
        }
        private Task<bool> Edit(WeatherOfTheDay weatherOfTheDay, AddOrEditWeatherOfTheDayCommand request, CancellationToken cancellationToken)
        {
            using (var scope = new TransactionScope())
            {
                if (weatherOfTheDay.WeatherTemperatures.Count != 24)
                {
                    throw new Exception("Weather record doesn't have 24 temperature measurements");
                }

                weatherOfTheDay.DateUtc = request.SelectedDate;
                weatherOfTheDay.CityId = request.SelectedCityId;
                weatherOfTheDay.Cloudiness = request.Cloudiness;
                weatherOfTheDay.Humidity = request.Humidity;
                weatherOfTheDay.Pressure = request.Pressure;
                weatherOfTheDay.RainChance = request.RainChance;
                weatherOfTheDay.WindSpeed = request.WindSpeed;
                _db.SaveChanges();

                foreach (var weatherTemperatureOfTheDay in weatherOfTheDay.WeatherTemperatures)
                {
                    //null reference possible but If at this point it shows up then the exception should be thrown
                    weatherTemperatureOfTheDay.TemperatureC = request
                        .Temperatures
                        .SingleOrDefault(x=>x.Time.TimeOfDay == weatherTemperatureOfTheDay.DayTimeUtc.TimeOfDay)
                        .Temperature;
                }
                _db.SaveChanges();

                scope.Complete();
            }

            return Task.Run(() => true, cancellationToken);
        }
        private static bool IsValid(AddOrEditWeatherOfTheDayCommand request)
        {
            var percentageValidator = new PercentageValidator();
            var isValid = percentageValidator.IsValid(request.Cloudiness)
                          && percentageValidator.IsValid(request.Humidity)
                          && percentageValidator.IsValid(request.RainChance)
                          && percentageValidator.IsValid(request.Cloudiness)
                          && new IsInRangeValidator(0, 500).IsValid(request.WindSpeed)
                          && new IsInRangeValidator(0, 1200).IsValid(request.Pressure)
                          && request.Temperatures.All(x => new IsInRangeValidator(-50, 65).IsValid(x.Temperature));
            return isValid;
        }
    }

}
