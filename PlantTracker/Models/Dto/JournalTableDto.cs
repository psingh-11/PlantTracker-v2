using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantTracker.Models.Dto
{
    public class JournalTableDto
    {
        public string JournalId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}