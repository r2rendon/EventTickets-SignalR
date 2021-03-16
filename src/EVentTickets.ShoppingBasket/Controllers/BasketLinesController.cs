using EventTickets.ShoppingBasket.Entities;
using EventTickets.ShoppingBasket.Models;
using EventTickets.ShoppingBasket.Repositories;
using EventTickets.ShoppingBasket.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketLinesController : ControllerBase
    {
        private readonly IBasketLineRepository _basketLineRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventCatalogService _eventCatalogService;

        public BasketLinesController(
            IBasketLineRepository basketLineRepository,
            IBasketRepository basketRepository,
            IEventRepository eventRepository,
            IEventCatalogService eventCatalogService)
        {
            this._basketLineRepository = basketLineRepository;
            this._basketRepository = basketRepository;
            this._eventRepository = eventRepository;
            this._eventCatalogService = eventCatalogService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketLineDto>>> Get([FromQuery] Guid basketId)
        {
            if (!await _basketRepository.BasketExistsAsync(basketId))
            {
                return NotFound();
            }

            var basketLines = await _basketLineRepository.GetBasketLinesAsync(basketId);
            var result = basketLines.Select(x => new BasketLineDto 
            {
                BasketId = basketId,
                BasketLineId = x.BasketLineId,
                EventId = x.EventId,
                Price = x.Price,
                TicketAmount = x.TicketAmount,
                Event = new EventDto
                {
                    EventId = x.Event.EventId,
                    Date = x.Event.Date,
                    Name = x.Event.Name
                }
            });

            return Ok(result);
        }

        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketLineDto>> Post(Guid basketId, [FromBody] CreateBasketLineModel basketLineForCreation)
        {
            if (!await _basketRepository.BasketExistsAsync(basketId))
            {
                return NotFound();
            }

            if (!await _eventRepository.EventExistsAsync(basketLineForCreation.EventId))
            {
                var eventFromCatalog = await _eventCatalogService.GetEventAsync(basketLineForCreation.EventId);
                await _eventRepository.AddEventAsync(eventFromCatalog);
            }

            var basket = new BasketLine
            {
                Price = basketLineForCreation.Price,
                TicketAmount = basketLineForCreation.TicketAmount,
                EventId = basketLineForCreation.EventId
            };
            var processedLine = await _basketLineRepository.AddOrUpdateAsync(basketId, basket);

            return Ok(processedLine);
        }
    }
}
