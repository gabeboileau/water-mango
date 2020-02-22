using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using water_mango_api.Models;

namespace water_mango_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {


        [HttpGet]
        public Plant Get()
        {
            return new Plant("Something");
        }

    }
}