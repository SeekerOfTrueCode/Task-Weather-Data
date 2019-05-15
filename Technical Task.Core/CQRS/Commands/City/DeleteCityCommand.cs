using MediatR;

namespace Technical_Task.Core.CQRS.Commands.City
{
    public class DeleteCityCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
