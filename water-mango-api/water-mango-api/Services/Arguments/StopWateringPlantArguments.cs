using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango_api.Services.Arguments
{
    public class StopWateringPlantArguments
    {
        public long Id { get; set; }

        public StopWateringPlantArguments(long id)
        {
            Id = id;
        }
    }
}
