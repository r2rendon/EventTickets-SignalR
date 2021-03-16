using EventTickets.Gateway.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventTickets.Gateway.Servicios
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient _httpClient;

        public EventCatalogService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _httpClient.GetStringAsync($"/api/categories");
            return JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<IEnumerable<EventDto>> GetEventByCategoryAsync(Guid categoryId)
        {
            var events = await _httpClient.GetStringAsync($"/api/events?categoryId={categoryId}");
            return JsonConvert.DeserializeObject<IEnumerable<EventDto>>(events);
        }

        public async Task<EventDto> GetEventByIdAsync(Guid id)
        {
            var events = await _httpClient.GetStringAsync($"/api/events/{id}");
            return JsonConvert.DeserializeObject<EventDto>(events);
        }
    }
}
