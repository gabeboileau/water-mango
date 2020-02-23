using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango_api.Services.Response
{
    public class WaterPlantFailed : WaterPlantResponse
    {
        public WaterPlantFailed(string errorMessage) : base(false, errorMessage) { }
    }
}
