using EventTickets.Gateway.Models;
using EventTickets.Gateway.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTickets.Gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTicketsController : ControllerBase
    {
        private readonly IEventCatalogService _catalogService;

        public EventTicketsController(IEventCatalogService catalogService)
        {
            this._catalogService = catalogService;
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var result = await _catalogService.GetCategoriesAsync();
            return Ok(result.Select(x => new CategoryDto
            {
                CategoryId = x.CategoryId,
                Name = x.Name
            }));
        }

        [HttpGet("events")]
        public async Task<ActionResult<IEnumerable<EventDto>>> Get([FromQuery] Guid categoryId)
        {
            var result = await _catalogService.GetEventByCategoryAsync(categoryId);
            return Ok(result.Select(x => new EventDto
            {
                Artist = x.Artist,
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Name = x.Name,
                Date = x.Date,
                Description = x.Description,
                EventId = x.EventId,
                Price = x.Price
            }));
        }

        [HttpPost("payment/{basketId}")]
        public async Task<ActionResult> PostPayment(Guid basketId)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672
                };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("send-payment", false, false, false, null);
                        var body = Encoding.UTF8.GetBytes(basketId.ToString());

                        channel.BasicPublish("", "send-payment", null, body);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
