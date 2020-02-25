using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using water_mango.contracts.Models;

namespace water_mango_api.Services.Response
{
    public class WaterPlantSuccess : WaterPlantResponse
    {
        public WaterPlantSuccess(Plant plant) : base(true, "")
        {
            base.Plant = plant;
        }
    }
}
