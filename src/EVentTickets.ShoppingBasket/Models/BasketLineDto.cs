using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Models
{
    public class BasketLineDto
    {
        public Guid BasketLineId { get; set; }

        public Guid BasketId { get; set; }

        public Guid EventId { get; set; }

        public int Price { get; set; }

        public int TicketAmount { get; set; }

        public EventDto Event { get; set; }
    }
}
