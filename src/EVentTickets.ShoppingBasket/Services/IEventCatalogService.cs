using EventTickets.ShoppingBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Services
{
    public interface IEventCatalogService
    {
        Task<Event> GetEventAsync(Guid eventId);
    }
}
