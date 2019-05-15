using MediatR;

namespace Technical_Task.Core.CQRS.Commands.City
{
    public class AddCityCommand : IRequest<AddCityCommandResult> 
    {
        public string Name { get; set; }
        public long CountryId { get; set; }
    }

    public class AddCityCommandResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CountryId { get; set; }

    }
}
