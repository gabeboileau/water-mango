using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using water_mango.contracts.Models;

namespace water_mango_api.Services
{
    public class PlantService
    {
        // Contant value that represents how long it takes to water a plant in SECONDS.
        public const int WATER_TIME = 10;

        // Constant value that represents how long the plant needs to rest before being watered in SECONDS.
        public const int REST_TIME = 30;

        // Constant value that represents when the user wants to be alerted about the plant because it hasn't been watered in HOURS.
        public const int PLANT_ALERT_TIME = 6;
        
        // list of plants in memory until we can get the state going
        private List<Plant> plants;

        public PlantService()
        {
            int amountOfPlants = 5;
            var plants = new List<Plant>();
            for (int i = 0; i < amountOfPlants; i++)
            {
                plants.Add(new Plant(i, "My name is generic : " + i, DateTime.Now));
            }

            this.plants = plants;

        }

        /// <summary>
        /// Gets all the plants.
        /// </summary>
        /// <returns></returns>
        public List<Plant> GetAllPlants() { return plants; }


        /// <summary>
        /// Gets the plant by identifier. Returns NULL if it's not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Plant GetPlantById(long id)
        {
            foreach (Plant p in plants) 
            {
                if (p.id == id)
                {
                    return p;
                }
            }

            return null;
        }
    }
}
