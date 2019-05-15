using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Technical_Task.Core.CQRS.Queries.City;
using Technical_Task.Data;

namespace Technical_Task.Core.CQRS.QueryHandlers.City
{
    public class GetAllCitiesOfTheCountryQueryHandler : IRequestHandler<GetAllCitiesOfTheCountryQuery, GetAllCitiesOfTheCountryQueryResult>
    {
        private readonly ApplicationDbContext _db;

        public GetAllCitiesOfTheCountryQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<GetAllCitiesOfTheCountryQueryResult> Handle(GetAllCitiesOfTheCountryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Normally you shouldn't take all of the records but there not big number of the Cities in one country therefore it can be done like this
                //It's made like this only because of knowing that there is not many cities in one country
                var model = new GetAllCitiesOfTheCountryQueryResult
                {
                    AllCitiesOfTheCountry = _db.Cities
                        .Include(x => x.Country)
                        .Where(x => x.CountryId == request.CountryId 
                                    && !x.IsDeleted 
                                    && !x.Country.IsDeleted)
                        .Select(x => new CityQueryResult
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
                return Task.Run(() => new GetAllCitiesOfTheCountryQueryResult
                {
                    AllCitiesOfTheCountry = new List<CityQueryResult>()
                }, cancellationToken);
            }
        }
    }
}
