using EventTickets.ShoppingBasket.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventTickets.ShoppingBasket.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient _httpClient;

        public EventCatalogService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<Event> GetEventAsync(Guid eventId)
        {
            var response = await _httpClient.GetStringAsync($"/api/events/{eventId}");
            return JsonConvert.DeserializeObject<Event>(response);
        }
    }
}
