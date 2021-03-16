using EventTickets.EventCatalog.Models;
using EventTickets.EventCatalog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            this._eventRepository = eventRepository;
        }

        //api/events/jdshfaksjdfas
        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventDto>> GetById(Guid eventId)
        {
            var result = await _eventRepository.GetEventByIdAsync(eventId);
            return Ok(new EventDto
            {
                Artist = result.Artist,
                CategoryId = result.CategoryId,
                CategoryName = result.Category.Name,
                Name = result.Name,
                Date = result.Date,
                Description = result.Description,
                EventId = result.EventId,
                Price = result.Price
            });
        }

        //api/events?categoryId=fadsfaj
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get([FromQuery] Guid categoryId)
        {
            var result = await _eventRepository.GetEventsByCategoryIdAsync(categoryId);
            return Ok(result.Select(x => new EventDto
            {
                Artist = x.Artist,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Date = x.Date,
                Description = x.Description,
                EventId = x.EventId,
                Price = x.Price
            }));
        }
    }
}
