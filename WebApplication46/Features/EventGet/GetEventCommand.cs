using FluentValidation;
using MediatR;
using WebApplication46.Containers;
using WebApplication46.Features.EventDelete;
using WebApplication46.Models;

namespace WebApplication46.Features.EventGet
{
    public class GetEventCommand : IRequest<Event?>
    {

        public Guid Id { get; set; }
        public class GetEventCommandHandler : IRequestHandler<GetEventCommand, Event?>
        {
            private readonly DataEvent _dataevent;

            public GetEventCommandHandler(DataEvent events)
            {
                _dataevent = events ?? throw new ArgumentNullException(nameof(events));
            }

            public async Task<Event?> Handle(GetEventCommand command, CancellationToken cancellationToken)
            {
                return await this._dataevent.GetElementId(command.Id);

              
            }
            public class GetProductCommandValidator : AbstractValidator<GetEventCommand>
            {
                public GetProductCommandValidator()
                {

                    RuleFor(c => c.Id).NotEmpty();

                }


            }
        }
    }
}
