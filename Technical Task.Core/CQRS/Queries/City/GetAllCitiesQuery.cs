using System.Collections.Generic;
using MediatR;

namespace Technical_Task.Core.CQRS.Queries.City
{
    public class GetAllCitiesQuery : IRequest<GetAllCitiesQueryResult>
    {
    }

    public class GetAllCitiesQueryResult
    {
        public ICollection<CityWithCountryIdQueryResult> AllCities { get; set; }
    }

    public class CityWithCountryIdQueryResult : CityQueryResult
    {
        public long CountryId { get; set; }
    }
}
