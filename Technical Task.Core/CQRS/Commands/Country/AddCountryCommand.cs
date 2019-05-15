using MediatR;

namespace Technical_Task.Core.CQRS.Commands.Country
{
    public class AddCountryCommand : IRequest<AddCountryCommandResult> 
    {
        public string Name { get; set; }
    }

    public class AddCountryCommandResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

    }
}
