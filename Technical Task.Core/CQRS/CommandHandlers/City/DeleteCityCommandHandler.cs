using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Technical_Task.Core.CQRS.Commands.City;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.CommandHandlers.City
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, bool>
    {
        private readonly ApplicationDbContext _db;
        public DeleteCityCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<bool> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var city = _db.Cities.Where(x => !x.IsDeleted).SingleOrDefault(x => x.Id == request.Id);
            bool deleted = false;
            if (city != null)
            {
                city.IsDeleted = true;
                city.DeletedDateUtc = DateTime.UtcNow;
                deleted = _db.SaveChanges() > 0;
            }
            
            return Task.Run(() => deleted, cancellationToken);
        }
    }
}
