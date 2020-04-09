using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantDAL.Repository;
using PlantDAL.EDMX;
using PlantTracker.Models.Dto;
using System.IO;

namespace PlantTracker.Controllers
{
    [Authorize]
    public class PlantController : Controller
    {
        [HttpGet]
        public ActionResult NewPlant()
        {
            var plantDto = new PlantDto();
            List<Plant> plantList = new List<Plant>();

            plantList = PlantCRUD.GetByUserID(User.Identity.GetUserId());
            foreach(var plant in plantList)
            {
                plantDto.Plants.Add(new SelectListItem
                {
                    Text = plant.Name,
                    Value = plant.ID.ToString()
                });
            }

            return View(plantDto);
        }

        [HttpPost]
        public ActionResult NewPlant(PlantDto plantDto)
        {
            Guid id = Guid.NewGuid();
            plantDto.ID = id;
            plantDto.UserID = User.Identity.GetUserId();

            var plantDir = Server.MapPath("~/Images/Plant/" + id.ToString());
            List<Images> imgList = Mappers.ImageMapper.MapHTTPToImage(plantDto.Images, plantDto, plantDir);
            Plant plant = Mappers.PlantMapper.MapDtoToDAL(plantDto, imgList);

            if (plantDto.CustomValues1 != null)
            {
                plantDto.CustomValues1.ID = Guid.NewGuid();
                CustomValues cv1 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues1, 1) as CustomValues;
                plant.CustomValues = cv1;
            }
            if (plantDto.CustomValues2 != null)
            {
                plantDto.CustomValues2.ID = Guid.NewGuid();
                CustomValues cv2 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues2, 2) as CustomValues;
                plant.CustomValues1 = cv2;
            }
            if (plantDto.CustomValues3 != null)
            {
                plantDto.CustomValues3.ID = Guid.NewGuid();
                CustomValues cv3 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues3, 3) as CustomValues;
                plant.CustomValues2 = cv3;
            }
            if (plantDto.CustomValues4 != null)
            {
                plantDto.CustomValues4.ID = Guid.NewGuid();
                CustomValues cv4 =  Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues4, 4) as CustomValues;
                plant.CustomValues3 = cv4;
            }
            if (plantDto.CustomValues5 != null)
            {
                plantDto.CustomValues5.ID = Guid.NewGuid();
                CustomValues cv5 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues5, 5) as CustomValues;
                plant.CustomValues4 = cv5;
            }

            PlantCRUD.Insert(plant);


            return RedirectToAction("PlantTable");
        }

        [HttpPost]
        public ActionResult GetPlantDetails(string PlantID)
        {
            return RedirectToAction("PlantDetails", new { plantId = PlantID });
        }

        [HttpGet]
        public ActionResult PlantDetails(string plantId)
        {
            Plant plant = PlantCRUD.GetByID(Guid.Parse(plantId));
            PlantDto dto = Mappers.PlantMapper.MapDALToDto(plant);

            List<Images> imgs = ImageCRUD.GetByPlantID(Guid.Parse(plantId));
 
            foreach(var img in imgs)
            {
                var idx = img.ImageFilePath.ToLower().IndexOf(@"\images\");
                if(idx != -1)
                {
                    var imgpath = "http://localhost:49592" + img.ImageFilePath.Substring(idx).Replace("\\", "/");
                    dto.imageFilePath.Add(imgpath);
                }
            }
               
            return View(dto);
        }



        [HttpPost]
        public ActionResult PlantDetails(PlantDto plantDto)
        {
            var plantDir = Server.MapPath("~/Images/Plant/" + plantDto.ID.ToString());
            List<Images> imgList = Mappers.ImageMapper.MapHTTPToImage(plantDto.Images, plantDto, plantDir);
            Plant plant = Mappers.PlantMapper.MapDtoToDAL(plantDto, imgList);

            if (plantDto.CustomValues1 != null)
            {
                plantDto.CustomValues1.ID = Guid.NewGuid();
                CustomValues cv1 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues1, 1) as CustomValues;
                plant.CustomValues = cv1;
            }
            if (plantDto.CustomValues2 != null)
            {
                plantDto.CustomValues2.ID = Guid.NewGuid();
                CustomValues cv2 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues2, 2) as CustomValues;
                plant.CustomValues1 = cv2;
            }
            if (plantDto.CustomValues3 != null)
            {
                plantDto.CustomValues3.ID = Guid.NewGuid();
                CustomValues cv3 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues3, 3) as CustomValues;
                plant.CustomValues2 = cv3;
            }
            if (plantDto.CustomValues4 != null)
            {
                plantDto.CustomValues4.ID = Guid.NewGuid();
                CustomValues cv4 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues4, 4) as CustomValues;
                plant.CustomValues3 = cv4;
            }
            if (plantDto.CustomValues5 != null)
            {
                plantDto.CustomValues5.ID = Guid.NewGuid();
                CustomValues cv5 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues5, 5) as CustomValues;
                plant.CustomValues4 = cv5;
            }

            PlantCRUD.Update(plant);


            return RedirectToAction("PlantTable");
        }

        public ActionResult PlantTable()
        {
            return View();
        }

        public ActionResult GetData()
        {
            // Initialization.
            JsonResult result = new JsonResult();
            try
            {
                var strCurrentUserId = User.Identity.GetUserId();

                // Initialization.
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

               
                var listOfPlants = PlantCRUD.Search(strCurrentUserId, search, orderDir,order, startRec, pageSize);
                var tablePtants = new List<PlantTableDto>();
                foreach(var plant in listOfPlants)
                {
                    List<Images> images = ImageCRUD.GetByPlantID(plant.ID);
                    PlantTableDto tableDto = new PlantTableDto();
                    tableDto.PlantId = plant.ID.ToString();
                    tableDto.Name = plant.Name;
                    tableDto.Type = plant.Type;
                    tableDto.Count = plant.Count.ToString();
                    tableDto.Species = plant.Species;
                    tableDto.Description = plant.Notes;
                    string firstImage = "";
                    if(images.Count() > 0)
                    {
                        firstImage = images.First().ImageFilePath;
                        int idx = firstImage.ToLower().IndexOf(@"\images\");
                        firstImage = firstImage.Substring(idx);
                    }
                    tableDto.Image =  firstImage;
                    tablePtants.Add(tableDto);
                }

                int totalRecords = PlantCRUD.GetByUserID(strCurrentUserId).Count();
                int recFilter = listOfPlants.Count();

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = listOfPlants.Count(), recordsFiltered = recFilter, data = tablePtants }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Loger.Log.Error(ex);
            }

            // Return info.
            return result;
        }
    }
}