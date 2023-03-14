using System.Collections;
using WebApplication46.Models;
namespace WebApplication46.Containers
{
    public interface DataSpace
    {


        public Space? GetElementId(Guid id);




    }
    public class SpaceData : DataSpace
    {

        public List<Space> spaces = new List<Space> {
                new Space { Id= new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6") },
                new Space { Id= new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7") },


        };



        public Space? GetElementId(Guid id)
        {
           return this.spaces.FirstOrDefault(p => p.Id == id); 

        }


    }
}
