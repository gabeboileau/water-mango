using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using water_mango.contracts.Models;
using water_mango_api.Services.Arguments;
using water_mango_api.Services.Response;

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

        private readonly object plantLock = new object();

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

        /// <summary>
        /// Creates a new plant with the given arguments.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public Plant CreatePlant(CreatePlantArguments args)
        {
            Plant newPlant = new Plant();
            
            // TODO: The id conter should be generated or handled by the DB
            newPlant.id = plants.Count + 1;
            newPlant.name = args.Name;
            newPlant.lastWatered = DateTime.Now;

            plants.Add(newPlant);
            return newPlant;
        }

        public WaterPlantResponse WaterPlant(WaterPlantArguments args)
        {
            Plant plant = GetPlantById(args.Id);
            if (plant == null)
            {
                return new WaterPlantFailed(String.Format("No plant with the Id: {0} was found..", args.Id));
            }

            // Ensure that the plant is not already being watered.
            if (plant.State.Equals(PlantState.Watering))
            {
                return new WaterPlantFailed(String.Format("{0} is currently being watered!", plant.name));
            }
            else if (plant.State.Equals(PlantState.Cooldown))
            {
                return new WaterPlantFailed(String.Format("{0}  plant has needs to chill for a bit", plant.name));
            }

            // we've validated that the plant exists and it's not currently being watered or on cooldown
            plant.State = PlantState.Watering;

            Task.Run(() => WaterPlant(plant));

            return new WaterPlantSuccess(plant);
        }

        private async Task<Plant> WaterPlant(Plant plant)
        {
            // watering for 30 seconds 
            Console.Error.Write("We're watering the plant...");
            Thread.Sleep(new TimeSpan(0, 0, 30));
            Console.Error.Write("We're done watering the plant!");

            return plant;
        }
    }
}
