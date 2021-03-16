using EventTickets.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.Gateway.Servicios
{
    interface IShoppingBasketService
    {
        Task<BasketDto> CreateBasket(CreateBasketModel basket);
    }
}
