using Microsoft.AspNet.Identity;
using PlantDAL.EDMX;
using PlantDAL.Repository;
using PlantTracker.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantTracker.Controllers.Plant
{
    public class PlantViewController : Controller
    {
        [HttpGet]
        public ActionResult PlantDisplay(string PlantID)
        {
            var curUrl = ConfigurationManager.AppSettings["url"];
            curUrl = curUrl.TrimEnd('/');

            PlantDAL.EDMX.Plant plant = PlantCRUD.GetByID(Guid.Parse(PlantID));
            PlantDto dto = Mappers.PlantMapper.MapDALToDto(plant);

            List<Images> imgs = ImageCRUD.GetByPlantID(Guid.Parse(PlantID));

            foreach (var img in imgs)
            {
                var idx = img.ImageFilePath.ToLower().IndexOf(@"\images\");
                if (dto.imageFilePath == null)
                    dto.imageFilePath = new List<string>();
                if (idx != -1)
                {
                    var imgpath = curUrl + img.ImageFilePath.Substring(idx).Replace("\\", "/");
                    dto.imageFilePath.Add(imgpath);
                }
            }


            List<PlantDAL.EDMX.Plant> plantList = new List<PlantDAL.EDMX.Plant>();

            plantList = PlantCRUD.GetByUserID(User.Identity.GetUserId());
            foreach (var plt in plantList)
            {
                dto.Plants.Add(new SelectListItem
                {
                    Text = plt.Name,
                    Value = plt.ID.ToString()
                });
            }

            if (plant.CustomValueOneID != null || plant.CustomValueOneID == Guid.Empty)
            {
                plant.CustomValues = CustomValueCRUD.GetByID(plant.CustomValueOneID);
                dto.CustomValues1 = Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues, 1) as CustomValueDto;
            }
            if (plant.CustomValueTwoD != null || plant.CustomValueOneID == Guid.Empty)
            {
                plant.CustomValues1 = CustomValueCRUD.GetByID(plant.CustomValueTwoD);
                dto.CustomValues2 = Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues1, 2) as CustomValueDto;
            }
            if (plant.CustomValueThreeID != null || plant.CustomValueThreeID == Guid.Empty)
            {
                plant.CustomValues2 = CustomValueCRUD.GetByID(plant.CustomValueThreeID);
                dto.CustomValues3 = Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues2, 3) as CustomValueDto;
            }
            if (plant.CustomValueFourID != null || plant.CustomValueFourID == Guid.Empty)
            {
                plant.CustomValues3 = CustomValueCRUD.GetByID(plant.CustomValueFourID);
                dto.CustomValues4 = Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues3, 4) as CustomValueDto;
            }
            if (plant.CustomValueFiveID != null || plant.CustomValueFiveID == Guid.Empty)
            {
                plant.CustomValues4 = CustomValueCRUD.GetByID(plant.CustomValueFiveID);
                dto.CustomValues5 = Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues4, 5) as CustomValueDto;
            }

            return View(dto);
        }
    }
}