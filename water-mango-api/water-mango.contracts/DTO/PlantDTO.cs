using System;
using System.Collections.Generic;
using System.Text;

namespace water_mango.contracts.DTO
{ 
    public class PlantDTO
    {
        // Empty constructor
        public PlantDTO() { }

        public PlantDTO(long id, string name, DateTime lastWatered)
        {
            this.Id = id;
            this.Name = name;
            this.LastWatered = lastWatered;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime LastWatered { get; set; }
    }

}
