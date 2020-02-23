using System;
using System.Collections.Generic;
using System.Text;

namespace water_mango.contracts.DTO
{
    public class CreatePlantDTO
    {
        // Empty constructor
        public CreatePlantDTO() { }

        public CreatePlantDTO(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
