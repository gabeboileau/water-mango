using System;
using System.Collections.Generic;
using System.Text;
using water_mango.contracts.Models;

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

        // Normally there would be a separate state enum - but for the sake of keeping it less complex, no. 
        public PlantState State { get; set; }
    }

}
