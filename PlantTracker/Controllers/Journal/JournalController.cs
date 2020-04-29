using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlantTracker.Models.Dto;
using PlantDAL.Repository;
using Microsoft.AspNet.Identity;
using PlantDAL.EDMX;
using System.Configuration;

namespace PlantTracker.Controllers.Journal
{
    [Authorize]
    public class JournalController : Controller
    {
        [HttpPost]
        public ActionResult DeleteImage(string JournalId, string FilePath)
        {
            var curUrl = ConfigurationManager.AppSettings["url"].TrimEnd('/');
            var pathDelete = FilePath.Replace(curUrl, "").Replace("/", "\\");
            try
            {
                var serverDir = Server.MapPath("/").TrimEnd('\\');
                var fileDel = serverDir + pathDelete;
                System.IO.File.Delete(fileDel);

                Images img = ImageCRUD.GetByFilePathAndJournalId(JournalId, fileDel);
                ImageCRUD.Delete(img);
            }
            catch (Exception ex)
            {

            }

            return Json(new { IsError = false, message = "success", data = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteJournal(string JournalId, string FilePath)
        {
            try
            {
                var curUrl = ConfigurationManager.AppSettings["url"].TrimEnd('/');
                var pathDelete = FilePath.Replace(curUrl, "").Replace("/", "\\");

                var serverDir = Server.MapPath("/").TrimEnd('\\');
                var fileDel = serverDir + pathDelete;

                int idx = fileDel.IndexOf(JournalId);
                if (idx != -1)
                {
                    fileDel = fileDel.Substring(0, idx + 36);
                    System.IO.Directory.Delete(fileDel, true);
                }
                Guid journalId = Guid.Parse(JournalId);
                PlantDAL.EDMX.Journal journal = JournalCRUD.GetByID(journalId);
                List<Images> images = ImageCRUD.GetByJournalID(journalId);
                foreach (var img in images)
                {
                    ImageCRUD.Delete(img);
                }

                JournalCRUD.Delete(journal);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return Json(new { IsError = false, message = "success", data = true }, JsonRequestBehavior.AllowGet);
        }



        // GET: Journal
        public ActionResult JournalTable()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewJournal()
        {
            var journalDto = new JournalDto();
            List<PlantDAL.EDMX.Plant> plantList = new List<PlantDAL.EDMX.Plant>();

            plantList = PlantCRUD.GetByUserID(User.Identity.GetUserId());
            foreach (var plant in plantList)
            {
                journalDto.Plants.Add(new SelectListItem
                {
                    Text = plant.Name,
                    Value = plant.ID.ToString()
                });
            }

            return View(journalDto);
        }

        [HttpPost]
        public ActionResult NewJournal(JournalDto journalDto)
        {
            Guid id = Guid.NewGuid();
            journalDto.ID = id;
            journalDto.UserID = User.Identity.GetUserId();

            var plantDir = Server.MapPath("~/Images/Journal/" + id.ToString());
            List<Images> imgList = Mappers.ImageMapper.MapHTTPToImage(journalDto.Images, journalDto, plantDir);
            PlantDAL.EDMX.Journal journal = Mappers.JournalMapper.MapDtoToDAL(journalDto, imgList);
            journal.DateModified = DateTime.Now;

            JournalCRUD.Insert(journal);
            return RedirectToAction("JournalTable");
        }

        [HttpPost]
        public ActionResult GetJournalDetails(string JournalID)
        {
            return RedirectToAction("JournalDetails", new { journalId = JournalID });
        }

        [HttpGet]
        public ActionResult JournalDetails(string journalId)
        {
            PlantDAL.EDMX.Journal journal = JournalCRUD.GetByID(Guid.Parse(journalId));
            JournalDto dto = Mappers.JournalMapper.MapDALToDto(journal);

            //PlantDAL.EDMX.Plant plant = PlantCRUD.GetByID(dto.PlantId);

            //dto.Plants.Add(new SelectListItem
            //{
            //    Text = plant.Name,
            //    Value = plant.ID.ToString()
            //});

            List< PlantDAL.EDMX.Plant> plantList = PlantCRUD.GetByUserID(User.Identity.GetUserId());
            foreach (var plant in plantList)
            {
                dto.Plants.Add(new SelectListItem
                {
                    Text = plant.Name,
                    Value = plant.ID.ToString()
                });
            }

            List<Images> imgs = ImageCRUD.GetByJournalID(Guid.Parse(journalId));

            var curUrl = ConfigurationManager.AppSettings["url"];
            curUrl = curUrl.TrimEnd('/');
            foreach (var img in imgs)
            {
                var idx = img.ImageFilePath.ToLower().IndexOf(@"\images\");
                if (idx != -1)
                {
                    if (dto.imageFilePath == null)
                        dto.imageFilePath = new List<string>();
                    var imgpath = curUrl + img.ImageFilePath.Substring(idx).Replace("\\", "/");
                    dto.imageFilePath.Add(imgpath);
                }
            }          

            return View(dto);
        }

        [HttpPost]
        public ActionResult JournalDetails(JournalDto journalDto)
        {
            var plantDir = Server.MapPath("~/Images/Journal/" + journalDto.ID.ToString());
            List<Images> imgList = Mappers.ImageMapper.MapHTTPToImage(journalDto.Images, journalDto, plantDir);

            foreach (var img in imgList)
            {
                ImageCRUD.Insert(img);
            }

            PlantDAL.EDMX.Journal journal = Mappers.JournalMapper.MapDtoToDAL(journalDto, imgList);
            journal.UserID = User.Identity.GetUserId();

            JournalCRUD.Update(journal);

            return RedirectToAction("JournalTable");
        }


        public ActionResult GetData()
        {
            // Initialization.
            JsonResult result = this.Json(new { draw = Convert.ToInt32(0), recordsTotal = 0, recordsFiltered = 0, data = Array.Empty<JournalTableDto>() }, JsonRequestBehavior.AllowGet);
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


                var listOfJournals = JournalCRUD.Search(strCurrentUserId, search, orderDir, order, startRec, pageSize);
                var tableJournals = new List<JournalTableDto>();
                foreach (var journal in listOfJournals)
                {
                    List<Images> images = ImageCRUD.GetByJournalID(journal.ID);
                    JournalTableDto tableDto = new JournalTableDto();
                    tableDto.JournalId = journal.ID.ToString();
                    tableDto.Name = journal.Name;
                    tableDto.Date = journal.DateModified.ToString();
                    string firstImage = "";
                    if (images.Count() > 0)
                    {
                        firstImage = images.First().ImageFilePath;
                        int idx = firstImage.ToLower().IndexOf(@"\images\");
                        firstImage = firstImage.Substring(idx);
                    }
                    tableDto.Image = firstImage;
                    tableJournals.Add(tableDto);
                }

                int totalRecords = PlantCRUD.GetByUserID(strCurrentUserId).Count();
                int recFilter = listOfJournals.Count();

                // Loading drop down lists.
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = listOfJournals.Count(), recordsFiltered = recFilter, data = tableJournals }, JsonRequestBehavior.AllowGet);
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