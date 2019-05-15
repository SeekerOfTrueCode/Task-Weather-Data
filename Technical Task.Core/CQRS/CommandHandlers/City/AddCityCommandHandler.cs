using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Technical_Task.Core.CQRS.Commands.City;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.CommandHandlers.City
{
    public class AddCityCommandHandler : IRequestHandler<AddCityCommand, AddCityCommandResult>
    {
        private readonly ApplicationDbContext _db;
        public AddCityCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        Task<AddCityCommandResult> IRequestHandler<AddCityCommand, AddCityCommandResult>.Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            var model = new AddCityCommandResult
            {
                Id = -1,
                Name = "Error",
                CountryId = -1
            };

            if( request.Name != null 
               && !_db.Cities.Any(x => x.Name == request.Name && !x.IsDeleted) 
               && _db.Countries.Any(y => y.Id == request.CountryId && !y.IsDeleted))
            {
                var newRow = new Data.DTO.City
                {
                    IsDeleted = false,
                    Name = request.Name,
                    CountryId = request.CountryId,
                };
                _db.Cities.Add(newRow);
                var added = _db.SaveChanges() > 0;
                if (added)
                {
                    model.Id = newRow.Id;
                    model.Name = newRow.Name;
                    model.CountryId = newRow.CountryId;
                }

            }

            return Task.Run(() => model, cancellationToken);
        }
    }
}
