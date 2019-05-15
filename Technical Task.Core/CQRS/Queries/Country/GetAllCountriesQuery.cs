using System.Collections.Generic;
using MediatR;

namespace Technical_Task.Core.CQRS.Queries.Country
{
    public class GetAllCountriesQuery : IRequest<GetAllCountriesQueryResult>
    {

    }

    public class GetAllCountriesQueryResult
    {
        public ICollection<CountryQueryResult> AllCountries { get; set; }
    }
    public class CountryQueryResult
    {
        public long Id;
        public string Name;
    }
}
