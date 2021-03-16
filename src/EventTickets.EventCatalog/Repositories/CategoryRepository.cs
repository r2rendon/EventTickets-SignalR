using EventTickets.EventCatalog.Contexts;
using EventTickets.EventCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EventCatalogDbContext _context;
        public CategoryRepository(EventCatalogDbContext context)
        {
            this._context = context;
        }
        public async Task<IReadOnlyList<Category>> GetAsync()
        {
            return await this._context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        }
    }
}
