using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango_api.Services.Arguments
{
    public class CreatePlantArguments
    {
        public CreatePlantArguments(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
