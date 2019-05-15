using MediatR;

namespace Technical_Task.Core.CQRS.Commands.Country
{
    public class DeleteCountryCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
