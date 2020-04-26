﻿using PlantDAL.EDMX;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantTracker.Models.Dto
{
    public class PlantDto
    {

        public System.Guid ID { get; set; }
        public string UserID { get; set; }
        public string ParentOneID { get; set; }
        public string ParentTwoID { get; set; }

        public List<SelectListItem> Plants { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Journals { get; set; } = new List<SelectListItem>();

        public string Name { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
        public string Genus { get; set; }
        public string Species { get; set; }
        public string SubSpecies { get; set; }
        public string Notes { get; set; }

        public PlantDto()
        {
            Images = new List<HttpPostedFileBase>();
        }

        public List<HttpPostedFileBase> Images { get; set; }
        public List<string> imageFilePath { get; set; }

        public CustomValueDto CustomValues1 { get; set; }
        public CustomValueDto CustomValues2 { get; set; }
        public CustomValueDto CustomValues3 { get; set; }
        public CustomValueDto CustomValues4 { get; set; }
        public CustomValueDto CustomValues5 { get; set; }


    }
}