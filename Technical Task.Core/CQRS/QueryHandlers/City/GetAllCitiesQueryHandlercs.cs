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
    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, GetAllCitiesQueryResult>
    {
        private readonly ApplicationDbContext _db;

        public GetAllCitiesQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<GetAllCitiesQueryResult> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Normally you shouldn't take all of the records but there not big number of the Cities in the world therefore it can be done like this
                var model = new GetAllCitiesQueryResult
                {
                    AllCities = _db.Cities
                        .Include(x=>x.Country)
                        .Where(x => !x.IsDeleted && !x.Country.IsDeleted)
                        .Select(x => new CityWithCountryIdQueryResult
                        {
                            Id = x.Id,
                            Name = x.Name,
                            CountryId = x.CountryId
                        }).ToList()
                };
                return Task.Run(() => model, cancellationToken);
            }
            catch (Exception exception)
            {
                //TODO log it exception
                //hmmm
                return Task.Run(() => new GetAllCitiesQueryResult
                {
                    AllCities = new List<CityWithCountryIdQueryResult>()
                }, cancellationToken);
            }
        }
    }
}
