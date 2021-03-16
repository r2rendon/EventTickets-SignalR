using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Models
{
    public class BasketDto
    {
        public Guid BasketId { get; set; }

        public Guid UserId { get; set; }

        public int NumberOfItems { get; set; }
    }
}
