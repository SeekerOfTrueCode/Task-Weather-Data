using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Technical_Task.Core.CQRS.Commands.Country;
using Technical_Task.Core.CQRS.Queries.Country;

namespace Technical_Task.Api.Controllers
{
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await _mediator.Send(new GetAllCountriesQuery());
            return Ok(result.AllCountries.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddCountry(string name)
        {
            var result = await _mediator.Send(new AddCountryCommand {Name = name});
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCountry(long id)
        {
            var result = await _mediator.Send(new DeleteCountryCommand { Id = id });
            return Ok(result);
        }
    }
}