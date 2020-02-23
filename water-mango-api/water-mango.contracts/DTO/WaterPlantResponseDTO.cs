using System;
using System.Collections.Generic;
using System.Text;

namespace water_mango.contracts.DTO
{
    public class WaterPlantResponseDTO
    {
        public WaterPlantResponseDTO() { }
        public WaterPlantResponseDTO(PlantDTO plant, string errorMessage, bool successful)
        {
            Plant = plant;
            ErrorMessage = errorMessage;
            Successful = successful;
        }

        public PlantDTO Plant { get; set; }

        public string ErrorMessage { get; set; }
        
        public bool Successful { get; set; }
    }
}
