using EventTickets.EventCatalog.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Contexts
{
    public class EventCatalogDbContext : DbContext
    {
        public EventCatalogDbContext(DbContextOptions<EventCatalogDbContext> options) :
            base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var concertGuid = Guid.NewGuid();
            var musicalGuid = Guid.NewGuid();
            var playGuid = Guid.NewGuid();
            //seeding
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = concertGuid,
                Name = "Conciertos"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = musicalGuid,
                Name = "Musicales"
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = playGuid,
                Name = "Obras"
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.NewGuid(),
                Artist = "Juanes",
                CategoryId = concertGuid,
                Date = DateTime.Now.AddMonths(1),
                Description = "Concierto Juanes",
                Name = "Concierto Juanes",
                Price = 100
            });

            modelBuilder.Entity<Event>().HasData(new Event
            {
                EventId = Guid.NewGuid(),
                Artist = "William Shakespear",
                CategoryId = playGuid,
                Date = DateTime.Now.AddMonths(2),
                Description = "Hamlet",
                Name = "Hamlet",
                Price = 100
            });

        }
    }
}
