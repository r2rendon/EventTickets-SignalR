using EventTickets.Gateway.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventTickets.Gateway.Servicios
{
    public class ShoppingBasketService : IShoppingBasketService
    {
        private readonly HttpClient _httpClient;

        public ShoppingBasketService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<BasketDto> CreateBasket(CreateBasketModel basket)
        {
            var result = await _httpClient.PostAsync($"/api/ShoppingBaskets", new StringContent(JsonConvert.SerializeObject(basket)));
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BasketDto>(response);
        }
    }
}
