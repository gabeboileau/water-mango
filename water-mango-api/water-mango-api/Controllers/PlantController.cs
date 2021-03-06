﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using water_mango_api.Services;
using water_mango.contracts.Models;
using water_mango.contracts.DTO;
using AutoMapper;
using water_mango_api.Services.Response;
using System.Threading;

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

        /// <summary>
        /// GET Root endpoint that returns all the plants.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<PlantDTO> Get()
        {
            List<Plant> plants = service.GetAllPlants();
            return mapper.Map<List<PlantDTO>>(plants);
        }

        /// <summary>
        /// GET Endpoint that returns a single plant found by it's identifier.
        /// 
        /// Returns a 404 NotFound if the id is invalid (< 0) or if it's not found in the service.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// POST Endpoint that creates plant.
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<PlantDTO> CreatePlant([FromBody] CreatePlantDTO plant)
        {
            Plant newPlant = service.CreatePlant(new Services.Arguments.CreatePlantArguments(plant.Name));
            return Accepted(mapper.Map<PlantDTO>(newPlant));
        }

        /// <summary>
        /// POST Endpoint that waters a plant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="waterPlant"></param>
        /// <returns></returns>
        [HttpPost("water/{id}")]
        public ActionResult<WaterPlantResponseDTO> WaterPlant(long id)
        {
            WaterPlantResponseDTO responseDTO = new WaterPlantResponseDTO();

            Console.Error.Write(Thread.CurrentThread);
            //using (Timer timer = new Timer(new TimerCallback((c) =>
            //{
            //    Console.Error.Write("what the fuck");
            //}), null, 3000, 1000))
            //{

            //    // Wait for 10 seconds
            //    Thread.Sleep(10000);

            //    // Then go slow for another 10 seconds
            //    timer.Change(0, 2000);
            //    Thread.Sleep(10000);
            //}

            WaterPlantResponse response = service.WaterPlant(new Services.Arguments.WaterPlantArguments(id));
            if (response.Successful)
            {
                responseDTO.Plant = mapper.Map<PlantDTO>(response.Plant);
            }

            responseDTO.ErrorMessage = response.ErrorMessage;
            responseDTO.Successful = response.Successful;
            return Accepted(responseDTO);
        }

        /// <summary>
        /// POST endpoint that stops watering the plant. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("stopWatering/{id}")]
        public ActionResult<WaterPlantResponseDTO> StopWateringPlant(long id)
        {
            WaterPlantResponseDTO responseDTO = new WaterPlantResponseDTO();

            WaterPlantResponse response = service.StopWateringPlant(new Services.Arguments.StopWateringPlantArguments(id));
            if (response.Successful)
            {
                responseDTO.Plant = mapper.Map<PlantDTO>(response.Plant);
            }

            responseDTO.ErrorMessage = response.ErrorMessage;
            responseDTO.Successful = response.Successful;
            return Accepted(responseDTO);
        }

    }
}