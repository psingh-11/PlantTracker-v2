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

namespace PlantTracker.Controllers.Journal
{
    public class JournalViewController : Controller
    {
        [HttpGet]
        public ActionResult JournalDisplay(string journalId)
        {
            var curUrl = ConfigurationManager.AppSettings["url"];
            curUrl = curUrl.TrimEnd('/');

            PlantDAL.EDMX.Journal journal = JournalCRUD.GetByID(Guid.Parse(journalId));
            JournalDto dto = Mappers.JournalMapper.MapDALToDto(journal);

            List<Images> imgs = ImageCRUD.GetByJournalID(Guid.Parse(journalId));

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

            PlantDAL.EDMX.Plant plant = PlantCRUD.GetByID(dto.PlantId);

                dto.Plants.Add(new SelectListItem
                {
                    Text = plant.Name,
                    Value = plant.ID.ToString()
                });
            
            return View(dto);
        }
    }
}