using FluentValidation;
using MediatR;
using WebApplication46.Containers;
using WebApplication46.Models;
namespace WebApplication46.Features.EventCreate
{
    
        public class AddEventCommand: IRequest<Event>
        {

           
            public string Name { get; set; }

            public string Description { get; set; }
            public DateTime Begin { get; set; }

            public DateTime End { get; set; }

            public Guid ImageId { get; set; }

            public Guid SpaceId { get; set; }
            public class AddEventCommandHandler : IRequestHandler<AddEventCommand, Event>
            {
                private readonly DataEvent _dataevent;
                private readonly DataImage _images;
                public readonly DataSpace _spaces;

                public AddEventCommandHandler(DataEvent events, DataImage images, DataSpace space)
                {
                        this._spaces = space ?? throw new ArgumentNullException(nameof(space));
                        this._dataevent = events ?? throw new ArgumentNullException(nameof(events));
                        this._images = images ?? throw new ArgumentNullException(nameof(images));
                }

                public async Task<Event> Handle(AddEventCommand command, CancellationToken cancellationToken)
                {
                    Event ev = new Event();
                    ev.Name = command.Name;
                    ev.Description = command.Description;
                    ev.Begin = command.Begin;
                    ev.End = command.End;
                    ev.ImageId = command.ImageId;
                    ev.SpaceId = command.SpaceId;
                    
                    return await this._dataevent.AddEvent(ev);
            }


            }
        public class AddProductCommandValidator : AbstractValidator<AddEventCommand>
        {
            public AddProductCommandValidator(DataImage images, DataSpace space)
            {
                
                RuleFor(c => c.Name).NotEmpty();
                RuleFor(c => c.Description).NotEmpty();
                RuleFor(c => c.Begin).NotEmpty();
                RuleFor(c => c.End).NotEmpty();
                RuleFor(c => this.ImageCheck(images, c.ImageId)).Equal(true).WithMessage("Изображения не существет");
                RuleFor(c => this.SpaceCheck(space, c.SpaceId)).Equal(true)
                    .WithMessage("Пространства не существет");
                RuleFor(c => this.DateCheck(c.Begin, c.End)).Equal(true)
                    .WithMessage("Дата конца дожна быть больше даты начала");
            }

            private bool DateCheck(DateTime? begin, DateTime? end)
            {
                if (begin is null) return false;
                if (end is null) return false;
                if (begin <= end)return true;
                return false;
            }

            private bool ImageCheck(DataImage images,  Guid id )
            {
                var ch =  images.GetElementId(id);
                if (ch is null ) return false;
                return true;
            }
            private bool SpaceCheck(DataSpace spaces, Guid id)
            {
                var ch =  spaces.GetElementId(id);
                if (ch is null) return false;
                return true;
            }
        }

    }
   
}
