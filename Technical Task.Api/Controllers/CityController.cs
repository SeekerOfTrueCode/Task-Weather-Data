using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical_Task.Core.CQRS.Commands.City;
using Technical_Task.Core.CQRS.Queries.City;

namespace Technical_Task.Api.Controllers
{
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCitiesOfTheCountry(long selectedCountryId)
        {
            var result = await _mediator.Send(new GetAllCitiesOfTheCountryQuery
            {
                CountryId = selectedCountryId
            });
            return Ok(result.AllCitiesOfTheCountry.ToList());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await this._mediator.Send(new GetAllCitiesQuery());
            return Ok(result.AllCities.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> AddCity(string name, long countryId)
        {
            var result = await _mediator.Send(new AddCityCommand { Name = name, CountryId = countryId });
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCity(long id)
        {
            var result = await _mediator.Send(new DeleteCityCommand { Id = id });
            return Ok(result);
        }
    }
}