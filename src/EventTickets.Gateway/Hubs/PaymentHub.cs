using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTickets.Gateway;

namespace EventTickets.Gateway.Hubs
{
    public class PaymentHub : Hub
    {
        private readonly Servicios.IEventCatalogService service;

        public PaymentHub(Servicios.IEventCatalogService incomingService)
        {
            this.service = incomingService;
        }

        public async Task SendPayment(Guid basketId)
        {
            EventTickets.Gateway.Servicios.IEventCatalogService service;
            var controller = new Controllers.EventTicketsController(this.service);
            Microsoft.AspNetCore.Mvc.ActionResult actionResult = await controller.PostPayment(basketId);

        }
    }
}
