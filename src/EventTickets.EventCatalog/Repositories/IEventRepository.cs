using EventTickets.EventCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetEventByIdAsync(Guid eventId);
        Task<IReadOnlyList<Event>> GetEventsByCategoryIdAsync(Guid categoryId);
    }
}
