using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Technical_Task.Core.CQRS.Commands.WeatherData;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.CommandHandlers.WeatherData
{
    public class DeleteWeatherOfTheDayCommandHandler : IRequestHandler<DeleteWeatherOfTheDayCommand, bool>
    {
        private readonly ApplicationDbContext _db;
        public DeleteWeatherOfTheDayCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<bool> Handle(DeleteWeatherOfTheDayCommand request, CancellationToken cancellationToken)
        {
            var weather = _db.Weather.SingleOrDefault(x => x.CityId == request.SelectedCityId
                                                           && x.DateUtc.Date == request.SelectedDate.Date
                                                           && !x.IsDeleted);

            var deleted = false;
            if (weather != null)
            {
                weather.IsDeleted = true;
                weather.DeletedDateUtc = DateTime.Now;
                deleted = _db.SaveChanges() > 0;
            }

            return Task.Run(() => deleted, cancellationToken);
        }
    }
}
