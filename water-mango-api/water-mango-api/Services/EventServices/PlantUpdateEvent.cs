using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace water_mango_api.Services.EventServices
{
    [Serializable]
    public class PlantUpdateEvent
    {
        public long Id { get; set; }

        public PlantUpdateEvent() { }

        public PlantUpdateEvent(long id)
        {
            this.Id = id;
        }
    }
}
