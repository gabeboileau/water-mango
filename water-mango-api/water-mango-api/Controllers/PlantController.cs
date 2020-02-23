using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using water_mango_api.Services;
using water_mango.contracts.Models;
using water_mango.contracts.DTO;
using AutoMapper;

namespace water_mango_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly PlantService service;
        private readonly IMapper mapper;

        public PlantController(PlantService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }


        [HttpGet]
        public List<PlantDTO> Get()
        {
            List<Plant> plants = service.GetAllPlants();
            return mapper.Map<List<PlantDTO>>(plants);
        }

        [HttpGet("{id}")]
        public ActionResult<PlantDTO> GetPlant(long id)
        {
            if (id < 0)
            {
                // this is not a valid identifier - we could do more, but alas, we will not. 
                return NotFound();
            }

            Plant plant = service.GetPlantById(id);
            if (plant == null)
            {
                return NotFound();
            }

            return mapper.Map<PlantDTO>(plant);
        }

        [HttpPost]
        public ActionResult<PlantDTO> WaterPlant([FromBody] CreatePlantDTO plant)
        {
            Console.Error.Write("content" + plant.Name);
            return Accepted(new Plant(192, plant.Name, DateTime.Now));
        }

    }
}