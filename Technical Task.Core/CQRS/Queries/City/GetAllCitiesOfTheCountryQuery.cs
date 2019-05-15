using System.Collections.Generic;
using MediatR;

namespace Technical_Task.Core.CQRS.Queries.City
{
    public class GetAllCitiesOfTheCountryQuery : IRequest<GetAllCitiesOfTheCountryQueryResult>
    {
        public long CountryId { get; set; }
    }

    public class GetAllCitiesOfTheCountryQueryResult
    {
        public ICollection<CityQueryResult> AllCitiesOfTheCountry { get; set; }
    }
    public class CityQueryResult
    {
        public long Id;
        public string Name;
    }
}
