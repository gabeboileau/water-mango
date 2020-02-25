using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango.contracts.Models
{
    public class Plant
    {
        public Plant() { }
        public Plant(long id, String name, DateTime lastWatered)
        {
            this.Id = id;
            this.Name = name;

            // setting the default last watered to now.
            this.LastWatered = lastWatered;

            // when a plant is created by the Lord - it's idle of course.
            this.State = PlantState.Idle;
        }

        public long Id { get; set; }

        public String Name { get; set; }

        public DateTime LastWatered { get; set; }

        public PlantState State { get; set; }



    }
}
