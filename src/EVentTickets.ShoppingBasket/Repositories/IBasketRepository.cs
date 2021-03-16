using EventTickets.ShoppingBasket.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Repositories
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasketByIdAsync(Guid basketId);

        Task AddBasketAsync(Basket basket);

        Task<bool> BasketExistsAsync(Guid basketId);
    }
}
