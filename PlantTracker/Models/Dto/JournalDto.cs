using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantTracker.Models.Dto
{
    public class JournalDto
    {
        public System.Guid ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public System.Guid PlantId { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }

        public List<HttpPostedFileBase> Images { get; set; } = new List<HttpPostedFileBase>();
        public List<string> imageFilePath { get; set; }

        public List<SelectListItem> Plants { get; set; } = new List<SelectListItem>();
    }
}