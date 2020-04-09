using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantTracker.Models.Dto
{
    public class PlantTableDto
    {
        public string PlantId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public string Type { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
    }
}