using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Technical_Task.Core.CQRS.Queries.Country;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.QueryHandlers.Country
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, GetAllCountriesQueryResult>
    {
        private readonly ApplicationDbContext _db;

        public GetAllCountriesQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<GetAllCountriesQueryResult> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Normally you shouldn't take all of the records but there not big number of the Countries therefore it can be done like this
                //It's made like this only because of knowing that there is not many countries
                var model = new GetAllCountriesQueryResult
                {
                    AllCountries = _db.Countries
                        .Where(x=>!x.IsDeleted)
                        .Select(x => new CountryQueryResult
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).ToList()
                };
                return Task.Run(() => model, cancellationToken);
            }
            catch (Exception exception)
            {
                //TODO log it exception
                //hmmmm 
                return Task.Run(() => new GetAllCountriesQueryResult
                {
                    AllCountries = new List<CountryQueryResult>()
                }, cancellationToken);

            }
        }
    }
}
