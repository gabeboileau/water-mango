using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango_api.Models
{
    public class Plant
    {
        public Plant(String name)
        {
            this.name = name;

            // setting the default last watered to now.
            this.lastWatered = DateTime.Now;
        }

        public String name { get; set; }

        public DateTime lastWatered { get; set; }

    }
}
