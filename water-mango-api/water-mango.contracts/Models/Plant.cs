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
            this.id = id;
            this.name = name;

            // setting the default last watered to now.
            this.lastWatered = lastWatered;
        }

        public long id { get; set; }

        public String name { get; set; }

        public DateTime lastWatered { get; set; }




    }
}
