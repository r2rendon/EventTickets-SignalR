using EventTickets.ShoppingBasket.Contexts;
using EventTickets.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public EventRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this._shoppingBasketDbContext = shoppingBasketDbContext;
        }
        public async Task AddEventAsync(Event @event)
        {
            await _shoppingBasketDbContext.AddAsync(@event);
            await _shoppingBasketDbContext.SaveChangesAsync();
        }

        public async Task<bool> EventExistsAsync(Guid eventId)
        {
            return await _shoppingBasketDbContext.Events.AnyAsync(x => x.EventId == eventId);
        }
    }
}
