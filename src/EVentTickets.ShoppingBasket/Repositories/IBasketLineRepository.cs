using EventTickets.ShoppingBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Repositories
{
    public interface IBasketLineRepository
    {
        Task<IReadOnlyList<BasketLine>> GetBasketLinesAsync(Guid basketId);

        Task<BasketLine> AddOrUpdateAsync(Guid basketId, BasketLine basketLine);
    }
}
