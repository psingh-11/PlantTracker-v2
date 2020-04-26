using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PlantDAL.EDMX;
using PlantDAL.Repository;
using PlantTracker.Models.Dto;

namespace PlantTracker.Mappers
{
    public class ImageMapper
    {
        public static List<Images> MapHTTPToImage(List<HttpPostedFileBase> httpImages, PlantDto plant, string serverPath)
        {
            List<Images> imgList = new List<Images>();

            var plantDir = serverPath;
            if (!Directory.Exists(plantDir))
            {
                Directory.CreateDirectory(plantDir);
            }

            foreach (HttpPostedFileBase file in plant.Images)
            {
                Guid imageId = Guid.NewGuid();
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    var ServerSavePath = Path.Combine(plantDir, imageId + extension);

                    Images img = new Images();
                    img.ID = imageId;
                    img.ImageFilePath = ServerSavePath;
                    img.PlantID = plant.ID;
                    //ImageCRUD.Insert(img);

                    file.SaveAs(ServerSavePath);

                    imgList.Add(img);
                }
            }

            return imgList;
        }

        public static List<Images> MapHTTPToImage(List<HttpPostedFileBase> httpImages, JournalDto journal, string serverPath)
        {
            List<Images> imgList = new List<Images>();

            var plantDir = serverPath;
            if (!Directory.Exists(plantDir))
            {
                Directory.CreateDirectory(plantDir);
            }

            foreach (HttpPostedFileBase file in journal.Images)
            {
                Guid imageId = Guid.NewGuid();
                if (file != null)
                {
                    string extension = Path.GetExtension(file.FileName);
                    var ServerSavePath = Path.Combine(plantDir, imageId + extension);

                    Images img = new Images();
                    img.ID = imageId;
                    img.ImageFilePath = ServerSavePath;
                    img.JournalID = journal.ID;
                    //ImageCRUD.Insert(img);

                    file.SaveAs(ServerSavePath);

                    imgList.Add(img);
                }
            }

            return imgList;
        }
    }
}