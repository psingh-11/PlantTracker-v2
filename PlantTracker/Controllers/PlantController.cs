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

            return View(plantDto);
        }

        //[HttpPost]
        //public ActionResult NewPlant(PlantDto plantDto)
        //{


        //    return View(plantDto);
        //}

        [HttpPost]
        public ActionResult NewPlant(PlantDto plantDto)
        {
            Guid id = Guid.NewGuid();
            plantDto.ID = id;
            plantDto.UserID = User.Identity.GetUserId();

            foreach (HttpPostedFileBase file in Request.Files)
            {
                //Checking file is available to save.  
                if (file != null)
                {
                    var InputFileName = Path.GetFileName(file.FileName);
                    var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                    //Save file to server folder  
                    file.SaveAs(ServerSavePath);
                    //assigning file uploaded status to ViewBag for showing message to user.  
                    //ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                }

            }



            if (plantDto.CustomValues1 != null)
            {
                plantDto.CustomValues1.ID = Guid.NewGuid();
            }
            if (plantDto.CustomValues2 != null)
            {
                plantDto.CustomValues2.ID = Guid.NewGuid();
            }
            if (plantDto.CustomValues3 != null)
            {
                plantDto.CustomValues3.ID = Guid.NewGuid();
            }
            if (plantDto.CustomValues4 != null)
            {
                plantDto.CustomValues4.ID = Guid.NewGuid();
            }
            if (plantDto.CustomValues5 != null)
            {
                plantDto.CustomValues5.ID = Guid.NewGuid();
            }


            //PlantCRUD.Insert(plant);
            return PlantTable();
        }

        [HttpGet]
        public ActionResult PlantDetails(/*Guid plantId*/)
        {
            //Plant plant = PlantCRUD.GetByID(plantId);
            Plant plant = new Plant();
            return View(plant);
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

                //AdditionalInfoDb db = new AdditionalInfoDb(_setting.DbConnection, _setting.TblProfessionals, _setting.TblAdditionalInfo);
                //var data = db.GetData(startRec, pageSize, order, orderDir, search);
                //int totalRecords = db.GetTotalCount();
                //int recFilter = db.GetFilteredCount(startRec, pageSize, search);

                var data = new[]
                {
                        new {
                            PlantId = "12002",
                            Image = @"\Images\test.jpg",
                            Name = "test",
                            Count = 2,
                            Type = "Indoor Plant",
                            Species = "test species",
                            Description = "This is test "
                        }
                }.ToList();


                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = data.Count(), recordsFiltered = 1, data = data }, JsonRequestBehavior.AllowGet);
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