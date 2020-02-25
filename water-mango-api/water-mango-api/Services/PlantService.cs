using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using water_mango.contracts.Models;
using water_mango_api.Services.Arguments;
using water_mango_api.Services.EventServices;
using water_mango_api.Services.Response;

namespace water_mango_api.Services
{
    public class PlantService
    {
        // Contant value that represents how long it takes to water a plant in SECONDS.
        public const int WATER_TIME = 10;

        // Constant value that represents how long the plant needs to rest before being watered in SECONDS.
        public const int COOLDOWN_TIME = 30;

        // Constant value that represents when the user wants to be alerted about the plant because it hasn't been watered in HOURS.
        public const int PLANT_ALERT_TIME = 6;

        // the plants list lock
        private readonly object plantLock = new object();

        // list of plants in memory until we can get the state going
        private List<Plant> plants;

        // object lock for the task dictionary
        private readonly object taskLock = new object();

        private Dictionary<long, Timer> needsWateringTimers = new Dictionary<long, Timer>();

        // dictionary of a plant id and the task that currently watering it
        private Dictionary<long, CancellationTokenSource> wateringTasks = new Dictionary<long, CancellationTokenSource>();

        // dependencies
        private PlantHub plantHub;

        public PlantService(PlantHub plantHub)
        {
            this.plantHub = plantHub;
            
            this.plants = new List<Plant>();

            int amountOfPlants = 5;
            for (int i = 0; i < amountOfPlants; i++)
            {
                Plant plant = new Plant(i, "Plant Boi # " + i, DateTime.Now);
                plants.Add(plant);

                needsWateringTimers.Add(plant.Id, new Timer((id) =>
                {
                    Console.WriteLine("Yo YO");
                    plant.State = PlantState.ThirstyAF;
                    plantHub.sendPlantUpdateEvent(new PlantUpdateEvent(plant.Id));

                }, plant.Id, TimeSpan.FromHours(PLANT_ALERT_TIME), TimeSpan.FromHours(PLANT_ALERT_TIME)));
            }
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
                if (p.Id == id)
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
        public Plant CreatePlant(CreatePlantArguments args)
        {
            Plant newPlant = new Plant();
            
            // TODO: The id conter should be generated or handled by the DB
            newPlant.Id = plants.Count + 1;
            newPlant.Name = args.Name;
            newPlant.LastWatered = DateTime.Now;

            lock (plantLock)
            {
                plants.Add(newPlant);
            }

            return newPlant;
        }

        public WaterPlantResponse StopWateringPlant(StopWateringPlantArguments args)
        {
            Plant plant = GetPlantById(args.Id);
            if (plant == null)
            {
                return new WaterPlantFailed(String.Format("No plant with the Id: {0} was not found..", args.Id));
            }

            if (!plant.State.Equals(PlantState.Watering))
            {
                // we're not watering. We can't stop.
                return new WaterPlantFailed(String.Format("The plant with id {0} is not currently being watered. We can't like, idk, not water?..", args.Id));
            }

            if (wateringTasks.ContainsKey(args.Id))
            {
                // there is a task currently running for watering the plant with this id
                CancellationTokenSource tokenSource = wateringTasks.GetValueOrDefault(args.Id, null);
                if (tokenSource != null)
                {
                    tokenSource.Cancel();
                    lock (taskLock)
                    {
                        // remove this item from the dictionary
                        wateringTasks.Remove(args.Id);
                    }

                    return new WaterPlantSuccess(plant);
                }
            }
            else
            {
                return new WaterPlantFailed(String.Format("No plant task is currently running with id {0}..", args.Id));
            }

            return new WaterPlantFailed(String.Format("Failed to stop watering plant with id {0}", args.Id));
        }

        public WaterPlantResponse WaterPlant(WaterPlantArguments args)
        {
            Plant plant = GetPlantById(args.Id);
            if (plant == null)
            {
                return new WaterPlantFailed(String.Format("No plant with the Id: {0} was not found..", args.Id));
            }

            // Ensure that the plant is not already being watered.
            if (plant.State.Equals(PlantState.Watering))
            {
                return new WaterPlantFailed(String.Format("{0} is currently being watered!", plant.Name));
            }
            else if (plant.State.Equals(PlantState.Cooldown))
            {
                return new WaterPlantFailed(String.Format("{0} plant needs to chill for a bit", plant.Name));
            }

            // we've validated that the plant exists and it's not currently being watered or on cooldown
            plant.State = PlantState.Watering;

            // send an event to all clients to update the plant with this id
            plantHub.sendPlantUpdateEvent(new PlantUpdateEvent(args.Id));

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            Task wateringTask = Task.Run(() => WaterPlant(plant, token), token);
            
            lock (taskLock)
            {
                wateringTasks.Add(plant.Id, tokenSource);
            }

            return new WaterPlantSuccess(plant);
        }

        private Plant WaterPlant(Plant plant, CancellationToken token)
        {
            int totalTime = 0;
            while(!token.IsCancellationRequested && totalTime < WATER_TIME)
            {
                Thread.Sleep(new TimeSpan(0, 0, 1));
                totalTime++;
            }

            if (token.IsCancellationRequested)
            {
                // we've canceled
                plant.State = PlantState.Idle;
                plantHub.sendPlantUpdateEvent(new PlantUpdateEvent(plant.Id));
                return plant;
            }

            plant.LastWatered = DateTime.Now;
            plant.State = PlantState.Cooldown;

            // change the timer for the plant that was just watered
            if (needsWateringTimers.ContainsKey(plant.Id))
            {
                Timer t = needsWateringTimers.GetValueOrDefault(plant.Id, null);
                if (t != null)
                {
                    t.Change(TimeSpan.FromHours(PLANT_ALERT_TIME), TimeSpan.FromHours(PLANT_ALERT_TIME));
                }
            }

            // send an event to all clients to update the plant with this id
            plantHub.sendPlantUpdateEvent(new PlantUpdateEvent(plant.Id));

            // start the cooldown
            Task.Run(() => CooldownPlant(plant));

            // we're done watering - so remove this task from the list
            lock (taskLock)
            {
                wateringTasks.Remove(plant.Id);
            }

            return plant;
        }

        private Plant CooldownPlant(Plant plant)
        {
            // cooldown for COOLDOWN_TIME
            Thread.Sleep(new TimeSpan(0, 0, COOLDOWN_TIME));

            plant.State = PlantState.Idle;

            // send an event to all clients to update the plant with this id
            plantHub.sendPlantUpdateEvent(new PlantUpdateEvent(plant.Id));

            return plant;
        }
    }
}
