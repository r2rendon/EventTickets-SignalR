using EventTickets.ShoppingBasket.Entities;
using EventTickets.ShoppingBasket.Models;
using EventTickets.ShoppingBasket.Repositories;
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
    public class ShoppingBasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public ShoppingBasketsController(IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository;
        }

        [HttpGet("{basketId}")]
        public async Task<ActionResult<BasketDto>> Get(Guid basketId)
        {
            var basket = await _basketRepository.GetBasketByIdAsync(basketId);
            if (basket == null)
            {
                return NotFound($"The basket with id {basketId} was not found.");
            }

            var result = new BasketDto
            {
                BasketId = basket.BasketId,
                NumberOfItems = basket.BasketLines.Sum(x => x.TicketAmount),
                UserId = basket.UserId
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> Post([FromBody] CreateBasketModel basketForCreation)
        {
            var basket = new Basket
            {
                UserId = basketForCreation.UserId
            };

            await _basketRepository.AddBasketAsync(basket);
            return Ok(new BasketDto
            {
                BasketId = basket.BasketId,
                UserId = basket.UserId
            });
        }
    }
}
