using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using water_mango.contracts.Models;

namespace water_mango_api.Services.Response
{
    public abstract class WaterPlantResponse
    {
        public WaterPlantResponse(bool successful, string errorMessage)
        {
            this.Successful = successful;
            this.ErrorMessage = errorMessage;
        }

        public bool Successful { get; set; }
        
        public string ErrorMessage { get; set; }

        public Plant Result { get; set; }
    }
}
