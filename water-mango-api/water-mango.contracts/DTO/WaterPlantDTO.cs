using System;
using System.Collections.Generic;
using System.Text;

namespace water_mango.contracts.DTO
{
    public class WaterPlantDTO
    {
        // Empty constructor
        public WaterPlantDTO() { }

        public WaterPlantDTO(long id)
        {
            this.Id = id;
        }

        public long Id { get; set; }
    }
}
