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
using System.Configuration;


namespace PlantTracker.Controllers
{
    [Authorize]
    public class PlantController : Controller
    {
        [HttpGet]
        public ActionResult NewPlant()
        {
            var plantDto = new PlantDto();
            List<PlantDAL.EDMX.Plant> plantList = new List<PlantDAL.EDMX.Plant>();

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
            PlantDAL.EDMX.Plant plant = Mappers.PlantMapper.MapDtoToDAL(plantDto, imgList);

            if (plantDto.CustomValues1.Name != null && plantDto.CustomValues1.Notes != null)
            {
                plantDto.CustomValues1.ID = Guid.NewGuid();
                CustomValues cv1 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues1, 1) as CustomValues;
                plant.CustomValues = cv1;
            }
            if (plantDto.CustomValues2.Name != null && plantDto.CustomValues2.Notes != null)
            {
                plantDto.CustomValues2.ID = Guid.NewGuid();
                CustomValues cv2 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues2, 2) as CustomValues;
                plant.CustomValues1 = cv2;
            }
            if (plantDto.CustomValues3.Name != null && plantDto.CustomValues3.Notes != null)
            {
                plantDto.CustomValues3.ID = Guid.NewGuid();
                CustomValues cv3 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues3, 3) as CustomValues;
                plant.CustomValues2 = cv3;
            }
            if (plantDto.CustomValues4.Name != null && plantDto.CustomValues4.Notes != null)
            {
                plantDto.CustomValues4.ID = Guid.NewGuid();
                CustomValues cv4 =  Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues4, 4) as CustomValues;
                plant.CustomValues3 = cv4;
            }
            if (plantDto.CustomValues5.Name != null && plantDto.CustomValues5.Notes != null)
            {
                plantDto.CustomValues5.ID = Guid.NewGuid();
                CustomValues cv5 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues5, 5) as CustomValues;
                plant.CustomValues4 = cv5;
            }

            PlantCRUD.Insert(plant);

            return RedirectToAction("PlantTable");
        }


        [HttpPost]
        public ActionResult DeletePlant(string PlantId, string FilePath)
        {
            try
            {       
                var curUrl = ConfigurationManager.AppSettings["url"].TrimEnd('/');
                var pathDelete = FilePath.Replace(curUrl, "").Replace("/", "\\");

                var serverDir = Server.MapPath("/").TrimEnd('\\');
                var fileDel = serverDir + pathDelete;

                int idx = fileDel.IndexOf(PlantId);
                if(idx != -1)
                {
                    fileDel = fileDel.Substring(0, idx + 36);
                    System.IO.Directory.Delete(fileDel, true);
                }
                Guid plantId = Guid.Parse(PlantId);
                PlantDAL.EDMX.Plant plant = PlantCRUD.GetByID(plantId);
                List<Images> images = ImageCRUD.GetByPlantID(plantId);
                foreach(var img in images)
                {
                    ImageCRUD.Delete(img);
                }


                if(plant.CustomValueOneID != null)
                {
                    CustomValues cv = CustomValueCRUD.GetByID(plant.CustomValueOneID);
                    CustomValueCRUD.Delete(cv);
                }
                if (plant.CustomValueTwoD != null)
                {
                    CustomValues cv = CustomValueCRUD.GetByID(plant.CustomValueTwoD);
                    CustomValueCRUD.Delete(cv);
                }
                if (plant.CustomValueThreeID != null)
                {
                    CustomValues cv = CustomValueCRUD.GetByID(plant.CustomValueThreeID);
                    CustomValueCRUD.Delete(cv);
                }
                if (plant.CustomValueFourID != null)
                {
                    CustomValues cv = CustomValueCRUD.GetByID(plant.CustomValueFourID);
                    CustomValueCRUD.Delete(cv);
                }
                if (plant.CustomValueFiveID != null)
                {
                    CustomValues cv = CustomValueCRUD.GetByID(plant.CustomValueFiveID);
                    CustomValueCRUD.Delete(cv);
                }

                List<PlantDAL.EDMX.Journal> journals = JournalCRUD.GetByPlantID(plant.ID);
                foreach(var jour in journals)
                {
                    List<Images> jourImages = ImageCRUD.GetByJournalID(jour.ID);
                    foreach (var img in jourImages)
                    {
                        ImageCRUD.Delete(img);
                    }

                    JournalCRUD.Delete(jour);
                }

                List<PlantDAL.EDMX.Plant> childrenPlants = PlantCRUD.GetByParentId(plant.ID);
                foreach(var plt in childrenPlants)
                {
                    if(plt.ParentOneID == plant.ID)
                    {
                        plt.ParentOneID = null;
                    }
                    if(plt.ParentTwoID == plant.ID)
                    {
                        plt.ParentTwoID = null;
                    }

                    PlantCRUD.Update(plt);
                }

                PlantCRUD.Delete(plant);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Json(new { IsError = false, message = "success", data = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteImage(string PlantId, string FilePath)
        {
            var curUrl = ConfigurationManager.AppSettings["url"].TrimEnd('/');
            var pathDelete = FilePath.Replace(curUrl, "").Replace("/", "\\");
            try
            {
                var serverDir = Server.MapPath("/").TrimEnd('\\');
                var fileDel = serverDir + pathDelete;
                System.IO.File.Delete(fileDel);

                Images img = ImageCRUD.GetByFilePathAndPlantId(PlantId, fileDel);
                ImageCRUD.Delete(img);
            }
            catch (Exception ex)
            {

            }

            return Json(new { IsError = false, message = "success", data = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult PlantDetails(string plantId)
       {
            PlantDAL.EDMX.Plant plant = PlantCRUD.GetByID(Guid.Parse(plantId));
            PlantDto dto = Mappers.PlantMapper.MapDALToDto(plant);

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

            List<PlantDAL.EDMX.Journal> journalList = JournalCRUD.GetByPlantID(dto.ID);
            foreach(var jour in journalList)
            {
                dto.Journals.Add(new SelectListItem
                {
                    Text = jour.Name,
                    Value = jour.ID.ToString()
                });
            }


            List<Images> imgs = ImageCRUD.GetByPlantID(Guid.Parse(plantId));
            
            var curUrl = ConfigurationManager.AppSettings["url"];
            curUrl = curUrl.TrimEnd('/');
            foreach (var img in imgs)
            {
                var idx = img.ImageFilePath.ToLower().IndexOf(@"\images\");
                if(idx != -1)
                {
                    if (dto.imageFilePath == null)
                        dto.imageFilePath = new List<string>();
                    var imgpath = curUrl + img.ImageFilePath.Substring(idx).Replace("\\", "/");
                    dto.imageFilePath.Add(imgpath);
                }
            }

            if(plant.CustomValueOneID != null || plant.CustomValueOneID == Guid.Empty)
            {
                plant.CustomValues = CustomValueCRUD.GetByID(plant.CustomValueOneID);
                dto.CustomValues1 = Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues, 1) as CustomValueDto;
            }
            if (plant.CustomValueTwoD != null || plant.CustomValueOneID == Guid.Empty)
            {
                plant.CustomValues1 = CustomValueCRUD.GetByID(plant.CustomValueTwoD);
                dto.CustomValues2= Mappers.CustomValueMapper.MapDALToDto(plant.CustomValues1, 2) as CustomValueDto;
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

        [HttpPost]
        public ActionResult PlantDetails(PlantDto plantDto)
        {
            var plantDir = Server.MapPath("~/Images/Plant/" + plantDto.ID.ToString());
            List<Images> imgList = Mappers.ImageMapper.MapHTTPToImage(plantDto.Images, plantDto, plantDir);

            foreach(var img in imgList)
            {
                ImageCRUD.Insert(img);
            }


            //List<Images> pltImgList = ImageCRUD.GetByPlantID(plantDto.ID);
            //foreach(var img in pltImgList)
            //{
            //    imgList.Add(img);
            //}


            PlantDAL.EDMX.Plant plant = Mappers.PlantMapper.MapDtoToDAL(plantDto, imgList);
            plant.UserID = User.Identity.GetUserId();

            if (plantDto.CustomValues1.Name != null && plantDto.CustomValues1.Notes != null)
            {
                if(plantDto.CustomValues1.ID == Guid.Empty)
                {
                    plantDto.CustomValues1.ID = Guid.NewGuid();
                }
                
                CustomValues cv1 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues1, 1) as CustomValues;
                plant.CustomValues = cv1;
                plant.CustomValueOneID = cv1.ID;

                CustomValueCRUD.Update(cv1);
            }
            if (plantDto.CustomValues2.Name != null && plantDto.CustomValues2.Name != null)
            {
                if (plantDto.CustomValues2.ID == Guid.Empty)
                {
                    plantDto.CustomValues2.ID = Guid.NewGuid();
                }

                CustomValues cv2 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues2, 2) as CustomValues;
                plant.CustomValues1 = cv2;
                plant.CustomValueTwoD = cv2.ID;

                CustomValueCRUD.Update(cv2);
            }
            if (plantDto.CustomValues3.Name != null && plantDto.CustomValues3.Name != null)
            {
                if (plantDto.CustomValues3.ID == Guid.Empty)
                {
                    plantDto.CustomValues3.ID = Guid.NewGuid();
                }

                CustomValues cv3 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues3, 3) as CustomValues;
                plant.CustomValues2 = cv3;
                plant.CustomValueThreeID = cv3.ID;

                CustomValueCRUD.Update(cv3);
            }
            if (plantDto.CustomValues4.Name != null && plantDto.CustomValues4.Name != null)
            {
                if (plantDto.CustomValues4.ID == Guid.Empty)
                {
                    plantDto.CustomValues4.ID = Guid.NewGuid();
                }

                CustomValues cv4 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues4, 4) as CustomValues;
                plant.CustomValues3 = cv4;
                plant.CustomValueFourID = cv4.ID;

                CustomValueCRUD.Update(cv4);
            }
            if (plantDto.CustomValues5.Name != null && plantDto.CustomValues5.Name != null)
            {
                if (plantDto.CustomValues5.ID == Guid.Empty)
                {
                    plantDto.CustomValues5.ID = Guid.NewGuid();
                }

                CustomValues cv5 = Mappers.CustomValueMapper.MapDtoToDAL(plantDto.CustomValues5, 5) as CustomValues;
                plant.CustomValues4 = cv5;
                plant.CustomValueFiveID = cv5.ID;

                CustomValueCRUD.Update(cv5);
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