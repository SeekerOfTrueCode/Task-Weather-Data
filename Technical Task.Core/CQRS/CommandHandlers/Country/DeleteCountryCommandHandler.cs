using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Technical_Task.Core.CQRS.Commands.Country;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.CommandHandlers.Country
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, bool>
    {
        private readonly ApplicationDbContext _db;
        public DeleteCountryCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var country = _db.Countries.Where(x => !x.IsDeleted).SingleOrDefault(x => x.Id == request.Id);
            bool deleted = false;
            if (country != null)
            {
                country.IsDeleted = true;
                country.DeletedDateUtc = DateTime.UtcNow;
                deleted = _db.SaveChanges() > 0;
            }
            
            return Task.Run(() => deleted, cancellationToken);
        }
    }
}
