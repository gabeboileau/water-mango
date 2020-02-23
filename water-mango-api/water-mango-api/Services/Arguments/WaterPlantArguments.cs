using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango_api.Services.Arguments
{
    public class WaterPlantArguments
    {
        public WaterPlantArguments(long id)
        {
            Id = id;
        }

        public long Id { get; set; }
    }
}
