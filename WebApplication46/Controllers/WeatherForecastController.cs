using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApplication46.Models;
using WebApplication46.Features.EventCreate;
using WebApplication46.Features.EventChange;
using WebApplication46.Features.EventDelete;
using WebApplication46.Features.EventGet;
using WebApplication46.Features.EventGetAll;
using WebApplication46.Features.EventGetFiltr;
using WebApplication46.Filtrs;

namespace WebApplication46.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        

       
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        /// <summary>
        /// �������� ��� �������
        /// </summary>
        ///  <remarks>
        /// �� ���� ������ �� ���������
        /// ������ ���������� ������ ������ ���� ������� � ����
        /// </remarks>
        /// <returns>������ ���� �������</returns>
        [HttpGet]
        [TypeFilter(typeof(SampleExceptionFilter))]
        [Route("api/Events")]
        public async Task<IActionResult> GetAll()
        {
            
                GetAllEventCommand client= new GetAllEventCommand();
                CancellationToken token = new CancellationToken();
                List<Event> ev = await _mediator.Send(client, token);
                return new JsonResult(new { id = ev });
            
         
        }
        /// <summary>
        /// �������� ������ �������, ������� �������� � ������ ���������� �������
        /// </summary>
        ///  <remarks>
        /// �� ���� ��������� ��� ���������  :
        /// 
        /// begin ���� datetime - ������ �������
        /// 
        /// end ���� datetime - ����� �������
        /// 
        /// ��������:
        /// 
        /// begin: 2023-03-14T22:02:57.704Z
        /// 
        /// end: 2023-04-14T22:02:57.704Z
        /// 
        /// ������ ������ �������, ������� �������� � ������ ���������� �������
        /// </remarks>
        [HttpGet]
        [TypeFilter(typeof(SampleExceptionFilter))]
        [Route("api/Events/{begin:datetime}&{end:datetime}")]
        public async Task<IActionResult> GetWithFiltr([FromRoute]  DateTime begin, DateTime end)
        {
            
            
                GetAllEventFiltrCommand client = new GetAllEventFiltrCommand { Begin=begin, End = end};
                CancellationToken token = new CancellationToken();
                List<Event> ev = await _mediator.Send(client, token);
                return new JsonResult(new { id = ev });
            
           
        }
        /// <summary>
        /// �������� ������� �� ��� id
        /// </summary>
        ///  <remarks>
        /// �� ���� ��������� �������� id ���� Guid
        /// 
        /// ��������:
        /// 
        /// 3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// 
        /// ������ ���������� ��� ���������� � �������, ���� ��� ����
        /// � ���� ���, �� ���������� ��������� �� ����
        /// </remarks>
        [HttpGet]
        [TypeFilter(typeof(SampleExceptionFilter))]
        [Route("api/Events/{id:guid}")]
        public async Task<IActionResult> GetEvent([FromRoute] Guid id)
        {
            
                GetEventCommand client = new GetEventCommand { Id = id};
                CancellationToken token = new CancellationToken();
                Event? ev = await _mediator.Send(client, token);
                if (ev == null) return BadRequest("������� � ����� id ���");
                return new JsonResult(new { Event = ev });
           
        }
        /// <summary>
        /// �������� ����� ������� 
        /// </summary>
        ///  <remarks>
        /// �� ���� ��������� ������ �������:
        /// ��������:
        /// 
        /// {
        /// 
            /// "name": "string",
            /// 
            ///"description": "string",
            ///
            ///"begin": "2023-03-14T21:57:07.693Z",
            ///
            /// "end": "2023-03-14T21:57:07.693Z",
            /// 
            /// "imageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            /// 
            ///"spaceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            ///
        /// }
        /// 
        /// ������ ���������� id,  ������ ������������ ������� � ���� ������ 
        /// 
        /// </remarks>
        [Route("api/Events")]
        [TypeFilter(typeof(SampleExceptionFilter))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddEventCommand client,
           CancellationToken token)
        {
          
           
                Event ev = await _mediator.Send(client, token);
                return new JsonResult(new { id = ev.Id });
           
        }
        /// <summary>
        /// �������� �������  
        /// </summary>
        ///  <remarks>
        /// �� ���� ����� ������ ������� ��������� id �������, ������� ����� ��������:
        /// 
        /// ��������:
        /// 
        /// 3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// 
        /// ����� �� ���� ����� body ���������� ������ ������ �������:
        /// 
        /// ��������:
        /// 
        /// {
        /// 
            /// "name": "string",
            /// 
            ///"description": "string",
            ///"begin": "2023-03-14T21:57:07.693Z",
            ///
            /// "end": "2023-03-14T21:57:07.693Z",
            /// 
            /// "imageId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            /// 
            ///"spaceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
            ///
        /// } 
        /// 
        /// ������ ���������� ���������� �������, ���� ��� ���� ������� ��������
        /// 
        /// ���� ���, �� ���������� ������� ������
        /// </remarks>
        
        [HttpPut]
        [TypeFilter(typeof(SampleExceptionFilter))]
        [Route("api/Events/{id:guid}")]
        public async Task<IActionResult> Change([FromRoute] Guid id , [FromBody] ChangeEventCommand client,
           CancellationToken token)
        {
            
                client.Id = id;
                Event? ev = await _mediator.Send(client, token);
                if (ev == null) return BadRequest("������� � ����� id ���");
                return new JsonResult(new { id = ev });
           
        }
        /// <summary>
        /// ������� ������� �� id 
        /// </summary>
        ///  <remarks>
        /// �� ���� ��������� id �������:
        /// 
        /// ��������:
        /// 
        /// 3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// 
        /// ������ ���������� true, ���� ������� ���� ������� ������
        /// 
        /// ���� ���, �� ���������� ������� ������
        /// </remarks>
        [HttpDelete]
        [TypeFilter(typeof(SampleExceptionFilter))]
        [Route("api/Events/{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            
                DeleteEventCommand client = new DeleteEventCommand { Id = id };
                CancellationToken token = new CancellationToken();
                bool ev = await _mediator.Send(client, token);
                if (ev is false) return BadRequest("������� � ����� id ���");
                return new JsonResult(true);
            
           
        }
    }
}