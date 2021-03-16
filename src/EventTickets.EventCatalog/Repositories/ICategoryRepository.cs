using EventTickets.EventCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Repositories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAsync();

        Task<Category> GetByIdAsync(Guid categoryId);
    }
}
