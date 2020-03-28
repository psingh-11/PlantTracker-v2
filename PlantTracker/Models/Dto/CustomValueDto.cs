using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantTracker.Models.Dto
{
    public class CustomValueDto
    {
        public Nullable<System.Guid> ID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
    }
}