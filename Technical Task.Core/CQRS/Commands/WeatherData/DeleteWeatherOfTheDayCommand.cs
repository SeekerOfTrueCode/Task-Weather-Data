using System;
using MediatR;

namespace Technical_Task.Core.CQRS.Commands.WeatherData
{
    public class DeleteWeatherOfTheDayCommand : IRequest<bool>
    {
        public long SelectedCityId { get; set; }
        public DateTime SelectedDate { get; set; }
    }
}
