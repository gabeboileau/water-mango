using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace water_mango_api.Services.EventServices
{
    public class PlantHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            Console.Error.Write("We were sent a message by user: " + user + " \n " + message);

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public void sendPlantUpdateEvent(PlantUpdateEvent plantUpdateEvent)
        {
            if (Clients != null)
            {
                Clients.All.SendAsync("updatePlant", plantUpdateEvent);
            }
        }
    }
}
