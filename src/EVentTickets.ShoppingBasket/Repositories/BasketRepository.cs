using EventTickets.ShoppingBasket.Contexts;
using EventTickets.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShoppingBasketDbContext _shoppingBasketDbContext;

        public BasketRepository(ShoppingBasketDbContext shoppingBasketDbContext)
        {
            this._shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task AddBasketAsync(Basket basket)
        {
            basket.BasketId = Guid.NewGuid();
            await _shoppingBasketDbContext.AddAsync(basket);
            await _shoppingBasketDbContext.SaveChangesAsync();
        }

        public async Task<bool> BasketExistsAsync(Guid basketId)
        {
            return await _shoppingBasketDbContext.Baskets.AnyAsync(x => x.BasketId == basketId);
        }

        public async Task<Basket> GetBasketByIdAsync(Guid basketId)
        {
            return await _shoppingBasketDbContext.Baskets.Include(x => x.BasketLines).SingleOrDefaultAsync(x => x.BasketId == basketId);
        }
    }
}
