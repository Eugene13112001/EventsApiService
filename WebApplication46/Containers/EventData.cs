using System.Collections;
using WebApplication46.Models;
namespace WebApplication46.Containers
{
    public interface DataEvent
    {
       
        public Task<Event> AddEvent(Event ev);
        public Task<Event> ChangeElement(int id, Event ev);

        public Task<bool> RemoveElement(Event ev);

        public Task<Event?> GetElementId(Guid id);

        public Task<int> GetIdOfElem(Guid id);
        public Task<List<Event>?> GetAll();

        public Task<List<Event>> GetEvents(DateTime begin , DateTime end);


    }
    public class EventData : DataEvent
    {
        public int Id = 8;

        public List<Event> events = new List<Event> {
                new Event { Id= new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Name = "1" ,
                Description = "ff" , Begin = new DateTime(2022, 7, 2), End = new DateTime(2022, 8, 2),
                ImageId=  new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                 SpaceId=  new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                },
                 new Event { Id= new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"), Name = "2" ,
                Description = "ff" , Begin = new DateTime(2022, 7, 2), End = new DateTime(2022, 8, 2),
                ImageId=  new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                 SpaceId=  new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                }



        };


        public async Task<Event> AddEvent(Event ev)
        {
            ev.Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa" + Convert.ToString(this.Id));
            this.Id += 1;
            await Task.Run(() => { this.events.Add(ev); });
            return ev;
        }
        public async Task<bool> RemoveElement(Event ev)
        {
           return await Task.Run(() => {return this.events.Remove(ev); });
        }
        public async Task<Event?> GetElementId(Guid id)
        {
            return await Task.Run(() => {return this.events.FirstOrDefault(p => p.Id == id); });
            
        }
        public async Task<Event> ChangeElement(int id, Event ev)
        {
            await Task.Run(() => { this.events[id] = ev; });
            return this.events[id];
        }
        public async Task<int> GetIdOfElem(Guid id)
        {
            return await Task.Run(() => { return this.events.FindIndex(p => p.Id == id); });
        }

        public async Task<List<Event>?> GetEvents(DateTime begin, DateTime end)
        {
            return await Task.Run(() => {
                return this.events.Where(p => (p.Begin >= begin) && (p.Begin <= end)).ToList();
            });
        }
        public async Task<List<Event>?> GetAll()
        {
            return await Task.Run(() => {
                return this.events.ToList();
            });
        }

    }
}
