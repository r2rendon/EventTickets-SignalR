using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventTickets.EventCatalog.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        public string Name { get; set; }

        public List<Event> Events { get; set; }
    }
}
