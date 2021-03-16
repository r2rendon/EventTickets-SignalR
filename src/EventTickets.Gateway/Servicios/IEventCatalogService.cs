using EventTickets.Gateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.Gateway.Servicios
{
    public interface IEventCatalogService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

        Task<EventDto> GetEventByIdAsync(Guid id);

        Task<IEnumerable<EventDto>> GetEventByCategoryAsync(Guid categoryId);
    }
}
