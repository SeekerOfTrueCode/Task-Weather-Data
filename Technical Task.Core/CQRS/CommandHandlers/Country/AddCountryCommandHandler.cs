using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Technical_Task.Core.CQRS.Commands.Country;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.CommandHandlers.Country
{
    public class AddCityCommandHandler : IRequestHandler<AddCountryCommand, AddCountryCommandResult>
    {
        private readonly ApplicationDbContext _db;
        public AddCityCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<AddCountryCommandResult> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var model = new AddCountryCommandResult
            {
                Id = -1,
                Name = "Error"
            };

            if (!_db.Countries.Any(x => x.Name == request.Name && !x.IsDeleted) && request.Name != null)
            {
                var newRow = new Data.DTO.Country
                {
                    IsDeleted = false,
                    Name = request.Name,
                };
                _db.Countries.Add(newRow);
                var added = _db.SaveChanges() > 0;
                if (added)
                {
                    model.Id = newRow.Id;
                    model.Name = newRow.Name;
                }

            }
            return Task.Run(() => model, cancellationToken);
        }

    }
}
