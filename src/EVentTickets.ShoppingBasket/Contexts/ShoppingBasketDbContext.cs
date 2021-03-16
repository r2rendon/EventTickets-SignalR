using EventTickets.ShoppingBasket.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Contexts
{
    public class ShoppingBasketDbContext : DbContext
    {
        public ShoppingBasketDbContext(DbContextOptions<ShoppingBasketDbContext> options) :
            base(options)
        {

        }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<BasketLine> BasketLines { get; set; }

        public DbSet<Event> Events { get; set; }

    }
}
