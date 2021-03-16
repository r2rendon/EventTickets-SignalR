using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Models
{
    public class EventDto
    {
        public Guid EventId { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }
    }
}
