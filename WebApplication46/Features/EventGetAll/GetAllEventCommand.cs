using FluentValidation;
using MediatR;
using WebApplication46.Containers;
using WebApplication46.Models;

namespace WebApplication46.Features.EventGetAll
{
    public class GetAllEventCommand : IRequest<List<Event>>
    {

        
        public class GetAllEventCommandHandler : IRequestHandler<GetAllEventCommand, List<Event>>
        {
            private readonly DataEvent _dataevent;

            public GetAllEventCommandHandler(DataEvent events)
            {
                _dataevent = events ?? throw new ArgumentNullException(nameof(events));
            }

            public async Task<List<Event>> Handle(GetAllEventCommand command, CancellationToken cancellationToken)
            {
                return await this._dataevent.GetAll();


            }
            
        }
    }
}
