using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using water_mango.contracts.DTO;
using water_mango.contracts.Models;

namespace water_mango_api.Mappings
{
    public class BasicMappings : Profile
    {
        public BasicMappings()
        {
            // Create a map for our plant -> plant dto
            CreateMap<Plant, PlantDTO>();
        }
    }
}
