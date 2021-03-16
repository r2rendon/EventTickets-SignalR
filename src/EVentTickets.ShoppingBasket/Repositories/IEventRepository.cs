using EventTickets.ShoppingBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Repositories
{
    public interface IEventRepository
    {
        Task<bool> EventExistsAsync(Guid eventId);

        Task AddEventAsync(Event @event);
    }
}
